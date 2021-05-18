using System;
using GolemEntity;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshTester : MonoBehaviour
{
    public Transform endPoint;

    [SerializeField] private float _closeDistance;
    [SerializeField] private float _stopDistance = 4f;
    private NavMeshAgent _navMeshAgent;
    private Animator _animator;
    private bool _isCloseToTarget;
    

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _closeDistance = _navMeshAgent.stoppingDistance;
        _animator = GetComponent<Animator>();
        AnimationChanger.SetIdle(_animator);
    }

    private void Update()
    {
        _navMeshAgent.SetDestination(endPoint.position);
        
        MoveToDestination();
        
    }

    private void MoveToDestination()
    {
        if (GetDistanceToTarget() >= _closeDistance)
        {
            //RunToDestination();
            WalkToDestination();
        }
        else if (GetDistanceToTarget() >= _stopDistance || GetDistanceToTarget() < _closeDistance)
        {
            WalkToDestination();
        }
        else if (GetDistanceToTarget() <= _stopDistance)
        {
            AnimationChanger.SetIdle(_animator);
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
        return Vector3.Distance(transform.position, endPoint.position);
    }
}
