using System;
using UnityEngine;

namespace __Scripts
{
    public class ExtraStatsCalculator : MonoBehaviour
    {
        private float _strength;
        private float _agility;
        private float _intelligence;

        public ExtraStatsCalculator(GolemBaseStats stats)
        {
            _strength = stats.Strength;
            _agility = stats.Agility;
            _intelligence = stats.Intelligence;
        }

        public float GetHealth()
        {
            return (_strength * 0.8f + _agility * 0.1f + _intelligence * 0.1f) * 10;
        }

        // public float GetStamina()
        // {
        //     
        // }
    }
}
