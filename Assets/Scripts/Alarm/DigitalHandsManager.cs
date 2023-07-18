using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Assets.Scripts
{
    public class DigitalHandsManager: MonoBehaviour
    {
        [SerializeField] private TMP_InputField _handInputHour;
        [SerializeField] private TMP_InputField _handInputMin;
        [SerializeField] private TMP_InputField _handInputSec;

        public event Action<float, float, float> DigitTimeUpdated;

        private void OnEnable()
        {
            _handInputHour.onEndEdit.AddListener(HourHandUpdated);
            _handInputMin.onEndEdit.AddListener(MinHandUpdated);
            _handInputSec.onEndEdit.AddListener(SecHandUpdated);
        }

        private void OnDisable()
        {
            _handInputHour.onEndEdit.RemoveListener(HourHandUpdated);
            _handInputMin.onEndEdit.RemoveListener(MinHandUpdated);
            _handInputSec.onEndEdit.RemoveListener(SecHandUpdated);
        }

        private void Awake()
        {
            TurnOnOffInputDigits(false);
        }

        private void HourHandUpdated(string hourStr)
        {
            var hour = float.Parse(hourStr);
            var min = float.Parse(_handInputMin.text);
            var sec = float.Parse(_handInputSec.text);

            DigitTimeUpdated(hour, min, sec);
        }

        private void MinHandUpdated(string minStr)
        {
            var min = float.Parse(minStr);
            var hour = float.Parse(_handInputHour.text);
            var sec = float.Parse(_handInputSec.text);

            DigitTimeUpdated(hour, min, sec);
        }

        private void SecHandUpdated(string secStr)
        {
            var sec = float.Parse(secStr);
            var min = float.Parse(_handInputMin.text);
            var hour = float.Parse(_handInputHour.text);

            DigitTimeUpdated(hour, min, sec);
        }

        public void SetNewTime(float hour, float min, float sec)
        {
            SetDigitToInput(_handInputHour, (int) hour);
            SetDigitToInput(_handInputMin, (int) min);
            SetDigitToInput(_handInputSec, (int) sec);
        }

        private void SetDigitToInput(TMP_InputField inputField, int time)
        {
            var strTime = time.ToString();

            if ((int)time / 10 == 0)
                strTime = "0" + time;

            inputField.text = strTime.ToString();
        }

        public void TurnOnOffInputDigits(bool IsActive)
        {
            _handInputHour.interactable = IsActive;
            _handInputMin.interactable = IsActive;
            _handInputSec.interactable = IsActive;
        }
    }
}
