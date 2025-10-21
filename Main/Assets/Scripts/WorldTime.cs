using System;
using System.Collections;
using UnityEngine;

namespace WorldTime
{
    public class WorldTime : MonoBehaviour
    {
        // Event that gets triggered every time the in-game time changes.
        public event EventHandler<TimeSpan> WorldTimeChanged;

        [SerializeField]
        private float dayLength;// Length of a full in-game day
        private TimeSpan currentTime;// Stores the current in-game time

        // Each in-game minute�s duration in real-time seconds.
        private float minuteLength => dayLength / WorldTimeConstant.MinutesInDay;

        private void Start()
        {
            // Start continuously adding in-game minutes
            StartCoroutine(routine: AddMinute());
        }
        private IEnumerator AddMinute()
        {
            currentTime += TimeSpan.FromMinutes(1);//increase in-game time by one minute
            WorldTimeChanged?.Invoke(sender: this, currentTime);//notify WorldLight time changed and passing update currentTime
            yield return new WaitForSeconds(minuteLength);//wait for real time duration of one in-game minute
            StartCoroutine(routine: AddMinute());//repeats the process in a loop
        }
    }

}

