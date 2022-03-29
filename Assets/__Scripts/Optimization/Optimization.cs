using System.Collections;
using UnityEngine;

namespace __Scripts.Optimization
{
    public class Optimization : MonoBehaviour
    {
        private Renderer _renderer;
        private Rigidbody _rigidbody;
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _renderer = GetComponent<Renderer>();
        
            _rigidbody.Sleep();
        }

        void Update()
        {
            if (transform.position.y <= -50)
            {
                Destroy(gameObject);
            }
        }

        public IEnumerator ShowDamage()
        {
            var colorCurrent = _renderer.material.color;
            _renderer.material.color = Color.red;
            yield return new WaitForSeconds(1f);
            _renderer.material.color = colorCurrent;
        }
    }
}
