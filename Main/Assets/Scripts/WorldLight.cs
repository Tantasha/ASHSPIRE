using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace WorldTime
{
    [RequireComponent(typeof(Light2D))]
    public class WorldLight : MonoBehaviour
    {
        private Light2D _light;//reference to Light2D component controlling in-game lighting

        [SerializeField]
        private WorldTime _worldTime;//reference to WorldTime script

        [SerializeField]
        private Gradient _gradient;//used to define colour transitions

        private void Awake()
        {
            _light = GetComponent<Light2D>();//get Light2D component attached to GameObject
            _worldTime.WorldTimeChanged += OnWorldTimeChanged;//notify WorldTime whenever in-game time changes
        }

        private void OnDestroy()
        {
            //prevent memory leaks
            _worldTime.WorldTimeChanged -= OnWorldTimeChanged;
        }

        private void OnWorldTimeChanged(object sender, TimeSpan newTime)
        {
            //Evaluate gradient percentage of a day and update Light2D colour accordingly
            _light.color = _gradient.Evaluate(time: PercentageOfDay(newTime));
        }

        private float PercentageOfDay(TimeSpan timeSpan)
        {
            //ensures it wraps around correctly after 24 hours
            return (float) timeSpan.TotalMinutes % WorldTimeConstant.MinutesInDay / WorldTimeConstant.MinutesInDay;
        }
    }
}

