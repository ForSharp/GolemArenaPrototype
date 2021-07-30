using UnityEngine;
using Random = UnityEngine.Random;

namespace GameLoop
{
    public class CalmIdleSetter : MonoBehaviour
    {
        private Animator _animator;
        private static readonly int Blend = Animator.StringToHash("Blend");

        private void Start()
        {
            _animator = GetComponent<Animator>();
            SetRandomCalmAnimation();
        }

        public void SetRandomCalmAnimation()
        {
            _animator.SetFloat(Blend, Random.Range(0, 21));
        }
    }
}
