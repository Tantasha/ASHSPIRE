using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace WorldTime
{
    [RequireComponent(typeof(Light2D))]
    public class WorldLight : MonoBehaviour
    {
<<<<<<< HEAD
        private Light2D _light;

        [SerializeField]
        private WorldTime _worldTime;

        [SerializeField]
        private Gradient _gradient;

        private void Awake()
        {
            _light = GetComponent<Light2D>();
            _worldTime.WorldTimeChanged += OnWorldTimeChanged;
=======
        private Light2D _light;//reference to Light2D component controlling in-game lighting

        [SerializeField]
        private WorldTime _worldTime;//reference to WorldTime script

        [SerializeField]
        private Gradient _gradient;//used to define colour transitions

        private void Awake()
        {
            _light = GetComponent<Light2D>();//get Light2D component attached to GameObject
            _worldTime.WorldTimeChanged += OnWorldTimeChanged;//notify WorldTime whenever in-game time changes
>>>>>>> AccountLink
        }

        private void OnDestroy()
        {
<<<<<<< HEAD
=======
            //prevent memory leaks
>>>>>>> AccountLink
            _worldTime.WorldTimeChanged -= OnWorldTimeChanged;
        }

        private void OnWorldTimeChanged(object sender, TimeSpan newTime)
        {
<<<<<<< HEAD
=======
            //Evaluate gradient percentage of a day and update Light2D colour accordingly
>>>>>>> AccountLink
            _light.color = _gradient.Evaluate(time: PercentageOfDay(newTime));
        }

        private float PercentageOfDay(TimeSpan timeSpan)
        {
<<<<<<< HEAD
=======
            //ensures it wraps around correctly after 24 hours
>>>>>>> AccountLink
            return (float) timeSpan.TotalMinutes % WorldTimeConstant.MinutesInDay / WorldTimeConstant.MinutesInDay;
        }
    }
}

