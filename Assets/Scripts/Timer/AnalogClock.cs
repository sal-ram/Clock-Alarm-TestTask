using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class AnalogClock : Clock
    {
        [SerializeField]
        private GameObject _hourHand;
        [SerializeField]
        private GameObject _minHand;
        [SerializeField]
        private GameObject _secHand;

        private const float _totalAmountOfSec = 60f;
        private const float _totalAmountOfMin = 60f;

        protected override void UpdateClockwise(TimeStruct time)
        {
            TranslateTo12h(ref time.Hour);
            CalculateAccurateTime(ref time);
            TurnHourClockwise(time.Hour);
            TurnMinClockwise(time.Min);
            TurnSecClockwise(time.Sec);
        }

        private void TranslateTo12h(ref float hour)
        {
            if (hour > 12)
                hour -= 12;
            else if (hour == 0)
                hour = 12;
        }

        private void CalculateAccurateTime(ref TimeStruct time)
        {
            time.Min += time.Sec / _totalAmountOfSec;
            time.Hour += time.Min / _totalAmountOfMin;
        }

        protected override void TurnHourClockwise(float hour)
        {
            var rotation = new Vector3(0, 0, -1 * hour * 30);
            _hourHand.transform.eulerAngles = rotation;
        }

        protected override void TurnMinClockwise(float min)
        {
            var rotation = new Vector3(0, 0, -1 * min * 6);
            _minHand.transform.eulerAngles = rotation;
        }

        protected override void TurnSecClockwise(float sec)
        {
            var rotation = new Vector3(0, 0, -1 * sec * 6);
            _secHand.transform.eulerAngles = rotation;
        }
    }
}
