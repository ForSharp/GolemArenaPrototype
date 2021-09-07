using System;
using System.Collections;
using System.Linq;
using BehaviourStrategy;
using Fight;
using GameLoop;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace GolemEntity
{
    public class GolemAI : MonoBehaviour
    {
        private bool _isAIControlAllowed;
        private bool _isIKAllowed;
        private bool _isDies;
        private bool _inAttack;
        private bool _isGetsHit;
        private bool _isAvoidsHit;

        private IMoveable _moveable;
        private IAttackable _attackable;

        private GameCharacterState _thisState;
        private GameCharacterState _targetState;
        private CommonMeleeAttackBehaviour _attack;
        private NavMeshAgent _navMeshAgent;
        private Animator _animator;
        private FightStatus _status;
        private float _timeToResetAttack;
        private SoundsController _soundsController;

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

            EventContainer.GolemDied += HandleGolemDeath;
            _thisState.AttackReceived += HandleHitReceiving;
            
        }

        private void Update()
        {
            SwitchStatuses();
            ResetAttackIfNeed();

            if (CanFight())
            {
                _status = FightStatus.Active;
            }
            else if (!_thisState.IsDead)
            {
                _status = FightStatus.Neutral;
            }
        }

        private void OnEnable()
        {
            Game.StartBattle += AllowFight;
        }

        private void OnDisable()
        {
            Game.StartBattle -= AllowFight;
        }

        private void AllowFight()
        {
            _isAIControlAllowed = true;
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
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private bool CanFight()
        {
            return _isAIControlAllowed && _targetState && !_thisState.IsDead && _status != FightStatus.GettingHit && _status != FightStatus.AvoidingHit;
        }

        private void SetDefaultBehaviour()
        {
            _moveable = new NoMoveBehaviour(_animator, _navMeshAgent, AnimationChanger.SetFightIdle);
            _moveable.Move(default, default);
            _attackable = new NoAttackBehaviour(_animator, AnimationChanger.SetFightIdle);
            _attackable.Attack();
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
        }

        private void OnCanFight()
        {
            if (CanFight())
                _status = FightStatus.Active;
        }

        #endregion
        
        private void SetFightBehaviour()
        {
            if (!_targetState)
                return;

            var distanceToTarget = Vector3.Distance(transform.position, _targetState.transform.position);
            
            if (InAttackDistance())
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
                return distanceToTarget <= _thisState.Stats.AttackRange * 1.5f;
            }

            bool NearToTarget()
            {
                return distanceToTarget <= CloseDistance - 10 && !_inAttack;
            }

            bool SeeTarget()
            {
                return distanceToTarget <= CloseDistance && !_inAttack;
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
            _attack.CustomConstructor(HitHeight, _thisState.Stats.AttackRange, DestructionRadius,
                _animator, _thisState.Group, _thisState.Stats.DamagePerHeat, GetDelayBetweenHits(),
                _thisState.Stats.HitAccuracy,
                _targetState.gameObject, _navMeshAgent, _thisState.Type,
                _thisState.RoundStatistics,
                AnimationChanger.SetSwordAttack, AnimationChanger.SetKickAttack);
            _attackable.Attack();
            _isIKAllowed = true;
            if (!_inAttack)
                TurnSmoothlyToTarget();
        }

        private float GetDelayBetweenHits()
        {
            const float seconds = 60;
            return seconds / (_thisState.Stats.AttackSpeed / 10);
        }

        private void SetDeadBehaviour()
        {
            if (!_isDies)
            {
                AnimationChanger.SetGolemDie(_animator);
                _isDies = true;
                StartCoroutine(WaitForSecondsToDisable(6));
            }

            _navMeshAgent.baseOffset = -0.8f;
        }

        private void HandleGolemDeath()
        {
            if (_thisState.IsDead)
            {
                SetDefaultBehaviour();
                _status = FightStatus.Dead;
                _thisState.LastEnemyAttacked.Kills += 1;
                EventContainer.GolemDied -= HandleGolemDeath;
                _thisState.AttackReceived -= HandleHitReceiving;
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
        }

        private void WalkSlowlyWithFightPosture()
        {
            _animator.applyRootMotion = false;
            SetMoveBehaviour(new WalkBehaviour(_thisState.Stats.AttackRange, _animator, _navMeshAgent,
                AnimationChanger.SetWalkingFight));
            _moveable.Move(_thisState.Stats.MoveSpeed / 2, _targetState.transform.position);
            _isIKAllowed = true;
        }

        private void WalkToTarget()
        {
            _animator.applyRootMotion = false;
            SetMoveBehaviour(new WalkBehaviour(_thisState.Stats.AttackRange, _animator, _navMeshAgent,
                AnimationChanger.SetGolemWalk));
            _moveable.Move(_thisState.Stats.MoveSpeed, _targetState.transform.position);
            _isIKAllowed = false;
        }

        private void RunToTarget(int direction = 1)
        {
            _animator.applyRootMotion = false;
            SetMoveBehaviour(new RunBehaviour(_thisState, _thisState.Stats.AttackRange, _animator, _navMeshAgent,
                false, AnimationChanger.SetGolemRun));
            _moveable.Move(_thisState.Stats.MoveSpeed * 2, _targetState.transform.position * direction);
            _isIKAllowed = false;
            
            while (_navMeshAgent.baseOffset > 0 && _inAttack)
            {
                _navMeshAgent.baseOffset -= Time.deltaTime * 0.25f;
            }

            _inAttack = false;
        }

        private void HandleHitReceiving(object sender, EventArgs args)
        {
            AttackHitEventArgs hitArgs = (AttackHitEventArgs) args;

            if (AttackFromBehind())
            {
                hitArgs.DamagePerHit *= 1.5f;
                GetHit(hitArgs);
                EventContainer.OnFightEvent(new FightEventArgs((AttackHitEventArgs) args, _thisState.Type, true));
                return;
            }

            GetHitOrAvoid(hitArgs);

            bool AttackFromBehind()
            {
                //Debug.Log($" {_thisState.Type} {transform.rotation.y}, {hitArgs.AttackerName} {hitArgs.AttackerRotationY}"); 
                return Math.Abs(transform.rotation.y - hitArgs.AttackerRotationY) < 0.35f;
            }
        }

        private void GetHitOrAvoid(AttackHitEventArgs hitArgs)
        {
            var hitChance = GetHitChance(hitArgs.HitAccuracy, _thisState.Stats.AvoidChance);
            var random = Random.Range(0, 1.0f);
            if (hitChance > random)
            {
                GetHit(hitArgs);
                EventContainer.OnFightEvent(new FightEventArgs(hitArgs, _thisState.Type, false));
            }
            else
            {
                _isAvoidsHit = false;
                _status = FightStatus.AvoidingHit;
                EventContainer.OnFightEvent(new FightEventArgs(hitArgs, _thisState.Type, false, true));
            }
        }

        private void GetHit(AttackHitEventArgs hitArgs)
        {
            _thisState.TakeDamage(hitArgs.DamagePerHit, _thisState.Stats.Defence, hitArgs.Statistics);
            if (!_thisState.IsDead)
            {
                _isGetsHit = false;
                _status = FightStatus.GettingHit;
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
                    _thisState.Stats.MoveSpeed * Time.deltaTime);
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