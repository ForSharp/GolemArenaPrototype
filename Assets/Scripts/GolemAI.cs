using System.Collections;
using System.Linq;
using GolemEntity;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class GolemAI : MonoBehaviour
{
    private bool _isAIControlAllowed = false;
    
    private IMoveable _moveable;
    private IAttackable _attackable;

    private GameCharacterState _thisState;
    private GameCharacterState _targetState;
    
    private NavMeshAgent _navMeshAgent;
    private Animator _animator;

    private const float CloseDistance = 10;
    private const float HitHeight = 1.75f;
    private const float DestructionRadius = 1f;
    
    private void Start()
    {
        _thisState = GetComponent<GameCharacterState>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();

        _moveable = new NoMoveBehaviour(_animator, animator => AnimationChanger.SetIdle(animator));
        _attackable = new NoAttackBehaviour(_animator, animator => AnimationChanger.SetIdle(animator));

        StartCoroutine(FindEnemies());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) //user input class
            _isAIControlAllowed = true;
        
        if (!_isAIControlAllowed || _thisState.IsDead || !_targetState)
            return;
        OrganizeBehaviour();
    }

    private void OrganizeBehaviour()
    {
        var distanceToTarget = Vector3.Distance(transform.position, _targetState.transform.position);
        
        if (_thisState.Stats.AttackRange >= distanceToTarget)
        {
            SetMoveBehaviour(new NoMoveBehaviour(_animator, animator => AnimationChanger.SetIdle(animator))); 
            _moveable.Move(default, default);
            transform.LookAt(_targetState.transform.position);
            var attack = gameObject.GetComponent<CommonAttackBehaviour>();
            SetAttackBehaviour(attack);
            attack.FactoryMethod(HitHeight, _thisState.Stats.AttackRange, DestructionRadius,
                _animator, _thisState.Group, false, AnimationChanger.SetGolemDoubleHit,
                AnimationChanger.SetGolemLeftHit,
                AnimationChanger.SetGolemRightHit);
            _attackable.Attack(_thisState.Stats.DamagePerHeat, 2, transform.position);
        }
        else if (CloseDistance >= distanceToTarget)
        {
            SetMoveBehaviour(new WalkBehaviour(_thisState.Stats.AttackRange, _animator, _navMeshAgent, 
                animator => AnimationChanger.SetGolemWalk(animator)));
            _moveable.Move(5, _targetState.transform.position); //5 just for test
        }
        else
        {
            SetMoveBehaviour(new RunBehaviour(_thisState, _thisState.Stats.AttackRange, _animator, _navMeshAgent, false, 
                animator => AnimationChanger.SetGolemRun(animator)));
            _moveable.Move(10, _targetState.transform.position);
        }
        
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
            StartCoroutine(FindEnemies());
        }
        
        if (enemies.Length > 0)
        {
            _targetState = enemies[Random.Range(0, enemies.Length)];
            //transform.LookAt(_targetState.transform.position); //attention
            yield return new WaitForSeconds(5);
            StartCoroutine(FindEnemies());
        }
    }
    
}
