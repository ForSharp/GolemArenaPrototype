using System;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshTester : MonoBehaviour
{
    public Transform endPoint;

    private NavMeshAgent _navMeshAgent;
    private Animator _animator;

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        //_animator.SetFloat("Forward", 0f);
    }

    private void Update()
    {
        _navMeshAgent.SetDestination(endPoint.position);
        
        if (_navMeshAgent.isStopped)
        {
            _animator.SetFloat("Forward", 0f);
        }

        if (!_navMeshAgent.isStopped)
        {
            _animator.SetFloat("Forward", 1f);
        }
    }
}
