using System;
using System.Collections;
using CharacterEntity;
using CharacterEntity.CharacterState;
using CharacterEntity.State;
using GameLoop;
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
        [SerializeField] private CharacterStatsPanel rtsPanel;
        [SerializeField] private GameObject autoButtonFalse;
        [SerializeField] private GameObject autoButtonTrue;
        
        [SerializeField] private float gravity = -9.81f;
        [SerializeField] private float groundDistance = 0.05f;
        [SerializeField] private LayerMask groundMask;

        private bool _aiControl = true;
        private PlayMode _playMode = PlayMode.Cinematic;
        private CharacterState _state;
        private Animator _animator;
        private NavMeshAgent _agent;
        private CharacterController _controller;
        private Vector3 _velocity;
        private Vector3 _moveDirection;
        private bool _isGrounded;
        private bool _isCanMove;
        private bool _isDead;
        private float _moveSpeed;
        
        public static event Action<bool> AllowAI;
        public static event Action AttackByController;
        
        public static event Action SpellCastByController;
        public static event Action<CharacterState> SetTargetByController;
        public static event Action<Vector3> SetMovementPointByController;
        public static event Action<PlayMode> PlayModeChanged;
        
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
            PlayModeChanged?.Invoke(PlayMode.Standard);
            SetStandardPanel();
            autoButtonFalse.SetActive(false);
            autoButtonTrue.SetActive(true);
            _aiControl = true;
            AllowAI?.Invoke(true);
            SetComponents();
            SetNavMeshOrController();
            _isDead = false;
            OnStandardCamera();
        }

        private void SetComponents()
        {
            if(!_state)
                _state = Player.PlayerCharacter;
            if (!_animator)
                _animator = _state.GetComponent<Animator>();
            if (!_agent)
                _agent = _state.GetComponent<NavMeshAgent>();
            if (!_controller)
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
            if (Input.GetKeyDown(KeyCode.K))
            {
                SpellCastByController?.Invoke();
            }
            
            
            switch (_playMode)
            {
                case PlayMode.Standard:
                    HandleJoystickInput();
                    break;
                case PlayMode.Rts:
                    if (Input.GetMouseButtonDown(1))
                    {
                        TrySetCharacterAsTargetOrMove();
                    }
                    break;
                case PlayMode.Cinematic:
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (Game.Stage != Game.GameStage.MainMenu)
                        {
                            TryShowHeroStates();
                        }
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

        private void TrySetCharacterAsTargetOrMove()
        {
            if (!_aiControl && !_isDead)
            {
                if (Camera.main is null) return;
                var ray = Camera.main.ScreenPointToRay(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
                if (!Physics.Raycast(ray, out var hit)) return;
                var coll = hit.collider;
                if (coll.TryGetComponent(out CharacterState state))
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
            if (coll.TryGetComponent(out CharacterState state))
            {
                rtsPanel.gameObject.SetActive(true);
                rtsPanel.HandleClick(state);
            }
            else if (!rtsPanel.InPanel && (state && !state.InventoryHelper.InventoryOrganization.InPanel))
            {
                rtsPanel.gameObject.SetActive(false);
                foreach (var character in Game.AllCharactersInSession)
                {
                    character.InventoryHelper.InventoryOrganization.HideAllInventory();
                }
            }
        }
        
        public void AutoButtonTrueClicked()
        {
            _aiControl = false;
            if (_playMode == PlayMode.Standard)
            {
                SetNavMeshOrController();
            }
            AllowAI?.Invoke(false);
            
            autoButtonFalse.SetActive(true);
            autoButtonTrue.SetActive(false);
        }
        
        public void AutoButtonFalseClicked()
        {
            _aiControl = true;
            if (_playMode == PlayMode.Standard)
            {
                SetNavMeshOrController();
            }
            AllowAI?.Invoke(true);

            autoButtonTrue.SetActive(true);
            autoButtonFalse.SetActive(false);
        }

        private void SetNavMeshOrController()
        {
            if (_aiControl)
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
                return !_aiControl && _isCanMove && !_isDead && _playMode == PlayMode.Standard && _controller.enabled;
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
            _moveSpeed = _state.Stats.moveSpeed;
            AnimationChanger.SetGolemWalk(_animator);
        }

        private void Run()
        {
            _moveSpeed = _state.Stats.moveSpeed * 1.5f;
            AnimationChanger.SetGolemRun(_animator);
        }

        #endregion

        private void SetStandardPanel()
        {
            StartCoroutine(SetStandardPanelAfterDelay());
        }

        private IEnumerator SetStandardPanelAfterDelay()
        {
            yield return new WaitForSeconds(0.1f);
            
            standardPanel.gameObject.SetActive(true);
            Player.PlayerCharacter.InventoryHelper.InventoryOrganization.ShowInventory();
            Player.PlayerCharacter.InventoryHelper.InventoryOrganization.HideNonEquippingSlots();
            Player.PlayerCharacter.SpellPanelHelper.SpellsPanel.ShowActiveSpells();
            foreach (var character in Game.AllCharactersInSession)
            {
                if (character != Player.PlayerCharacter)
                {
                    character.InventoryHelper.InventoryOrganization.HideAllInventory();
                }
            }
            rtsPanel.gameObject.SetActive(false);
        }

        private void SetRtsPanel()
        {
            standardPanel.gameObject.SetActive(false);
            rtsPanel.gameObject.SetActive(true);
            Player.PlayerCharacter.InventoryHelper.InventoryOrganization.ShowInventory();
            rtsPanel.HandleClick(Player.PlayerCharacter);
        }

        public void StartButtonClick()
        {
            Game.OnStartBattle();
        }

        public void SwitchMode(string mode)
        {
            if (Game.Stage == Game.GameStage.MainMenu)
                return;
            
            switch (mode)
            {
                case "Standard":
                    _playMode = PlayMode.Standard;
                    if (_aiControl)
                    {
                        autoButtonFalse.SetActive(false);
                        autoButtonTrue.SetActive(true);
                        
                        _agent.enabled = true;
                        _controller.enabled = false;
                        _isCanMove = false;
                    }
                    else
                    {
                        autoButtonFalse.SetActive(true);
                        autoButtonTrue.SetActive(false);
                        
                        _agent.enabled = false;
                        _controller.enabled = true;
                        _isCanMove = true;
                    }
                    SetStandardPanel();
                    OnStandardCamera();
                    PlayModeChanged?.Invoke(PlayMode.Standard);
                    break;
                case "Rts":
                    _playMode = PlayMode.Rts;
                    if (_aiControl)
                    {
                        autoButtonFalse.SetActive(false);
                        autoButtonTrue.SetActive(true);
                    }
                    else
                    {
                        autoButtonFalse.SetActive(true);
                        autoButtonTrue.SetActive(false);
                    }
                    SetRtsPanel();
                    OnRtsCamera();
                    PlayModeChanged?.Invoke(PlayMode.Rts);
                    _agent.enabled = true;
                    _controller.enabled = false;
                    _isCanMove = false;
                    break;
                case "Cinematic":
                    _playMode = PlayMode.Cinematic;
                    autoButtonFalse.SetActive(false);
                    autoButtonTrue.SetActive(false);
                    SetRtsPanel();
                    OnCinematicCamera();
                    PlayModeChanged?.Invoke(PlayMode.Cinematic);
                    _agent.enabled = true;
                    _controller.enabled = false;
                    _isCanMove = false;
                    _aiControl = true;
                    AllowAI?.Invoke(_aiControl);
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