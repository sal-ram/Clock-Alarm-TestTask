using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class AlarmTimeInfo : TimeInfo
    {
        private TimeStruct alarmTimeData;

        public void UpdateTimeFromAnalog(float hour, float min, float sec)
        {
            Sec += sec;

            if (Sec >= _totalAmountOfSec)
                Sec %= _totalAmountOfSec;
            else if (Sec < 0)
                Sec = _totalAmountOfSec + Sec % _totalAmountOfSec;

            Min += min;

            if (Min >= _totalAmountOfMin)
                Min %= _totalAmountOfMin;
            else if (Min < 0)
                Min += _totalAmountOfMin + Min % _totalAmountOfMin;

            Hour += hour;

            if (Hour >= _totalAmountOfHour)
                Hour %= _totalAmountOfHour;
            else if (Hour < 0)
                Hour += _totalAmountOfHour + Hour % _totalAmountOfHour;
        }

        public void UpdateTimeFromDigit(float hour, float min, float sec)
        {
            if (0 <= hour && hour < 24 && 0 <= min && min < 60 && 0 <= sec && sec < 60)
            {
                CalculateAccurateTime(hour, min, sec);
            }
            else
            {
                Debug.Log("Данные не входят в диапазон");
            }
        }

        public override void SetNewTime(TimeStruct time)
        {
            CalculateAccurateTime(time.Hour, time.Min, time.Sec);
        }

        public TimeStruct GetAlarmTime()
        {
            return alarmTimeData;
        }

        public void UpdateAlarmTime()
        {
            alarmTimeData = GetTime();
        }
    }
}
