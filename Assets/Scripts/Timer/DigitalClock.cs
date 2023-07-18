using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Assets.Scripts
{
    class DigitalClock : Clock
    {

        [SerializeField]  private TMP_InputField _hourDigit;
        [SerializeField]  private TMP_InputField _minDigit;
        [SerializeField]  private TMP_InputField _secDigit;

        protected override void UpdateClockwise(TimeStruct time)
        {
            TurnHourClockwise(time.Hour);
            TurnMinClockwise(time.Min);
            TurnSecClockwise(time.Sec);
        }

        protected override void TurnHourClockwise(float hour)
        {
            var strHour = hour.ToString();

            if ((int) hour / 10 == 0)
                strHour = "0" + hour;

            _hourDigit.text = strHour.ToString();
        }

        protected override void TurnMinClockwise(float min)
        {
            var strMin = min.ToString();

            if ((int) min / 10 == 0)
                strMin = "0" + min;

            _minDigit.text = strMin.ToString();
        }

        protected override void TurnSecClockwise(float sec)
        {
            var strSec = sec.ToString();

            if ((int)sec / 10 == 0)
                strSec = "0" + sec;

            _secDigit.text = strSec.ToString();
        }
    }
}
