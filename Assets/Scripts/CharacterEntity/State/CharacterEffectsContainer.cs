using System;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterEntity.State
{
    public class CharacterEffectsContainer : MonoBehaviour
    {
        //партикл эффекты должны быть дочерними к префабу персонажа, этот скрипт должен висеть на каждом префабе персонажа
        [SerializeField] private ParticleSystem lvlUpEffect;
        [SerializeField] private ParticleSystem targetEnemyEffect;
        [SerializeField] private ParticleSystem targetFriendEffect;

        private readonly List<ParticleSystem> _effects;
        
        private void Start()
        {
            FillList();
            StopAllEffect();
        }

        private void FillList()
        {
            _effects.Add(lvlUpEffect);
            _effects.Add(targetEnemyEffect);
            _effects.Add(targetFriendEffect);
        }

        private void StopAllEffect()
        {
            foreach (var effect in _effects)
            {
                effect.Stop();
            }
        }

        public void PlayTargetEnemy()
        {
            targetEnemyEffect.Play();
        }
        
        public void PlayTargetFriend()
        {
            targetFriendEffect.Play();
        }
        
        public void PlayLvlUpEffect()
        {
            lvlUpEffect.Play();
        }
    }
}
