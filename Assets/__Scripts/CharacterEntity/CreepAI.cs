using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using __Scripts.CharacterEntity.State;
using __Scripts.GameLoop;
using Behaviour;
using Behaviour.Abstracts;
using CharacterEntity.CharacterState;
using CharacterEntity.State;
using GameLoop;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace CharacterEntity
{
    public class CreepAI : MonoBehaviour
    {
        private bool _isDies;
        private bool _inAttack;
        
        private IMoveable _moveable;
        private IAttackable _attackable;
        
        private CreepState _thisState;
        private State.CharacterState _targetState;
        private ChampionState _ownerState;
        private CommonMeleeAttackBehaviour _attack;
        private NavMeshAgent _navMeshAgent;
        private Animator _animator;
        private FightStatus _status;
        private float _timeToResetAttack;
        //private SoundsController _soundsController;
        
        private const float CloseDistance = 20;
        private const float HitHeight = 0.75f;
        private const float DestructionRadius = 0.35f;
        private const int AutoResetTargetDelay = 30;
        
        private void Start()
        {
            _thisState = GetComponent<CreepState>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
            _attack = GetComponent<CommonMeleeAttackBehaviour>();

            _ownerState = _thisState.CreepOwner;
            _status = FightStatus.Neutral;
            AnimationChangerCreep.SetIdle(_animator);
            StartCoroutine(FindEnemies());
            _isDies = false;
            //_soundsController = GetComponent<SoundsController>();
        }

        private void OnEnable()
        {
            EventContainer.CharacterDied += HandleCharacterDeath;
            _isDies = false;

            StartCoroutine(AddListenerAfterDelay());
        }

        private IEnumerator AddListenerAfterDelay()
        {
            yield return new WaitForSeconds(0.05f);
            _thisState.AttackReceived += HandleHitReceiving;
            _thisState.StunCharacter += StunCharacter;
            _thisState.EndStunCharacter += EndStunCharacter;
            _navMeshAgent.enabled = true;
        }

        private void OnDestroy()
        {
            RemoveListeners();
        }

        private void RemoveListeners()
        {
            EventContainer.CharacterDied -= HandleCharacterDeath;
            _thisState.AttackReceived -= HandleHitReceiving;
            _thisState.StunCharacter -= StunCharacter;
            _thisState.EndStunCharacter -= EndStunCharacter;
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
        
        private bool CanFight()
        {
            return !_thisState.IsDead && Game.Stage == Game.GameStage.Battle && _status != FightStatus.Stunned;
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
                    SetDefaultBehaviour();
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
                    break;
                case FightStatus.AvoidingHit:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        private void OnAttackStarted()
        {
            _inAttack = true;
            _timeToResetAttack = 0;
            _animator.applyRootMotion = true;
        }

        private void OnAttackEnded()
        {
            _inAttack = false;
            
            _navMeshAgent.enabled = true;
        }
        
        private void SetDefaultBehaviour()
        {
            _moveable = new NoMoveBehaviour(_animator, _navMeshAgent, AnimationChangerCreep.SetIdle);
            _moveable.Move(default, default);
            _attackable = new NoAttackBehaviour(_animator, AnimationChangerCreep.SetIdle);
            _attackable.Attack();
            _navMeshAgent.enabled = true;
        }
        
        private List<string> _activeStunsId = new List<string>();

        private void StunCharacter(string stunId)
        {
            _status = FightStatus.Stunned;
            _activeStunsId.Add(stunId);
        }

        private void EndStunCharacter(string stunId)
        {
            _activeStunsId.Remove(stunId);
            if (_activeStunsId.Count == 0)
                _status = FightStatus.Neutral;
        }
        
        private void SetFightBehaviour()
        {
            if (!_targetState)
                return;

            var distanceToTarget = Vector3.Distance(transform.position, _targetState.transform.position);

            if (InAttackDistance())
            {
                AttackTarget();
            }
            else
            {
                RunToTarget();
            }

            bool InAttackDistance()
            {
                return distanceToTarget <= _thisState.Stats.attackRange * 1.5f;
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
            if (!_targetState)
                return;
            
            _animator.applyRootMotion = true;
            SetMoveBehaviour(new NoMoveBehaviour(_animator, _navMeshAgent, AnimationChangerCreep.SetIdle));
            _moveable.Move(default, default);

            SetAttackBehaviour(_attack);
            _attack.Initialize(HitHeight, _thisState.Stats.attackRange, DestructionRadius,
                _animator, _thisState.Group, _thisState.Stats.damagePerHeat, GetDelayBetweenHits(),
                _thisState.Stats.hitAccuracy,
                _targetState.gameObject, _thisState.Type,
                _ownerState.RoundStatistics,
                AnimationChangerCreep.SetAttack);
            _attackable.Attack();
            
            if (!_inAttack)
                TurnSmoothlyToTarget();
        }
        
        private float GetDelayBetweenHits()
        {
            const float seconds = 60;
            return seconds / (_thisState.Stats.attackSpeed / 10);
        }
        
        private void RunToTarget(int direction = 1)
        {
            _animator.applyRootMotion = false;
            SetMoveBehaviour(new RunNavMeshBehaviour(_thisState, _thisState.Stats.attackRange, _animator, _navMeshAgent,
                AnimationChangerCreep.SetMoving));
            _moveable.Move(_thisState.Stats.moveSpeed * 2, _targetState.transform.position * direction);

            _inAttack = false;
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
        
        private void SetDeadBehaviour()
        {
            if (!_isDies)
            {
                AnimationChangerCreep.SetDeath(_animator);
                _isDies = true;
                StartCoroutine(WaitForSecondsToDisable(4));
            }

            _navMeshAgent.enabled = false;
        }
        
        private IEnumerator WaitForSecondsToDisable(int sec)
        {
            yield return new WaitForSeconds(sec);
            Game.AllCharactersInSession.Remove(_thisState);
            
            Destroy(gameObject);
        }
        
        private void HandleCharacterDeath(RoundStatistics killer)
        {
            if (_thisState.IsDead)
            {
                SetDeadBehaviour();
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
        
        private void SetMoveBehaviour(IMoveable moveable)
        {
            _moveable = moveable;
        }
        
        private void SetAttackBehaviour(IAttackable attackable)
        {
            _attackable = attackable;
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
            }
            else
            {
                GetHit(hitArgs);
                EventContainer.OnFightEvent(_thisState,
                    new FightEventArgs((AttackHitEventArgs)args, _thisState.Type, false));
            }

            bool AttackFromBehind()
            {
                return Math.Abs(transform.rotation.y - hitArgs.AttackerRotationY) < 0.35f;
            }
        }
        
        private void GetHit(AttackHitEventArgs hitArgs)
        {
            _thisState.TakeDamage(hitArgs.DamagePerHit, null);
            //_thisState.TakeDamage(hitArgs.DamagePerHit, hitArgs.Statistics);
            if (!_thisState.IsDead)
            {
                //_soundsController.PlayHittingEnemySound();
            }
        }
        
        private State.CharacterState[] GetEnemies()
        {
            return Game.AllCharactersInSession.Where(p => p.IsDead == false)
                .Where(p => p.Group != _thisState.Group).ToArray();
        }

        private IEnumerator FindEnemies()
        {
            var enemies = GetEnemies();

            if (enemies.Length == 0)
            {
                if (Game.Stage == Game.GameStage.Battle && _ownerState.IsDead)
                {
                    EventContainer.OnWinBattle(_ownerState);
                    _ownerState.RoundStatistics.Wins++;
                    _ownerState.RoundStatistics.WinLastRound = true;
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
    }
}