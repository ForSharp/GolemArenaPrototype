using System;
using UnityEngine;

namespace Controller
{
    public class StateController : MonoBehaviour
    {
        [SerializeField] private float _acceleration = 2;
        [SerializeField] private float _deceleration = 2;
        private Animator _animator;
        private float _velocityZ = 0f;
        private float _velocityX = 0f;
        private float _maximumWalkVelocity = 0.5f;
        private float _maximumRunVelocity = 1f;

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            bool forwardPressed = Input.GetKey("w");
            bool leftPressed = Input.GetKey("a");
            bool rightPressed = Input.GetKey("d");
            bool runPressed = Input.GetKey("left shift");

            float currentMaxVelocity = runPressed ? _maximumRunVelocity : _maximumWalkVelocity;
            
            if (forwardPressed && _velocityZ < currentMaxVelocity)
            {
                _velocityZ += Time.deltaTime * _acceleration;
            }

            if (leftPressed && _velocityX > -currentMaxVelocity)
            {
                _velocityX -= Time.deltaTime * _acceleration;
            }

            if (rightPressed && _velocityX < currentMaxVelocity)
            {
                _velocityX += Time.deltaTime * _acceleration;
            }

            if (!forwardPressed && _velocityZ > 0)
            {
                _velocityZ -= Time.deltaTime * _deceleration;
            }

            if (!forwardPressed && _velocityZ < 0)
            {
                _velocityZ = 0;
            }

            if (!leftPressed && _velocityX < 0)
            {
                _velocityX += Time.deltaTime * _deceleration;
            }
            
            if (!rightPressed && _velocityX > 0)
            {
                _velocityX -= Time.deltaTime * _deceleration;
            }

            if (!leftPressed && !rightPressed && _velocityX != 0 && (_velocityX > 0.05f && _velocityX < 0.05f))
            {
                _velocityX = 0;
            }
            
            _animator.SetFloat("Forward", _velocityZ);
            _animator.SetFloat("Turn", _velocityX);
        }
    }
}
