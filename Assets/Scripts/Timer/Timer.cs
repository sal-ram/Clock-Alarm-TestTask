using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Timer : MonoBehaviour
    {
        private readonly float _tickInterval = 1.0f;
        private float _currentTime = 0f;
        private ClockTimeInfo _clockTimeInfo = new();

        [SerializeField] private TimeChecker timeChecker;

        public static event Action<TimeStruct> TimeUpdated;

        private bool IsActive = true;

        private void OnEnable()
        {
            timeChecker.TimeChecked += FixTime;
        }

        private void OnDisable()
        {
            timeChecker.TimeChecked -= FixTime;
        }

        private void Start()
        {
            SetDefaultTime();
        }

        private void Update()
        {
            _currentTime += Time.deltaTime;

            if (_currentTime >= _tickInterval)
            {
                _currentTime = 0f;
                _clockTimeInfo.AddNewSec();

                if (IsActive)
                {
                    TimeUpdated?.Invoke(_clockTimeInfo.GetTime());
                }
            }
        }

        private void FixTime(TimeStruct timeCorrect)
        {
            _clockTimeInfo.SetNewTime(timeCorrect);
            TimeUpdated?.Invoke(timeCorrect);
        }

        private void SetDefaultTime()
        {
            TimeStruct time = new();
            time.Hour = 0;
            time.Min = 0;
            time.Sec = 0;

            _clockTimeInfo.SetNewTime(time);
        }

        public void TurnOnOffTimer(bool IsActive)
        {
            this.IsActive = IsActive;
        }
    }
}
