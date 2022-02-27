using System;
using System.Collections.Generic;
using GameLoop;
using UnityEngine;

namespace CharacterEntity.State
{
    public class CharacterEffectsContainer : MonoBehaviour
    {
        [SerializeField] private ParticleSystem lvlUpEffectPrefab;
        [SerializeField] private ParticleSystem targetEnemyEffectPrefab;
        [SerializeField] private ParticleSystem targetFriendEffectPrefab;

        private ParticleSystem _lvlUpEffect;
        private ParticleSystem _targetEnemyEffect;
        private ParticleSystem _targetFriendEffect;
        
        private readonly List<ParticleSystem> _effects = new List<ParticleSystem>();
        
        private void Start()
        {
            CreateEffects();
            StopAllEffect();
        }

        private void OnEnable()
        {
            Game.ClearEffects += StopAllEffect;
        }

        private void OnDisable()
        {
            Game.ClearEffects -= StopAllEffect;
        }

        private void CreateEffects()
        {
            _lvlUpEffect = Instantiate(lvlUpEffectPrefab, gameObject.transform).GetComponent<ParticleSystem>();
            _targetEnemyEffect = Instantiate(targetEnemyEffectPrefab, gameObject.transform).GetComponent<ParticleSystem>();
            _targetFriendEffect = Instantiate(targetFriendEffectPrefab, gameObject.transform).GetComponent<ParticleSystem>();
            FillList();
        }
        
        private void FillList()
        {
            _effects.Add(_lvlUpEffect);
            _effects.Add(_targetEnemyEffect);
            _effects.Add(_targetFriendEffect);
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
            _targetEnemyEffect.Play();
        }

        public void StopPlayingTargetEnemy()
        {
            _targetEnemyEffect.Stop();
        }
        
        public void PlayTargetFriend()
        {
            _targetFriendEffect.Play();
        }
        
        public void StopPlayingTargetFriend()
        {
            _targetFriendEffect.Stop();
        }
        
        public void PlayLvlUpEffect()
        {
            _lvlUpEffect.Play();
        }
    }
}
