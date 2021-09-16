using System;
using Fight;
using GameLoop;
using GolemEntity;
using UI;
using UnityEngine;
using UnityEngine.AI;

namespace Controller
{
    public enum PlayMode
    {
        Standard,
        Rts,
        Cinematic
    }

    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private HeroControllerPanel standardPanel;
        [SerializeField] private GolemStatsPanel rtsPanel;
        [SerializeField] private GameObject autoButton;
        
        [SerializeField] private float gravity = -9.81f;
        [SerializeField] private float groundDistance = 0.05f;
        [SerializeField] private LayerMask groundMask;

        public static PlayMode PlayMode = PlayMode.Cinematic;

        private GameCharacterState _state;
        private Animator _animator;
        private NavMeshAgent _agent;
        private CharacterController _controller;
        private Vector3 _velocity;
        private Vector3 _moveDirection;
        private bool _aiControl;
        private bool _isGrounded;
        private bool _isCanMove;
        private float _moveSpeed;

        private void Awake()
        {
            _aiControl = true;
        }

        private void OnEnable()
        {
            Game.StartBattle += SetStandard;
        }

        private void OnDisable()
        {
            Game.StartBattle -= SetStandard;
        }

        private void SetStandard()
        {
            PlayMode = PlayMode.Standard;
            SetStandardPanel();
            autoButton.SetActive(true);
            _state = Player.PlayerCharacter;
            _animator = _state.GetComponent<Animator>();
            _agent = _state.GetComponent<NavMeshAgent>();
            _controller = _state.GetComponent<CharacterController>();
        }
        
        #region AnimationEvents

        private void OnAttackStarted()
        {
            _isCanMove = false;
        }

        private void OnAttackEnded()
        {
            _isCanMove = true;
        }

        private void OnStartJump()
        {
            _isCanMove = false;
        }
        
        private void OnEndJump()
        {
            _isCanMove = true;
        }
        
        private void OnCanFight()
        {
            _isCanMove = true;
        }

        #endregion
        
        private void Update()
        {
            switch (PlayMode)
            {
                case PlayMode.Standard:
                    HandleJoystickInput();
                    break;
                case PlayMode.Rts:
                    break;
                case PlayMode.Cinematic:
                    if (Input.GetMouseButtonDown(0))
                    {
                        TryShowHeroStates();
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            if (CanShowMainMenu())
            {
                Game.Stage = Game.GameStage.MainMenu;
                Game.OnOpenMainMenu();
            }

            bool CanShowMainMenu()
            {
                return Input.GetKeyDown(KeyCode.Escape) && Game.Stage != Game.GameStage.MainMenu;
            }
        }

        private void TryShowHeroStates()
        {
            var ray = Camera.main.ScreenPointToRay(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                var coll = hit.collider;
                if (coll.TryGetComponent(out GameCharacterState state))
                {
                    rtsPanel.gameObject.SetActive(true);
                    rtsPanel.HandleClick(state);
                }
                else if (!rtsPanel.inPanel)
                {
                    rtsPanel.gameObject.SetActive(false);
                    CameraMovement.Instance.SetDefaultTargetChanging();
                }
            }
        }

        public static event Action<bool> AllowAI;
        
        public void AutoButtonClicked()
        {
            _aiControl = !_aiControl;
            AllowAI?.Invoke(_aiControl);

            SetNavMeshOrController();
        }

        private void SetNavMeshOrController()
        {
            if (_aiControl)
            {
                _agent.enabled = true;
                _controller.enabled = false;
            }
            else
            {
                _agent.enabled = false;
                _controller.enabled = true;
            }
        }

        public void SwitchMode(string mode)
        {
            switch (mode)
            {
                case "Standard":
                    PlayMode = PlayMode.Standard;
                    autoButton.SetActive(true);
                    SetStandardPanel();
                    break;
                case "Rts":
                    PlayMode = PlayMode.Rts;
                    autoButton.SetActive(true);
                    SetRtsPanel();
                    break;
                case "Cinematic":
                    PlayMode = PlayMode.Cinematic;
                    autoButton.SetActive(false);
                    SetRtsPanel();
                    break;
            }
        }

        private void HandleJoystickInput()
        {
            if (!_aiControl && _isCanMove)
            {
                MoveCharacter();
                MakeSomersault();
            }
        }

        private void MoveCharacter()
        {
            _isGrounded = Physics.CheckSphere(_state.transform.position, groundDistance, groundMask);

            if (_isGrounded && _velocity.y < 0)
            {
                _velocity.y = -2f;
            }
            
            float moveZ = Input.GetAxis("Vertical");
            float rotateX = Input.GetAxis("Horizontal") * 250 * Time.deltaTime;

            _moveDirection = new Vector3(0, 0, moveZ);
            _moveDirection = _state.transform.TransformDirection(_moveDirection);

            if (_isGrounded)
            {
                _state.transform.Rotate(Vector3.up, rotateX);
                
                if (_moveDirection != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
                {
                    print("walk");
                    Walk();
                }
                else if (_moveDirection != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
                {
                    print("run");
                    Run();
                }
                else
                {
                    Idle();
                }

                _moveDirection *= _moveSpeed;

                
            }
            
            _controller.Move(_moveDirection * Time.deltaTime);

            _velocity.y += gravity * Time.deltaTime;
            _controller.Move(_velocity * Time.deltaTime);
        }

        private void MakeSomersault()
        {
            if (_isGrounded)
            {
                if (Input.GetKeyDown(KeyCode.LeftAlt))
                {
                    AnimationChanger.SetSomersault(_animator);
                }
            }
        }
        
        private void Idle()
        {
            AnimationChanger.SetFightIdle(_animator);
        }

        private void Walk()
        {
            print("walk anim");
            _moveSpeed = _state.Stats.MoveSpeed;
            AnimationChanger.SetGolemWalk(_animator);
        }

        private void Run()
        {
            print("run anim");
            _moveSpeed = _state.Stats.MoveSpeed * 2;
            AnimationChanger.SetGolemRun(_animator);
        }
        
        private void SetStandardPanel()
        {
            standardPanel.gameObject.SetActive(true);
            rtsPanel.gameObject.SetActive(false);
        }

        private void SetRtsPanel()
        {
            standardPanel.gameObject.SetActive(false);
            rtsPanel.gameObject.SetActive(true);
            rtsPanel.HandleClick(Player.PlayerCharacter);
        }
        
        public void StartButtonClick()
        {
            if (Game.Stage != Game.GameStage.Battle)
            {
                Game.Stage = Game.GameStage.Battle;
                Game.OnStartBattle();
            }
        }
    }
}