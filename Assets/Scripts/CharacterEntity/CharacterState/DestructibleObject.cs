﻿using GameLoop;
using UnityEngine;

namespace CharacterEntity.CharacterState
{
    public class DestructibleObject : MonoBehaviour, IDestructible, IBurnable
    {
        public bool IsDead { get; private set; } 
    
        public void TakeDamage(float damage, RoundStatistics statistics)
        {
            
        }

        public void Burn(float duration, float periodicDamage, int intensity)
        {
            
        }
    }
}
