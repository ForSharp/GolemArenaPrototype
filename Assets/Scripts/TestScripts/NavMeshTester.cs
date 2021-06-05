using System;
using GolemEntity;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshTester : MonoBehaviour
{
    //public Transform endPoint;

    [SerializeField] private float _closeDistance;
    [SerializeField] private float _stopDistance = 4f;
    private NavMeshAgent _navMeshAgent;
    private Animator _animator;
    private bool _isCloseToTarget;
    private CurrentGameCharacterState _characterState;
    private EnemySeeker _enemySeeker;

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _closeDistance = _navMeshAgent.stoppingDistance;
        _animator = GetComponent<Animator>();
        AnimationChanger.SetIdle(_animator);
        _characterState = GetComponent<CurrentGameCharacterState>();
        _enemySeeker = GetComponent<EnemySeeker>();
    }

    private void Update()
    {
        if (_characterState.isDead || !_enemySeeker)
            return;
        
        _navMeshAgent.SetDestination(_enemySeeker.transform.position);
        
        MoveToDestination();
        
    }

    private void MoveToDestination()
    {
        if (_enemySeeker.inAttackPos)
        {
            AnimationChanger.SetIdle(_animator);
        }
        else if (GetDistanceToTarget() >= _closeDistance)
        {
            RunToDestination();
            //WalkToDestination();
        }
        else if (GetDistanceToTarget() >= _stopDistance || GetDistanceToTarget() < _closeDistance)
        {
            WalkToDestination();
        }

    }
    
    private void RunToDestination()
    {
        AnimationChanger.SetGolemRun(_animator);
    }

    private void WalkToDestination()
    {
        AnimationChanger.SetGolemWalk(_animator);
    }

    private float GetDistanceToTarget()
    {
        return Vector3.Distance(transform.position, _enemySeeker.transform.position);
    }
}
