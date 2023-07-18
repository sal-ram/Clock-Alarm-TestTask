using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class AnalogHandsManager: MonoBehaviour
    {
        [SerializeField] private HandMover _handMoverHour;
        [SerializeField] private HandMover _handMoverMin;
        [SerializeField] private HandMover _handMoverSec;

        private readonly float oneSecAngle = 360 / 60;
        private readonly float oneMinAngle = 360 / 60;
        private readonly float oneHourAngle = 360 / 12;
        private const float _totalAmountOfSec = 60f;
        private const float _totalAmountOfMin = 60f;
        private const float _totalAmountOfHour = 24f;

        public event Action<float, float, float> AnalogTimeUpdated;

        private void OnEnable()
        {
            _handMoverHour.TimeUpdated += HourHandUpdated;
            _handMoverMin.TimeUpdated += MinHandUpdated;
            _handMoverSec.TimeUpdated += SecHandUpdated;
        }

        private void OnDisable()
        {
            _handMoverHour.TimeUpdated -= HourHandUpdated;
            _handMoverMin.TimeUpdated -= MinHandUpdated;
            _handMoverSec.TimeUpdated -= SecHandUpdated;
        }
        private void Awake()
        {
            TurnOnOffHands(false);
        }
        private void UpdateSecHand(float sec)
        {
            _handMoverSec.TurnHandMover(sec);
        }
        private void UpdateMinHand(float min)
        {
            _handMoverMin.TurnHandMover(min);
        }
        private void UpdateHourHand(float hour)
        {
            _handMoverHour.TurnHandMover(hour);
        }

        private void HourHandUpdated(float hourAngleDelta)
        {
            var hour = hourAngleDelta / oneHourAngle;
            var min = hour * _totalAmountOfMin;
            var sec = min * _totalAmountOfSec;

            UpdateSecHand(sec);
            UpdateMinHand(min);
            AnalogTimeUpdated.Invoke(hour, min, sec);
        }

        private void MinHandUpdated(float minAngleDelta)
        {
            var min = minAngleDelta / oneMinAngle;
            var sec = min * _totalAmountOfSec;
            var hour = min / _totalAmountOfMin;

            UpdateSecHand(sec);
            UpdateHourHand(hour);
            AnalogTimeUpdated.Invoke(hour, min, sec);
        }

        private void SecHandUpdated(float secAngleDelta)
        {
            var sec = secAngleDelta / oneSecAngle;
            var min = sec / _totalAmountOfSec;
            var hour = min / _totalAmountOfMin;

            UpdateMinHand(min);
            UpdateHourHand(hour);
            AnalogTimeUpdated.Invoke(hour, min, sec);
        }

        public void SetNewTime(float hour, float min, float sec)
        {
            _handMoverSec.SetNewTime(sec);
            _handMoverMin.SetNewTime(min);
            _handMoverHour.SetNewTime(hour);
        }

        public void TurnOnOffHands(bool IsActive)
        {
            _handMoverHour.ActivateHand(IsActive);
            _handMoverMin.ActivateHand(IsActive);
            _handMoverSec.ActivateHand(IsActive);
        }
    }
}
