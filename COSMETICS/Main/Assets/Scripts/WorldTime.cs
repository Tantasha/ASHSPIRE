using System;
using System.Collections;
using UnityEngine;

namespace WorldTime
{
    public class WorldTime : MonoBehaviour
    {
        public event EventHandler<TimeSpan> WorldTimeChanged;

        [SerializeField]
        private float dayLength;
        private TimeSpan currentTime;

        private float minuteLength => dayLength / WorldTimeConstant.MinutesInDay;

        private void Start()
        {
            StartCoroutine(routine: AddMinute());
        }
        private IEnumerator AddMinute()
        {
            currentTime += TimeSpan.FromMinutes(1);
            WorldTimeChanged?.Invoke(sender: this, currentTime);
            yield return new WaitForSeconds(minuteLength);
            StartCoroutine(routine: AddMinute());
        }
    }

}

