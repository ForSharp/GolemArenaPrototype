using System;
using GameLoop;
using UnityEngine;

namespace GolemEntity
{
    public class SoundsController : MonoBehaviour
    {
        [SerializeField] private AudioClip[] footStepClips;
        [SerializeField] private AudioClip[] attackClips;
        [SerializeField] private AudioClip[] gettingHitClips;
        [SerializeField] private AudioClip[] dyingClips;
        [SerializeField] private AudioClip[] hittingEnemyClips;
        [SerializeField] private AudioClip[] victoryClips;
        [SerializeField] private AudioClip[] clickClips;
        private AudioSource _audioSource;

        private void OnEnable()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void PlayHittingEnemySound()
        {
            //тыдыщ
        }
        
        public void OnFootStep()
        {
            //step sound
        }
        
        private void OnAttackStarted()
        {
            //attack sound
        }

        private void OnGettingHit()
        {
            //grunt pain sound
        }

        private void OnDying()
        {
            //dying sound
        }
    }
}
