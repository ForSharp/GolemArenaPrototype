using __Scripts.GameLoop;
using UnityEngine;
using Random = UnityEngine.Random;

namespace __Scripts.Environment
{
    public class Announcer : MonoBehaviour
    {
        [SerializeField] private AudioClip[] oneKillClips;
        [SerializeField] private AudioClip[] doubleKillClips;
        [SerializeField] private AudioClip[] tripleKillClips;
        [SerializeField] private AudioClip rampageClip;
        [SerializeField] private AudioClip fightClip;
        [SerializeField] private AudioClip endGame;
        [SerializeField] private AudioSource audioSource;

        private void OnEnable()
        {
            EventContainer.CharacterDied += AnnounceKilling;
            Game.StartBattle += StartFight;
            Game.EndGame += EndGame;
        }

        private void OnDisable()
        {
            EventContainer.CharacterDied -= AnnounceKilling;
            Game.StartBattle -= StartFight;
            Game.EndGame -= EndGame;
        }

        private void StartFight()
        {
            audioSource.PlayOneShot(fightClip);
        }

        private void EndGame()
        {
            audioSource.PlayOneShot(endGame);
        }

        private void AnnounceKilling(RoundStatistics killer)
        {
            if (killer == null)
                return;
            
            switch (killer.RoundKills)
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
