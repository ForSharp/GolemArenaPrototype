using System;
using UnityEngine;

namespace __Scripts.TestScripts
{
    public class GeniusController : MonoBehaviour
    {
        public float speed = 6.0f;
        public float rotationSpeed = 60.0f;
        public float jumpValue = 8.0f;
        public float gravity = 20f;
        private Vector3 _moveDirection = Vector3.zero;
        
        private CharacterController _character;

        private void Start()
        {
            _character = GetComponent<CharacterController>();
        }

        private void Update()
        {
            if (_character.isGrounded)
            {
                _moveDirection = new Vector3(0, 0, Input.GetAxis("Vertical"));
                _moveDirection = transform.TransformDirection(_moveDirection);
                _moveDirection *= speed;
                
                transform.Rotate(Vector3.up, Input.GetAxis("Horizontal") * Time.deltaTime * rotationSpeed);

                if (Input.GetButton("Jump"))
                {
                    _moveDirection.y = jumpValue;
                }
            }

            _moveDirection.y -= gravity * Time.deltaTime;
            _character.Move(_moveDirection * Time.deltaTime);
        }

        private void MoveGenius()
        {
            transform.position += new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        }
    }
}
