using System;
using System.Collections;
using System.Linq;
using BehaviourStrategy;
using BehaviourStrategy.Abstracts;
using Controller;
using Environment;
using FightState;
using GameLoop;
using Inventory;
using UnityEngine;
using UnityEngine.AI;
using PlayMode = Controller.PlayMode;
using Random = UnityEngine.Random;

namespace GolemEntity
{
    public class GolemAI : MonoBehaviour
    {
        //компонент, который весит на герое 0
        private FireballSpell _fireballSpell; //вынести туда, где вся логика работы со спеллами

        private bool _isAIControlAllowed = true;
        private bool _playerAI = true;
        private bool _isIKAllowed;
        private bool _isDies;
        private bool _inAttack;
        private bool _isGetsHit;
        private bool _isAvoidsHit;
        private bool _isWin;

        private IMoveable _moveable;
        private IAttackable _attackable;
        private ICastable _spellCast;

        private GameCharacterState _thisState;
        private GameCharacterState _targetState;
        private CommonMeleeAttackBehaviour _attack;
        private NavMeshAgent _navMeshAgent;
        private Animator _animator;
        private FightStatus _status;
        private float _timeToResetAttack;
        private SoundsController _soundsController;
        private Vector3 _targetPoint;
        private PlayMode _playMode = PlayMode.Cinematic;

        private const float CloseDistance = 20;
        private const float HitHeight = 0.75f;
        private const float DestructionRadius = 0.35f;
        private const int AutoResetTargetDelay = 30;

        private void Start()
        {
            _thisState = GetComponent<GameCharacterState>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
            _attack = GetComponent<CommonMeleeAttackBehaviour>();

            _status = FightStatus.Neutral;
            AnimationChanger.SetFightIdle(_animator, true);
            StartCoroutine(FindEnemies());
            _isDies = false;
            _soundsController = GetComponent<SoundsController>();

            if (_thisState == Player.PlayerCharacter)
            {
                _fireballSpell = GetComponent<FireballSpell>();
            }
        }

        private void Update()
        {
            SwitchStatuses();
            ResetAttackIfNeed();

            if (CanFight() && _targetState)
            {
                _status = FightStatus.Active;
            }
            else if (CanFight() && !_targetState)
            {
                _status = FightStatus.Neutral;
            }
        }

        private void OnEnable()
        {
            Game.StartBattle += AllowFight;
            EventContainer.GolemDied += HandleGolemDeath;
            _isDies = false;

            StartCoroutine(AddListenerAfterDelay());
        }

        private IEnumerator AddListenerAfterDelay()
        {
            yield return new WaitForSeconds(0.05f);
            _thisState.AttackReceived += HandleHitReceiving;
            _navMeshAgent.enabled = true;
            if (_thisState == Player.PlayerCharacter)
            {
                PlayerController.AllowAI += SetAIBehaviour;
                PlayerController.AttackByController += AttackTarget;
                PlayerController.SetMovementPointByController += RunToTargetByController;
                PlayerController.SetTargetByController += SetTarget;
                PlayerController.PlayModeChanged += ChangeMode;

                PlayerController.SpellCastByController += CastSpell;
            }
            else if (_thisState != Player.PlayerCharacter)
            {
                PlayerController.AllowAI += CheckHumanAllowAI;
            }
        }

        private void OnDisable()
        {
            Game.StartBattle -= AllowFight;
            EventContainer.GolemDied -= HandleGolemDeath;
            _thisState.AttackReceived -= HandleHitReceiving;
            if (_thisState == Player.PlayerCharacter)
            {
                PlayerController.AllowAI -= SetAIBehaviour;
                PlayerController.AttackByController -= AttackTarget;
                PlayerController.SetMovementPointByController -= RunToTargetByController;
                PlayerController.SetTargetByController -= SetTarget;
                PlayerController.PlayModeChanged -= ChangeMode;

                PlayerController.SpellCastByController -= CastSpell;
            }
            else if (_thisState != Player.PlayerCharacter)
            {
                PlayerController.AllowAI -= CheckHumanAllowAI;
            }
        }

        private void ChangeMode(PlayMode mode)
        {
            _playMode = mode;
        }

        private void CheckHumanAllowAI(bool isAllow)
        {
            if (_thisState != Player.PlayerCharacter)
            {
                _playerAI = isAllow;
            }
        }

        private void SetAIBehaviour(bool isAllow)
        {
            if (_thisState == Player.PlayerCharacter)
            {
                _isAIControlAllowed = isAllow;
            }
        }

        private void AllowFight()
        {
            if (_thisState != Player.PlayerCharacter)
            {
                _isAIControlAllowed = true;
            }

            _isWin = false;
        }

        private void SwitchStatuses()
        {
            switch (_status)
            {
                case FightStatus.Neutral:
                    SetDefaultBehaviour();
                    break;
                case FightStatus.Active:
                    SetFightBehaviour();
                    break;
                case FightStatus.Stunned:
                    break;
                case FightStatus.Fallen:
                    break;
                case FightStatus.CastsSpell:
                    break;
                case FightStatus.Dead:
                    SetDeadBehaviour();
                    break;
                case FightStatus.Scared:
                    SetScaredBehaviour();
                    break;
                case FightStatus.GettingHit:
                    SetGettingHitBehaviour();
                    break;
                case FightStatus.AvoidingHit:
                    SetAvoidingHitBehaviour();
                    break;
                case FightStatus.RunningToTarget:
                    RunToTarget(_targetPoint);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private bool CanFight()
        {
            return _isAIControlAllowed && !_thisState.IsDead && _status != FightStatus.GettingHit &&
                   _status != FightStatus.AvoidingHit && _status != FightStatus.CastsSpell &&
                   Game.Stage == Game.GameStage.Battle;
        }

        private void SetDefaultBehaviour()
        {
            if (!_isAIControlAllowed)
            {
                return;
            }

            _moveable = new NoMoveBehaviour(_animator, _navMeshAgent, AnimationChanger.SetFightIdle);
            _moveable.Move(default, default);
            _attackable = new NoAttackBehaviour(_animator, AnimationChanger.SetFightIdle);
            _attackable.Attack();
            _navMeshAgent.enabled = true;
        }

        #region AnimationEvents

        private void OnAttackStarted()
        {
            _inAttack = true;
            _timeToResetAttack = 0;
        }

        private void OnAttackEnded()
        {
            _inAttack = false;
            if (_thisState != Player.PlayerCharacter)
            {
                _navMeshAgent.enabled = true;
            }
            else if (_thisState == Player.PlayerCharacter && _isAIControlAllowed ||
                     _thisState == Player.PlayerCharacter && _playMode != PlayMode.Standard && _isAIControlAllowed)
            {
                _navMeshAgent.enabled = true;
            }
        }

        private void OnStartJump()
        {
            _navMeshAgent.enabled = false;
        }

        private void OnEndJump()
        {
            if (_thisState != Player.PlayerCharacter)
            {
                _navMeshAgent.enabled = true;
            }
            else if (_thisState == Player.PlayerCharacter && _isAIControlAllowed ||
                     _thisState == Player.PlayerCharacter && _playMode != PlayMode.Standard && _isAIControlAllowed)
            {
                _navMeshAgent.enabled = true;
            }
        }

        private void OnCanFight()
        {
            if (!_thisState.IsDead && _isAIControlAllowed)
            {
                _status = FightStatus.Active;
            }
            else if (!_thisState.IsDead && !_isAIControlAllowed && _thisState == Player.PlayerCharacter)
            {
                _status = FightStatus.Neutral;
            }
        }

        private void OnSpellCastStarted()
        {
            _status = FightStatus.CastsSpell;
        }

        private void OnSpellCasted()
        {
            _status = FightStatus.Neutral;
        }

        #endregion

        private void SetFightBehaviour()
        {
            if (!_targetState)
                return;

            var distanceToTarget = Vector3.Distance(transform.position, _targetState.transform.position);

            if (NeedToCatchUpPlayer())
            {
                RunToTarget();
            }
            else if (InAttackDistance())
            {
                AttackTarget();
            }
            else if (NearToTarget())
            {
                WalkSlowlyWithFightPosture();
            }
            else if (SeeTarget())
            {
                WalkToTarget();
            }
            else
            {
                RunToTarget();
            }

            bool InAttackDistance()
            {
                return distanceToTarget <= _thisState.Stats.attackRange * 1.5f;
            }

            bool NearToTarget()
            {
                return distanceToTarget <= CloseDistance - 10 && !_inAttack;
            }

            bool SeeTarget()
            {
                return distanceToTarget <= CloseDistance && !_inAttack;
            }

            bool NeedToCatchUpPlayer()
            {
                return !InAttackDistance() && _targetState == Player.PlayerCharacter && !_playerAI;
            }
        }

        private void SetScaredBehaviour()
        {
            if (!_targetState)
                return;

            var distanceToTarget = Vector3.Distance(transform.position, _targetState.transform.position);
            if (distanceToTarget < CloseDistance * 3)
            {
                RunToTarget(-1);
            }
            else if (distanceToTarget > CloseDistance * 3)
            {
                SetDefaultBehaviour();
            }
        }

        private void AttackTarget()
        {
            _animator.applyRootMotion = true;
            SetMoveBehaviour(new NoMoveBehaviour(_animator, _navMeshAgent, AnimationChanger.SetFightIdle));
            _moveable.Move(default, default);

            SetAttackBehaviour(_attack);
            _attack.CustomConstructor(HitHeight, _thisState.Stats.attackRange, DestructionRadius,
                _animator, _thisState.Group, _thisState.Stats.damagePerHeat, GetDelayBetweenHits(),
                _thisState.Stats.hitAccuracy,
                _targetState.gameObject, _thisState.Type,
                _thisState.RoundStatistics,
                AnimationChanger.SetSwordAttack, AnimationChanger.SetKickAttack);
            _attackable.Attack();
            _isIKAllowed = true;
            if (!_inAttack)
                TurnSmoothlyToTarget();
        }

        public void CastSpell()
        {
            //привести к нужному типу, эти данные могут храниться например в каррент стэйт

            SetSpellCast(_fireballSpell);
            _fireballSpell.CustomConstructor(ItemContainer.Instance.GetFireBallLvl1(), _targetState.transform,
                _animator, AnimationChanger.SetCastFireBall, _thisState);
            _spellCast.CastSpell();
        }


        private float GetDelayBetweenHits()
        {
            const float seconds = 60;
            return seconds / (_thisState.Stats.attackSpeed / 10);
        }

        private void SetDeadBehaviour()
        {
            if (!_isDies)
            {
                AnimationChanger.SetGolemDie(_animator);
                _isDies = true;
                StartCoroutine(WaitForSecondsToDisable(6));
            }

            _navMeshAgent.enabled = false;
        }

        private void HandleGolemDeath(RoundStatistics killer)
        {
            if (_thisState.IsDead)
            {
                SetDefaultBehaviour();
                _status = FightStatus.Dead;
                return;
            }

            if (_targetState)
            {
                if (_targetState.IsDead)
                {
                    StartCoroutine(FindEnemies());
                }

                return;
            }

            StartCoroutine(FindEnemies());
        }

        private IEnumerator WaitForSecondsToDisable(int sec)
        {
            yield return new WaitForSeconds(sec);
            gameObject.SetActive(false);
            DeathEffect.Instatnce.CreateDeathEffect(new Vector3(transform.position.x, transform.position.y + 1,
                transform.position.z));
            if (Game.RoundEnded)
            {
                EventContainer.OnNewRound();
            }
        }

        private void WalkSlowlyWithFightPosture()
        {
            _animator.applyRootMotion = false;
            SetMoveBehaviour(new WalkNavMeshBehaviour(_thisState.Stats.attackRange, _animator, _navMeshAgent,
                AnimationChanger.SetWalkingFight));
            _moveable.Move(_thisState.Stats.moveSpeed / 2, _targetState.transform.position);
            _isIKAllowed = true;
        }

        private void WalkToTarget()
        {
            _animator.applyRootMotion = false;
            SetMoveBehaviour(new WalkNavMeshBehaviour(_thisState.Stats.attackRange, _animator, _navMeshAgent,
                AnimationChanger.SetGolemWalk));
            _moveable.Move(_thisState.Stats.moveSpeed, _targetState.transform.position);
            _isIKAllowed = false;
        }

        private void RunToTargetByController(Vector3 point)
        {
            if (_thisState == Player.PlayerCharacter)
            {
                _targetPoint = point;
                _status = FightStatus.RunningToTarget;
            }
        }

        private void RunToTarget(int direction = 1)
        {
            _animator.applyRootMotion = false;
            SetMoveBehaviour(new RunNavMeshBehaviour(_thisState, _thisState.Stats.attackRange, _animator, _navMeshAgent,
                false, AnimationChanger.SetGolemRun));
            _moveable.Move(_thisState.Stats.moveSpeed * 2, _targetState.transform.position * direction);
            _isIKAllowed = false;

            _inAttack = false;
        }

        private void RunToTarget(Vector3 point)
        {
            if (_thisState == Player.PlayerCharacter)
            {
                _animator.applyRootMotion = false;
                SetMoveBehaviour(new RunNavMeshBehaviour(_thisState, _thisState.Stats.attackRange, _animator,
                    _navMeshAgent,
                    false, AnimationChanger.SetGolemRun));
                _moveable.Move(_thisState.Stats.moveSpeed * 2, point);
                _isIKAllowed = false;
                _isAIControlAllowed = false;

                if ((point - transform.position).sqrMagnitude < 30)
                {
                    _moveable = new NoMoveBehaviour(_animator, _navMeshAgent, AnimationChanger.SetFightIdle);
                    _moveable.Move(default, default);
                }
            }
        }

        private void SetTarget(GameCharacterState target)
        {
            if (_thisState == Player.PlayerCharacter)
            {
                _targetState = target;
                _isAIControlAllowed = true;
            }
        }

        private void HandleHitReceiving(object sender, EventArgs args)
        {
            AttackHitEventArgs hitArgs = (AttackHitEventArgs)args;

            if (AttackFromBehind())
            {
                hitArgs.DamagePerHit *= 1.5f;
                GetHit(hitArgs);
                EventContainer.OnFightEvent(_thisState,
                    new FightEventArgs((AttackHitEventArgs)args, _thisState.Type, true));
                return;
            }

            GetHitOrAvoid(hitArgs);

            bool AttackFromBehind()
            {
                return Math.Abs(transform.rotation.y - hitArgs.AttackerRotationY) < 0.35f;
            }
        }

        private void GetHitOrAvoid(AttackHitEventArgs hitArgs)
        {
            if (_status == FightStatus.CastsSpell)
            {
                GetHit(hitArgs);
                return;
            }

            var hitChance = GetHitChance(hitArgs.HitAccuracy, _thisState.Stats.avoidChance);
            var random = Random.Range(0, 1.0f);
            if (hitChance > random)
            {
                GetHit(hitArgs);
                EventContainer.OnFightEvent(_thisState, new FightEventArgs(hitArgs, _thisState.Type, false));
            }
            else
            {
                _isAvoidsHit = false;

                _status = FightStatus.AvoidingHit;


                EventContainer.OnFightEvent(_thisState, new FightEventArgs(hitArgs, _thisState.Type, false, true));
            }
        }

        private void GetHit(AttackHitEventArgs hitArgs)
        {
            _thisState.TakeDamage(hitArgs.DamagePerHit, hitArgs.Statistics);
            if (!_thisState.IsDead)
            {
                if (_status != FightStatus.CastsSpell)
                {
                    _isGetsHit = false;
                    _status = FightStatus.GettingHit;
                }

                _soundsController.PlayHittingEnemySound();
            }
        }

        private static float GetHitChance(float accuracy, float evasion)
        {
            if (accuracy >= evasion)
            {
                float difference = accuracy / evasion;
                if (difference >= 5)
                    return 0.95f;
                if (5 > difference && difference >= 4)
                {
                    var res = difference - 4f;
                    var res2 = res / 20f;
                    return res2 + 0.90f;
                }

                if (4 > difference && difference >= 3)
                {
                    var res = difference - 3f;
                    var res2 = res / 20f;
                    return res2 + 0.85f;
                }

                if (3 > difference && difference >= 2)
                {
                    var res = difference - 2f;
                    var res2 = res / 10f;
                    return res2 + 0.75f;
                }

                if (2 > difference && difference >= 1f)
                {
                    var res = difference - 1f;
                    var res2 = res / 4f;
                    return res2 + 0.5f;
                }
            }
            else
            {
                float difference = evasion / accuracy;
                if (difference >= 5)
                    return 1 - 0.95f;
                if (5 > difference && difference >= 4)
                {
                    var res = difference - 4f;
                    var res2 = res / 20f;
                    return 1 - res2 - 0.90f;
                }

                if (4 > difference && difference >= 3)
                {
                    var res = difference - 3f;
                    var res2 = res / 20f;
                    return 1 - res2 - 0.85f;
                }

                if (3 > difference && difference >= 2)
                {
                    var res = difference - 2f;
                    var res2 = res / 10f;
                    return 1 - res2 - 0.75f;
                }

                if (2 > difference && difference >= 1f)
                {
                    var res = difference - 1f;
                    var res2 = res / 4f;
                    return 1 - res2 - 0.5f;
                }
            }

            throw new Exception();
        }

        private void SetAvoidingHitBehaviour()
        {
            if (!_isAvoidsHit)
            {
                _isAvoidsHit = true;
                _animator.applyRootMotion = true;
                AnimationChanger.SetAvoidHit(_animator);
            }
        }

        private void SetGettingHitBehaviour()
        {
            if (!_isGetsHit)
            {
                _isGetsHit = true;
                _animator.applyRootMotion = true;
                AnimationChanger.SetGetHit(_animator);
            }
        }

        private void Fall()
        {
            _animator.applyRootMotion = true;
        }

        private void SetMoveBehaviour(IMoveable moveable)
        {
            _moveable = moveable;
        }

        private void SetAttackBehaviour(IAttackable attackable)
        {
            _attackable = attackable;
        }

        private void SetSpellCast(ICastable castable)
        {
            _spellCast = castable;
        }

        private GameCharacterState[] GetEnemies()
        {
            return Game.AllGolems.Where(p => p.IsDead == false)
                .Where(p => p.Group != _thisState.Group).ToArray();
        }

        private IEnumerator FindEnemies()
        {
            var enemies = GetEnemies();

            if (enemies.Length == 0)
            {
                if (Game.Stage == Game.GameStage.Battle && !_isWin)
                {
                    _isWin = true;
                    EventContainer.OnWinBattle(_thisState);
                    _thisState.RoundStatistics.Wins++;
                    _thisState.RoundStatistics.WinLastRound = true;
                }

                yield return new WaitForSeconds(1);
                _targetState = null;
                StartCoroutine(FindEnemies());
            }

            if (enemies.Length > 0)
            {
                _targetState = enemies[Random.Range(0, enemies.Length)];
                yield return new WaitForSeconds(AutoResetTargetDelay);
                StartCoroutine(FindEnemies());
            }
        }

        private void OnAnimatorIK(int layerIndex)
        {
            if (_isIKAllowed)
            {
                TurnHeadToTarget();
            }
        }

        private void TurnHeadToTarget()
        {
            if (_targetState)
            {
                _animator.SetLookAtWeight(1);
                _animator.SetLookAtPosition(new Vector3(_targetState.transform.position.x,
                    _targetState.transform.position.y + HitHeight, _targetState.transform.position.z));
            }
        }

        private void TurnSmoothlyToTarget()
        {
            if (_targetState)
            {
                Vector3 direction = _targetState.transform.position - transform.position;
                Quaternion rotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation,
                    _thisState.Stats.moveSpeed * Time.deltaTime);
            }
        }

        private void ResetAttackIfNeed()
        {
            if (_inAttack)
            {
                _timeToResetAttack += Time.deltaTime;
                if (_timeToResetAttack >= GetDelayBetweenHits() * 1.5f)
                {
                    ForceEndAttack();
                }
            }
        }

        private void ForceEndAttack()
        {
            _timeToResetAttack = 0;
            _inAttack = false;
        }
    }
}