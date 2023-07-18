using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public class ClockTimeInfo : TimeInfo
    {
        public override void SetNewTime(TimeStruct time)
        {
            Hour = time.Hour;
            Min = time.Min;
            Sec = time.Sec;
        }

        public void AddNewSec()
        {
            Sec++;

            if (Sec >= 60)
            {
                Sec = 0;
                Min++;

                if (Min >= 60)
                {
                    Min = 0;
                    Hour++;

                    if (Hour >= 24)
                    {
                        Hour = 0;
                    }
                }
            }
        }
    }
}
