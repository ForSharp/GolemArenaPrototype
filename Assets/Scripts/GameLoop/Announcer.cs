using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameLoop
{
    public class Announcer : MonoBehaviour
    {
        [SerializeField] private AudioClip[] oneKillClips;
        [SerializeField] private AudioClip[] doubleKillClips;
        [SerializeField] private AudioClip[] tripleKillClips;
        [SerializeField] private AudioClip rampageClip;
        [SerializeField] private AudioClip fightClip;
        [SerializeField] private AudioSource audioSource;

        private void OnEnable()
        {
            EventContainer.GolemDied += AnnounceKilling;
            Game.StartBattle += StartFight;
        }

        private void OnDisable()
        {
            EventContainer.GolemDied -= AnnounceKilling;
            Game.StartBattle -= StartFight;
        }

        private void StartFight()
        {
            audioSource.PlayOneShot(fightClip);
        }

        private void AnnounceKilling(RoundStatistics killer)
        {
            switch (killer.Kills)
            {
                case 1:
                    audioSource.PlayOneShot(oneKillClips[Random.Range(0, oneKillClips.Length)]);
                    break;
                case 2:
                    audioSource.PlayOneShot(doubleKillClips[Random.Range(0, doubleKillClips.Length)]);
                    break;
                case 3:
                    audioSource.PlayOneShot(tripleKillClips[Random.Range(0, tripleKillClips.Length)]);
                    break;
                default:
                    audioSource.PlayOneShot(rampageClip);
                    break;
            }
        }
    }
}
