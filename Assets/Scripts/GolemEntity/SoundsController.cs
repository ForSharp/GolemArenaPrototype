using System;
using GameLoop;
using UnityEngine;
using Random = UnityEngine.Random;

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
            _audioSource.PlayOneShot(hittingEnemyClips[Random.Range(0, hittingEnemyClips.Length)]);
        }
        
        #region AnimationEvents
        public void OnFootStep()
        {
            _audioSource.PlayOneShot(footStepClips[Random.Range(0, footStepClips.Length)]);
        }
        
        public void OnAttackStarted()
        {
            _audioSource.PlayOneShot(attackClips[Random.Range(0, attackClips.Length)]);
        }

        public void OnGettingHit()
        {
            _audioSource.PlayOneShot(gettingHitClips[Random.Range(0, gettingHitClips.Length)]);
        }

        public void OnDying()
        {
            _audioSource.PlayOneShot(dyingClips[Random.Range(0, dyingClips.Length)]);
        }
        #endregion
    }
}
