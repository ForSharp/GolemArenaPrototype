using UnityEngine;
using Random = UnityEngine.Random;

namespace __Scripts.Environment
{
    public class DeathEffect : MonoBehaviour
    {
        [SerializeField] private GameObject[] deathFX;
        public static DeathEffect Instatnce { get; private set; }

        private void Awake()
        {
            Instatnce = this;
        }

        public void CreateDeathEffect(Vector3 pos)
        {
            var effect = Instantiate(deathFX[Random.Range(0, deathFX.Length)], pos, Quaternion.identity);
            Destroy(effect,3);
        }
    }
}
