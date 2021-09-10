using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Environment
{
    public class Crowd : MonoBehaviour
    {
        [SerializeField] private GameObject[] spectatorPrefab;
        [SerializeField] private Transform[] standingSpectatorPlaces;
        [SerializeField] private Transform[] sittingSpectatorPlaces;
        [SerializeField] private Transform target;

        [Header("Set Dynamically")] 
        private readonly List<GameObject> _standingSpectators = new List<GameObject>();
        private readonly List<GameObject> _sittingSpectators = new List<GameObject>();
        
        private static readonly int StandingCrowd = Animator.StringToHash("StandingCrowd");
        private static readonly int SittingIdle = Animator.StringToHash("SittingIdle");
        private static readonly int SittingCrowd = Animator.StringToHash("SittingCrowd");
        private static readonly int SitStandingCrowd = Animator.StringToHash("SitStandingCrowd");

        private const int StandAnimationAmount = 21;
        private const int SittingIdleAnimationAmount = 5;
        private const int SittingCrowdAnimationAmount = 19;
        private const int SitStandSitAnimationAmount = 10;

        private void Start()
        {
            InstantiateSittingSpectators();
            InstantiateStandingSpectators();
        }

        private void InstantiateStandingSpectators()
        {
            for (var i = 0; i < standingSpectatorPlaces.Length; i++)
            {
                var spectator = Instantiate(spectatorPrefab[Random.Range(0, spectatorPrefab.Length)], 
                    standingSpectatorPlaces[i].position, Quaternion.identity, transform);
                _standingSpectators.Add(spectator);
                var animator = spectator.GetComponent<Animator>();
                animator.Play("StandTree");
                animator.SetFloat(StandingCrowd, Random.Range(0, StandAnimationAmount));
                animator.speed = Random.Range(0.75f, 1.25f);
                
                SetSpectatorRotation(spectator);
            }
        }

        private void InstantiateSittingSpectators()
        {
            for (var i = 0; i < sittingSpectatorPlaces.Length; i++)
            {
                var spectator = Instantiate(spectatorPrefab[Random.Range(0, spectatorPrefab.Length)],
                    sittingSpectatorPlaces[i].position, Quaternion.identity, transform);
                _sittingSpectators.Add(spectator);
                var animator = spectator.GetComponent<Animator>();
                animator.SetFloat(SittingIdle, Random.Range(0, SittingIdleAnimationAmount));
                animator.speed = Random.Range(0.75f, 1.25f);
                
                SetSpectatorRotation(spectator);
            }
        }

        private void SetSpectatorRotation(GameObject spectator)
        {
            spectator.transform.LookAt(target);
            var rot = spectator.transform.rotation;
            spectator.transform.rotation = new Quaternion(0, rot.y, 0, rot.w);
        }
    }
}
