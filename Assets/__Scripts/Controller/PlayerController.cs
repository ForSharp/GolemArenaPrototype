using System;
using System.Collections;
using __Scripts.CharacterEntity;
using __Scripts.CharacterEntity.State;
using __Scripts.GameLoop;
using __Scripts.Inventory.Abstracts.Spells;
using __Scripts.UI;
using UnityEngine;
using UnityEngine.AI;

namespace __Scripts.Controller
{
    public enum PlayMode
    {
        Standard,
        Cinematic,
        CastSpell
    }

    public enum ChoosingTargetMode
    {
        Enemy,
        Friend,
        All
    }

    public sealed class PlayerController : MonoBehaviour
    {
        [SerializeField] private ControllerPanel controllerPanel;
        [SerializeField] private ChampionStatsPanel standardPanel;
        [SerializeField] private GameObject autoButtonFalse;
        [SerializeField] private GameObject autoButtonTrue;
        [SerializeField] private GameObject controlMode;

        [SerializeField] private float gravity = -9.81f;
        [SerializeField] private FixedJoystick fixedJoystick;
        [SerializeField] private Camera cameraMain;

        private bool _aiControl = true;
        private PlayMode _playMode = PlayMode.Cinematic;
        private PlayMode _previousPlayMode;
        private ChampionState _state;
        private Animator _animator;
        private NavMeshAgent _agent;
        private CharacterController _controller;
        private ChampionState _currentCharacter;
        private Vector3 _velocity;
        private Vector3 _moveDirection;
        private bool _isCanMove;
        private bool _isDead;
        private float _moveSpeed;
        private ChoosingTargetMode _targetMode;
        private ISpellItem _currentSpell;
        private int _currentSpellNumb;

        public static event Action<bool> AllowAI;
        public static event Action AttackByController;
        public static event Action<PlayMode> PlayModeChanged;

        private void OnEnable()
        {
            Game.StartBattle += SetStandard;
            EventContainer.CharacterDied += CheckDeath;
            EventContainer.PlayerCharacterCreated += EnableControlMode;
        }

        private void OnDisable()
        {
            Game.StartBattle -= SetStandard;
            EventContainer.CharacterDied -= CheckDeath;
            EventContainer.PlayerCharacterCreated -= EnableControlMode;
        }

        private void CheckDeath(RoundStatistics killer)
        {
            if (_state.IsDead)
                _isDead = true;
        }

        private void SetStandard()
        {
            _playMode = PlayMode.Standard;
            _previousPlayMode = PlayMode.Standard;
            PlayModeChanged?.Invoke(PlayMode.Standard);
            SetStandardPanel();
            autoButtonFalse.SetActive(false);
            autoButtonTrue.SetActive(true);
            controllerPanel.gameObject.SetActive(false);
            _aiControl = true;
            AllowAI?.Invoke(true);
            SetComponents();
            SetNavMeshOrController();
            _isDead = false;
            OnStandardCamera();
        }

        private void SetComponents()
        {
            if (!_state)
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
            switch (_playMode)
            {
                case PlayMode.Standard:
                    HandleInput();
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
                case PlayMode.CastSpell:
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (Game.Stage != Game.GameStage.MainMenu)
                        {
                            TryChooseTarget();
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

        public void StartChoosingTarget(ChoosingTargetMode mode, ISpellItem spellItem, int spellNumb)
        {
            _playMode = PlayMode.CastSpell;
            _targetMode = mode;
            _currentSpell = spellItem;
            _currentSpellNumb = spellNumb;
        }

        private void TryChooseTarget()
        {
            if (Camera.main is null)
            {
                _currentCharacter.SpellManager.CancelCast(_currentSpell, _currentSpellNumb);
            }

            var ray = cameraMain.ScreenPointToRay(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            if (!Physics.Raycast(ray, out var hit))
            {
                _currentCharacter.SpellManager.CancelCast(_currentSpell, _currentSpellNumb);
            }

            var coll = hit.collider;

            if (coll.TryGetComponent(out CharacterState target))
            {
                switch (_targetMode)
                {
                    case ChoosingTargetMode.Enemy:
                        if (CheckEnemy(target))
                        {
                            _currentCharacter.SpellManager.CastSpell(_currentSpell, target, _currentSpellNumb);
                            _playMode = _previousPlayMode;
                            return;
                        }

                        break;
                    case ChoosingTargetMode.Friend:
                        if (CheckFriend(target))
                        {
                            _currentCharacter.SpellManager.CastSpell(_currentSpell, target, _currentSpellNumb);
                            _playMode = _previousPlayMode;
                            return;
                        }

                        break;
                    case ChoosingTargetMode.All:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            _playMode = _previousPlayMode;
            _currentCharacter.SpellManager.CancelCast(_currentSpell, _currentSpellNumb);

            bool CheckFriend(CharacterState characterState)
            {
                foreach (var friend in Game.GetFriends(_currentCharacter))
                {
                    if (friend == characterState)
                    {
                        return true;
                    }
                }

                return false;
            }

            bool CheckEnemy(CharacterState characterState)
            {
                foreach (var enemy in Game.GetEnemies(_currentCharacter))
                {
                    if (enemy == characterState)
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        private void TryShowHeroStates()
        {
            if (cameraMain is null) return;
            var ray = cameraMain.ScreenPointToRay(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            if (!Physics.Raycast(ray, out var hit)) return;
            var coll = hit.collider;
            if (coll.TryGetComponent(out ChampionState state))
            {
                standardPanel.gameObject.SetActive(true);
                standardPanel.HandleClick(state);
                _currentCharacter = state;
            }
            else if ((!standardPanel.InPanel && !_currentCharacter) || (!standardPanel.InPanel && _currentCharacter)
                     && !_currentCharacter.InventoryHelper.InventoryOrganization.InPanel &&
                     !_currentCharacter.SpellPanelHelper.SpellsPanel.InPanel)
            {
                standardPanel.gameObject.SetActive(false);
                foreach (var character in Game.AllChampionsInSession)
                {
                    character.InventoryHelper.InventoryOrganization.HideAllInventory();
                    character.SpellPanelHelper.SpellsPanel.HideAll();
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

            controllerPanel.gameObject.SetActive(true);
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

            controllerPanel.gameObject.SetActive(false);
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

        private void HandleInput()
        {
            if (CanMove())
            {
                //MoveCharacterByKeyboard();
                MoveCharacterByJoystick();
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

        public void AttackByButton()
        {
            AttackByController?.Invoke();
        }

        private void MoveCharacterByJoystick()
        {
            var moveVector = Vector3.zero;
            
            moveVector.x = fixedJoystick.Horizontal;
            moveVector.z = fixedJoystick.Vertical;
            
            moveVector = cameraMain.transform.TransformDirection(moveVector);
            moveVector.y = 0;
            
            if (moveVector.x != 0 || moveVector.z != 0)
            {
                if (Mathf.Abs(fixedJoystick.Horizontal) > 0.5 || Mathf.Abs(fixedJoystick.Vertical) > 0.5 )
                {
                    _animator.applyRootMotion = false;
                    Run();
                }
                else
                {
                    _animator.applyRootMotion = false;
                    Walk();
                }
                
            }
            else
            {
                _animator.applyRootMotion = true;
                Idle();
            }
            
            if (Vector3.Angle(Vector3.forward, moveVector) > 1 || Vector3.Angle(Vector3.forward, moveVector) == 0)
            {
                var direction = Vector3.RotateTowards(_state.transform.forward, moveVector, _moveSpeed, 0);
                _state.transform.rotation = Quaternion.LookRotation(direction);
            }
            
            moveVector *= _moveSpeed;
            _controller.Move(moveVector * Time.deltaTime);
            
            _velocity.y = -5f;
            _velocity.y += gravity * Time.deltaTime;
            _controller.Move(_velocity * Time.deltaTime);
        }

        private void MoveCharacterByKeyboard()
        {
            _velocity.y = -5f;

            var moveZ = Input.GetAxis("Vertical");
            var rotateX = Input.GetAxis("Horizontal") * 250 * Time.deltaTime;

            _moveDirection = new Vector3(0, 0, moveZ);
            _moveDirection = _state.transform.TransformDirection(_moveDirection);

            _state.transform.Rotate(Vector3.up, rotateX);

            if (_moveDirection != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
            {
                _animator.applyRootMotion = false;
                Walk();
            }
            else if (_moveDirection != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
            {
                _animator.applyRootMotion = false;
                Run();
            }
            else
            {
                _animator.applyRootMotion = true;
                Idle();
            }

            _moveDirection *= _moveSpeed;


            _controller.Move(_moveDirection * Time.deltaTime);

            _velocity.y += gravity * Time.deltaTime;
            _controller.Move(_velocity * Time.deltaTime);
        }

        private void MakeSomersault()
        {
            if (Input.GetKeyDown(KeyCode.LeftAlt))
            {
                AnimationChanger.SetSomersault(_animator);
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
            _moveSpeed = _state.Stats.moveSpeed * 1.75f;
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
                if (character != Player.PlayerCharacter && character is ChampionState champion)
                {
                    champion.InventoryHelper.InventoryOrganization.HideAllInventory();
                    champion.SpellPanelHelper.SpellsPanel.HideAll();
                }
            }

            standardPanel.SetPlayerCharacter();
            _currentCharacter = Player.PlayerCharacter;
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
                    _previousPlayMode = PlayMode.Standard;
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

                        controllerPanel.gameObject.SetActive(true);
                    }

                    SetStandardPanel();
                    OnStandardCamera();
                    PlayModeChanged?.Invoke(PlayMode.Standard);
                    break;
                case "Cinematic":
                    _playMode = PlayMode.Cinematic;
                    _previousPlayMode = PlayMode.Cinematic;
                    autoButtonFalse.SetActive(false);
                    autoButtonTrue.SetActive(false);
                    OnCinematicCamera();
                    PlayModeChanged?.Invoke(PlayMode.Cinematic);
                    _agent.enabled = true;
                    _controller.enabled = false;
                    _isCanMove = false;
                    _aiControl = true;
                    AllowAI?.Invoke(_aiControl);

                    controllerPanel.gameObject.SetActive(false);
                    break;
            }
        }

        private void EnableControlMode()
        {
            controlMode.SetActive(true);
        }

        public static event Action StandardCamera;

        public static event Action CinematicCamera;

        private static void OnStandardCamera()
        {
            StandardCamera?.Invoke();
        }

        private static void OnCinematicCamera()
        {
            CinematicCamera?.Invoke();
        }
    }
}