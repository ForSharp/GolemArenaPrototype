using UnityEngine;

namespace Scripts
{
    public class CyborgController : MonoBehaviour
    {
        [Header("Set in Inspector")]
        [SerializeField] private float walkSpeed;
        [SerializeField] private float runSpeed;

        [SerializeField] private LayerMask groundMask;
        [SerializeField] private float gravity;
        [SerializeField] private float groundDistance;
        [SerializeField] private float jumpHeight;
        
        [Header("SetDynamically")]
        [SerializeField] private bool isGrounded;

        //[HideInInspector] public bool isMoving = false;
        
        private Vector3 _moveDirection;
        private Vector3 _velocity;
        private float _moveSpeed;

        private CharacterController _controller;
        private Animator _animator;
        private static readonly int Blend = Animator.StringToHash("Blend");

        private void Start()
        {
            _controller = GetComponent<CharacterController>();
            _animator = GetComponentInChildren<Animator>();
        }

        private void Update()
        {
            MoveCharacter();

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Press();
            }
        }

        private void MoveCharacter()
        {
            isGrounded = Physics.CheckSphere(transform.position, groundDistance, groundMask);

            if (isGrounded && _velocity.y < 0)
            {
                _velocity.y = -2f;
            }
            
            float moveZ = Input.GetAxis("Vertical");

            _moveDirection = new Vector3(0, 0, moveZ);
            _moveDirection = transform.TransformDirection(_moveDirection);

            if (isGrounded)
            {
                if (_moveDirection != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
                {
                    Walk();
                }
                else if (_moveDirection != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
                {
                    Run();
                }
                else
                {
                    Idle();
                }

                _moveDirection *= _moveSpeed;

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Jump();
                }
            }
            
            _controller.Move(_moveDirection * Time.deltaTime);

            _velocity.y += gravity * Time.deltaTime;
            _controller.Move(_velocity * Time.deltaTime);
        }

        private void Idle()
        {
            _animator.SetFloat(Blend, 0, 0.1f, Time.deltaTime);
        }

        private void Walk()
        {
            _moveSpeed = walkSpeed;
            _animator.SetFloat(Blend, 0.5f, 0.1f, Time.deltaTime);
        }

        private void Run()
        {
            _moveSpeed = runSpeed;
            _animator.SetFloat(Blend, 1, 0.1f, Time.deltaTime);
        }

        private void Jump()
        {
            _velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
            //_animator.SetFloat(Blend, 1, 0.1f, Time.deltaTime);
            _animator.SetTrigger("Jump");
        }

        private void Press()
        {
            _animator.SetTrigger("Press");
        }
        
    }
}
