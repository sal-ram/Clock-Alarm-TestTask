using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public abstract class Clock : MonoBehaviour
    {
        private void OnEnable()
        {
            Timer.TimeUpdated += UpdateClockwise;
        }

        private void OnDisable()
        {
            Timer.TimeUpdated -= UpdateClockwise;
        }

        protected abstract void UpdateClockwise(TimeStruct time);
        protected abstract void TurnHourClockwise(float hour);
        protected abstract void TurnMinClockwise(float min);
        protected abstract void TurnSecClockwise(float sec);
    }
}
