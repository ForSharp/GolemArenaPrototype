using System;
using System.Collections.Generic;
using GameLoop;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Environment
{
    public enum CrowdIntensity
    {
        High,
        Medium,
        Low,
        VeryLow
    }
    
    public class Crowd : MonoBehaviour
    {
        [SerializeField] private GameObject[] spectatorPrefab;
        [SerializeField] private Transform[] standingSpectatorPlaces;
        [SerializeField] private Transform[] sittingSpectatorPlaces;
        [SerializeField] private Transform[] staticSittingSpectatorPlaces;
        [SerializeField] private Transform target;
        [SerializeField] private CrowdIntensity crowdIntensity;

        [Header("Not Optimized")] 
        [SerializeField] private bool fullFilling;
        
        private readonly List<GameObject> _standingSpectators = new List<GameObject>();
        private readonly List<GameObject> _sittingSpectators = new List<GameObject>();
        
        private static readonly int StandingCrowd = Animator.StringToHash("StandingCrowd");
        private static readonly int SittingIdle = Animator.StringToHash("SittingIdle");
        private static readonly int SittingCrowd = Animator.StringToHash("SittingCrowd");
        private static readonly int SitStandingCrowd = Animator.StringToHash("SitStandingCrowd");
        private static readonly int Sit = Animator.StringToHash("Sit");
        private static readonly int SitStandSit = Animator.StringToHash("SitStandSit");

        private const int StandAnimationAmount = 21;
        private const int SittingIdleAnimationAmount = 5;
        private const int SittingCrowdAnimationAmount = 19;
        private const int SitStandSitAnimationAmount = 10;

        private void Start()
        {
            InstantiateSittingSpectators();
            InstantiateStandingSpectators();

            if (fullFilling)
            {
                InstantiateStaticSittingSpectators();
            }
        }

        private void OnEnable()
        {
            EventContainer.CharacterDied += SetHeroDiedReaction;
            Game.StartBattle += SetStartRoundReaction;
        }

        private void OnDisable()
        {
            EventContainer.CharacterDied -= SetHeroDiedReaction;
            Game.StartBattle -= SetStartRoundReaction;
        }

        private void SetHeroDiedReaction(RoundStatistics statistics)
        {
            foreach (var spectator in _sittingSpectators)
            {
                var chance = Random.Range(0, 1.0f);
                if (chance > 0.5f)
                {
                    SetRandomSittingAnimation(spectator);
                }
            }
        }

        private void SetStartRoundReaction()
        {
            foreach (var spectator in _sittingSpectators)
            {
                var chance = Random.Range(0, 1.0f);
                if (chance > 0.5f)
                {
                    SetRandomSitStandingAnimation(spectator);
                }
            }
        }

        private static void SetRandomSittingAnimation(GameObject spectator)
        {
            var animator = spectator.GetComponent<Animator>();
            animator.SetTrigger(Sit);
            animator.SetFloat(SittingCrowd, Random.Range(0, SittingCrowdAnimationAmount));
        }

        private static void SetRandomSitStandingAnimation(GameObject spectator)
        {
            var animator = spectator.GetComponent<Animator>();
            animator.SetTrigger(SitStandSit);
            animator.SetFloat(SitStandingCrowd, Random.Range(0, SitStandSitAnimationAmount));
        }
        
        private void InstantiateStandingSpectators()
        {
            for (var i = 0; i < standingSpectatorPlaces.Length; i++)
            {
                if (CanContinue())
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
        }

        private void InstantiateSittingSpectators()
        {
            for (var i = 0; i < sittingSpectatorPlaces.Length; i++)
            {
                if (CanContinue())
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
        }

        private void InstantiateStaticSittingSpectators()
        {
            for (var i = 0; i < staticSittingSpectatorPlaces.Length; i++)
            {
                if (CanContinue())
                {
                    var spectator = Instantiate(spectatorPrefab[Random.Range(0, spectatorPrefab.Length)],
                        staticSittingSpectatorPlaces[i].position, Quaternion.identity, transform);
                    var animator = spectator.GetComponent<Animator>();
                    animator.Play("SittingPose");
                    SetSpectatorRotation(spectator);
                }
            }
        }

        private void SetSpectatorRotation(GameObject spectator)
        {
            spectator.transform.LookAt(target);
            var rot = spectator.transform.rotation;
            spectator.transform.rotation = new Quaternion(0, rot.y, 0, rot.w);
        }

        private bool CanContinue()
        {
            var chance = Random.Range(0, 1.0f);
            
            switch (crowdIntensity)
            {
                case CrowdIntensity.High:
                    return true;
                case CrowdIntensity.Medium:
                    if (chance > 0.5f)
                        return true;
                    return false;
                case CrowdIntensity.Low:
                    if (chance > 0.75f)
                        return true;
                    return false;
                case CrowdIntensity.VeryLow:
                    if (chance > 0.9f)
                        return true;
                    return false;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
