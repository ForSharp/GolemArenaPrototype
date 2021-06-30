using System;
using System.Collections;
using System.Linq;
using GolemEntity;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class GolemAI : MonoBehaviour
{
    private bool _isAIControlAllowed = false;
    private bool _isIKAllowed = false;

    private IMoveable _moveable;
    private IAttackable _attackable;

    private GameCharacterState _thisState;
    private GameCharacterState _targetState;

    private NavMeshAgent _navMeshAgent;
    private Animator _animator;
    private FightStatus _status;

    private const float CloseDistance = 10;
    private const float HitHeight = 1.75f;
    private const float DestructionRadius = 1f;
    private const float MoveSpeed = 5f;
    private const float DelayBetweenHits = 3f;

    private void Start()
    {
        _thisState = GetComponent<GameCharacterState>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        
        _status = FightStatus.Neutral;
        AnimationChanger.SetFightIdle(_animator, true);
        StartCoroutine(FindEnemies());

        EventContainer.GolemDied += HandleGolemDeath;
    }

    private void Update()
    {
        SwitchStatuses();
        
        if (Input.GetKeyDown(KeyCode.I))
            _isAIControlAllowed = true;

        if (_isAIControlAllowed && _targetState)
        {
            _status = FightStatus.Active;
        }

        _status = FightStatus.Neutral;
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
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void SetDefaultBehaviour()
    {
        _moveable = new NoMoveBehaviour(_animator, _navMeshAgent, AnimationChanger.SetFightIdle);
        _attackable = new NoAttackBehaviour(_animator, AnimationChanger.SetFightIdle);
    }

    private void SetFightBehaviour()
    {
        var distanceToTarget = Vector3.Distance(transform.position, _targetState.transform.position);

        if (_thisState.Stats.AttackRange * 1.5 >= distanceToTarget)
        {
            AttackEnemy();
        }
        else if (CloseDistance - 3 >= distanceToTarget)
        {
            WalkSlowlyWithFightPosture();
        }
        else if (CloseDistance >= distanceToTarget)
        {
            WalkToEnemy();
        }
        else
        {
            RunToEnemy();
        }
    }

    private void AttackEnemy()
    {
        var thisPos = transform.position;

        transform.LookAt(_targetState.transform.position);
        _animator.applyRootMotion = true;
        SetMoveBehaviour(new NoMoveBehaviour(_animator, _navMeshAgent, AnimationChanger.SetFightIdle));
        _moveable.Move(default, default);

        var attack = gameObject.GetComponent<CommonMeleeAttackBehaviour>();
        SetAttackBehaviour(attack);
        attack.FactoryMethod(HitHeight, _thisState.Stats.AttackRange, DestructionRadius,
            _animator, _thisState.Group, _thisState.RoundStatistics, false,
            AnimationChanger.SetHitAttack, AnimationChanger.SetKickAttack);
        _attackable.Attack(_thisState.Stats.DamagePerHeat, DelayBetweenHits, thisPos);

        _navMeshAgent.SetDestination(thisPos);
        _isIKAllowed = true;
    }

    private void HandleGolemDeath()
    {
        if (_thisState.IsDead)
        {
            AnimationChanger.SetGolemDie(_animator);

            _thisState.LastEnemyAttacked.Kills += 1;
            EventContainer.GolemDied -= HandleGolemDeath;
            _navMeshAgent.baseOffset = -0.75f;
            StartCoroutine(WaitForSecondsToDisable(6));
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
    }

    private void WalkSlowlyWithFightPosture()
    {
        _animator.applyRootMotion = false;
        SetMoveBehaviour(new WalkBehaviour(_thisState.Stats.AttackRange, _animator, _navMeshAgent,
            AnimationChanger.SetWalkingFight));
        _moveable.Move(MoveSpeed / 2, _targetState.transform.position);
        _isIKAllowed = true;
    }

    private void WalkToEnemy()
    {
        _animator.applyRootMotion = false;
        SetMoveBehaviour(new WalkBehaviour(_thisState.Stats.AttackRange, _animator, _navMeshAgent,
            AnimationChanger.SetGolemWalk));
        _moveable.Move(MoveSpeed, _targetState.transform.position);
        _isIKAllowed = false;
    }

    private void RunToEnemy()
    {
        _animator.applyRootMotion = false;
        SetMoveBehaviour(new RunBehaviour(_thisState, _thisState.Stats.AttackRange, _animator, _navMeshAgent,
            false, AnimationChanger.SetGolemRun));
        _moveable.Move(MoveSpeed * 2, _targetState.transform.position);
        _isIKAllowed = false;
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
        return FindObjectsOfType<GameCharacterState>().Where(p => p.IsDead == false)
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
            yield return new WaitForSeconds(30);
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
}