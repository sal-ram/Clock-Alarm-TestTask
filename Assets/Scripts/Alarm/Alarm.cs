using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Assets.Scripts
{
    public class Alarm : MonoBehaviour
    {
        [SerializeField] private AnalogHandsManager _analogHandsManager;
        [SerializeField] private DigitalHandsManager _digitalHandsManager;
        [SerializeField] private TMP_Text _alarmTimeText;

        private bool IsAlarmActive;

        private AlarmTimeInfo _alarmTimeInfo = new();

        private void OnEnable()
        {
            _analogHandsManager.AnalogTimeUpdated += UpdateTimeInfoFromAnalog;
            _digitalHandsManager.DigitTimeUpdated += UpdateTimeInfoFromDigit;
            Timer.TimeUpdated += SetTimeFromTimer;
        }

        private void OnDisable()
        {
            _analogHandsManager.AnalogTimeUpdated -= UpdateTimeInfoFromAnalog;
            _digitalHandsManager.DigitTimeUpdated -= UpdateTimeInfoFromDigit;
            Timer.TimeUpdated -= SetTimeFromTimer;
        }

        private void UpdateTimeInfoFromAnalog(float hour, float min, float sec)
        {
            _alarmTimeInfo.UpdateTimeFromAnalog(-hour, -min, -sec);
            _digitalHandsManager.SetNewTime(_alarmTimeInfo.Hour, _alarmTimeInfo.Min, _alarmTimeInfo.Sec);
        }

        private void UpdateTimeInfoFromDigit(float hour, float min, float sec)
        {
            _alarmTimeInfo.UpdateTimeFromDigit(hour, min, sec);
            _analogHandsManager.SetNewTime(_alarmTimeInfo.Hour, _alarmTimeInfo.Min, _alarmTimeInfo.Sec);
        }

        private void SetTimeFromTimer(TimeStruct time)
        {
            if (CheckIsAlarmNow(_alarmTimeInfo.GetAlarmTime(), time) && IsAlarmActive)
                CallAlarm();

            _alarmTimeInfo.SetNewTime(time);
        }

        private bool CheckIsAlarmNow(TimeStruct timeAlarm, TimeStruct timeActual)
        {
            if ((int)timeAlarm.Hour != (int)timeActual.Hour)
                return false;
            if ((int)timeAlarm.Min != (int)timeActual.Min)
                return false;
            if ((int)timeAlarm.Sec != (int)timeActual.Sec)
                return false;

            return true;
        }

        private void CallAlarm()
        {
            var alarmTime = _alarmTimeInfo.GetAlarmTime();
            Debug.Log("Будильник в " + (int)alarmTime.Hour + ":" + (int)alarmTime.Min + ":" + (int)alarmTime.Sec + " Пора вставать! Повторится через сутки, если не выключить");
            StartCoroutine(AlarmNotification(alarmTime)); 
        }

        public void SetAlarm(bool IsActive)
        {
            IsAlarmActive = IsActive;

            if (IsAlarmActive)
            {
                _alarmTimeInfo.UpdateAlarmTime();
                var alarmTime = _alarmTimeInfo.GetAlarmTime();
                Debug.Log("Будильник будет в " + (int)alarmTime.Hour + ":" + (int)alarmTime.Min + ":" + (int)alarmTime.Sec);
                _alarmTimeText.text = "Будильник будет в " + (int)alarmTime.Hour + ":" + (int)alarmTime.Min + ":" + (int)alarmTime.Sec;
            }
            else 
            {
                Debug.Log("Будильник удален");
            }
        }

        private IEnumerator AlarmNotification(TimeStruct alarmTime)
        {
            _alarmTimeText.text = "Будильник в " + (int)alarmTime.Hour + ":" + (int)alarmTime.Min + ":" + (int)alarmTime.Sec + "!";
            yield return new WaitForSeconds(3f);
            _alarmTimeText.text = "Будильник будет в " + (int)alarmTime.Hour + ":" + (int)alarmTime.Min + ":" + (int)alarmTime.Sec;
        }
    }
}
