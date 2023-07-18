using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public abstract class TimeInfo
    {
        public float Sec { get; protected set; }
        public float Min { get; protected set; }
        public float Hour { get; protected set; }

        protected const float _totalAmountOfSec = 60f;
        protected const float _totalAmountOfMin = 60f;
        protected const float _totalAmountOfHour = 24f;

        public abstract void SetNewTime(TimeStruct time);

        public TimeStruct GetTime()
        {
            TimeStruct time = new TimeStruct();
            time.Hour = Hour;
            time.Min = Min;
            time.Sec = Sec;

            return time;
        }

        protected void CalculateAccurateTime(float hour, float min, float sec)
        {
            Sec = sec;
            Min = min + Sec / _totalAmountOfSec;
            Hour = hour + Min / _totalAmountOfMin;
        }
    }
}
