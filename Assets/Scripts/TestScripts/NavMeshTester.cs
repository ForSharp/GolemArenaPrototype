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
        //_animator.SetFloat("Forward", 1f, 0.1f, Time.deltaTime);
        _animator.SetFloat("Forward", 0.5f);
    }

    private void Update()
    {
        _navMeshAgent.SetDestination(endPoint.position);
        
        //_animator.SetFloat("Forward", 1f);
        
        
        if (_navMeshAgent.isStopped)
        {
            _animator.SetFloat("Forward", 0f);
        }
        
        // if (!_navMeshAgent.isStopped)
        // {
        //     _animator.SetFloat("Forward", 0.5f);
        // }
        
        if (!_navMeshAgent.isStopped)
        {
            _animator.SetFloat("Forward", 1f);
        }
    }
}
