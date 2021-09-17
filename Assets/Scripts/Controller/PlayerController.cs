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

    public sealed class PlayerController : MonoBehaviour
    {
        [SerializeField] private HeroControllerPanel standardPanel;
        [SerializeField] private GolemStatsPanel rtsPanel;
        [SerializeField] private GameObject autoButton;
        
        [SerializeField] private float gravity = -9.81f;
        [SerializeField] private float groundDistance = 0.05f;
        [SerializeField] private LayerMask groundMask;

        private static bool AIControl { get; set; }
        
        private static PlayMode _playMode = PlayMode.Cinematic;
        private GameCharacterState _state;
        private Animator _animator;
        private NavMeshAgent _agent;
        private CharacterController _controller;
        private Vector3 _velocity;
        private Vector3 _moveDirection;
        private bool _isGrounded;
        private bool _isCanMove;
        private bool _isDead;
        private float _moveSpeed;

        private void Awake()
        {
            AIControl = true;
        }

        private void OnEnable()
        {
            Game.StartBattle += SetStandard;
            EventContainer.GolemDied += CheckDeath;
        }

        private void OnDisable()
        {
            Game.StartBattle -= SetStandard;
            EventContainer.GolemDied -= CheckDeath;
        }

        private void CheckDeath(RoundStatistics killer)
        {
            if (_state.IsDead)
                _isDead = true;
        }
        
        private void SetStandard()
        {
            _playMode = PlayMode.Standard;
            SetStandardPanel();
            autoButton.SetActive(true);
            _state = Player.PlayerCharacter;
            _animator = _state.GetComponent<Animator>();
            _agent = _state.GetComponent<NavMeshAgent>();
            _controller = _state.GetComponent<CharacterController>();
            _isDead = false;
            OnStandardCamera();
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
            switch (_playMode)
            {
                case PlayMode.Standard:
                    HandleJoystickInput();
                    break;
                case PlayMode.Rts:
                    if (Input.GetMouseButtonDown(1))
                    {
                        TryAimCharacter();
                    }
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

        private void TryAimCharacter()
        {
            if (!AIControl && !_isDead)
            {
                if (Camera.main is null) return;
                var ray = Camera.main.ScreenPointToRay(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
                if (!Physics.Raycast(ray, out var hit)) return;
                var coll = hit.collider;
                if (coll.TryGetComponent(out GameCharacterState state))
                {
                    if (state == _state)
                    {
                        state.SoundsController.PlayClickAndVictorySound();
                    }
                    else
                    {
                        SetTargetByController?.Invoke(state);
                    }
                }
                else
                {
                    SetMovementPointByController?.Invoke(hit.point);
                }
            }
        }
        
        private void TryShowHeroStates()
        {
            if (Camera.main is null) return;
            var ray = Camera.main.ScreenPointToRay(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            if (!Physics.Raycast(ray, out var hit)) return;
            var coll = hit.collider;
            if (coll.TryGetComponent(out GameCharacterState state))
            {
                rtsPanel.gameObject.SetActive(true);
                rtsPanel.HandleClick(state);
            }
            else if (!rtsPanel.inPanel)
            {
                rtsPanel.gameObject.SetActive(false);
            }
        }

        public static event Action<bool> AllowAI;
        public static event Action AttackByController;
        public static event Action<GameCharacterState> SetTargetByController;
        public static event Action<Vector3> SetMovementPointByController;
        
        public void AutoButtonClicked()
        {
            AIControl = !AIControl;
            AllowAI?.Invoke(AIControl);

            SetNavMeshOrController();
        }

        private void SetNavMeshOrController()
        {
            if (AIControl)
            {
                _agent.enabled = true;
                _controller.enabled = false;
                _isCanMove = false;
            }
            else
            {
                _agent.enabled = false;
                _controller.enabled = true;
                _isCanMove = true;
            }
        }

        #region StandardController

        private void HandleJoystickInput()
        {
            if (CanMove())
            {
                MoveCharacter();
                MakeSomersault();
                Attack();
            }

            bool CanMove()
            {
                return !AIControl && _isCanMove && !_isDead && _playMode == PlayMode.Standard;
            }
        }

        private void Attack()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                AttackByController?.Invoke();
            }
        }

        private void MoveCharacter()
        {
            _isGrounded = Physics.CheckSphere(_state.transform.position, groundDistance, groundMask);

            if (_isGrounded && _velocity.y < 0)
            {
                _velocity.y = -2f;
            }
            
            var moveZ = Input.GetAxis("Vertical");
            var rotateX = Input.GetAxis("Horizontal") * 250 * Time.deltaTime;

            _moveDirection = new Vector3(0, 0, moveZ);
            _moveDirection = _state.transform.TransformDirection(_moveDirection);

            if (_isGrounded)
            {
                _state.transform.Rotate(Vector3.up, rotateX);
                
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
            _moveSpeed = _state.Stats.MoveSpeed;
            AnimationChanger.SetGolemWalk(_animator);
        }

        private void Run()
        {
            _moveSpeed = _state.Stats.MoveSpeed * 1.5f;
            AnimationChanger.SetGolemRun(_animator);
        }

        #endregion

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
            Game.OnStartBattle();
        }

        public void SwitchMode(string mode)
        {
            switch (mode)
            {
                case "Standard":
                    _playMode = PlayMode.Standard;
                    autoButton.SetActive(true);
                    SetStandardPanel();
                    OnStandardCamera();
                    break;
                case "Rts":
                    _playMode = PlayMode.Rts;
                    autoButton.SetActive(true);
                    SetRtsPanel();
                    OnRtsCamera();
                    break;
                case "Cinematic":
                    _playMode = PlayMode.Cinematic;
                    autoButton.SetActive(false);
                    SetRtsPanel();
                    OnCinematicCamera();
                    break;
            }
        }

        public static event Action StandardCamera;
        public static event Action RtsCamera;
        public static event Action CinematicCamera;

        private static void OnStandardCamera()
        {
            StandardCamera?.Invoke();
        }

        private static void OnRtsCamera()
        {
            RtsCamera?.Invoke();
        }

        private static void OnCinematicCamera()
        {
            CinematicCamera?.Invoke();
        }
    }
}