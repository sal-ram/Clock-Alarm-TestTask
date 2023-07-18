using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class TimeChecker: MonoBehaviour
    {
        [SerializeField] private InternetTimeService InternetTimeService;

        public event Action<TimeStruct> TimeChecked;

        private const float _tickIntervalForCheck = 60f * 60f; // 1 час = 3600 сек
        private float _currentCheckerTime = 60f * 60f;

        private TimeStruct _actualTime;

        private void OnEnable()
        {
            Timer.TimeUpdated += UpdateTime;
        }

        private void OnDisable()
        {
            Timer.TimeUpdated -= UpdateTime;
        }

        private void UpdateTime(TimeStruct time)
        {
            _actualTime = time;
        }

        private void Update()
        {
            _currentCheckerTime += Time.deltaTime;

            if (_currentCheckerTime >= _tickIntervalForCheck)
            {
                _currentCheckerTime = 0f;
                CheckTimeOnFirstService();
                CheckTimeOnSecondService();
            }
        }

        public async void CheckTimeOnFirstService()
        {
            var internetTime = await InternetTimeService.GetInternetTime(TimeAPIServices.NinjaTimeAPI);

            CheckIsUpdatingNeccesary(internetTime);
        }

        public async void CheckTimeOnSecondService()
        {
            var internetTime = await InternetTimeService.GetInternetTime(TimeAPIServices.WorldTimeAPI);

            CheckIsUpdatingNeccesary(internetTime);
        }

        private void CheckIsUpdatingNeccesary(TimeStruct internetTime)
        {
            if (!CompareTime(_actualTime, internetTime))
            {
                TimeChecked.Invoke(internetTime);
            }
        }

        private bool CompareTime(TimeStruct localTime, TimeStruct internetTime)
        {
            if (localTime.Hour != internetTime.Hour)
                return false;
            else if (localTime.Hour != internetTime.Hour)
                return false;
            else if (localTime.Sec != internetTime.Sec)
                return false;

            return true;
        }
    }
}
