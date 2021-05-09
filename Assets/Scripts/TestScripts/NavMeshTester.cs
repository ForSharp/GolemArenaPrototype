using System;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshTester : MonoBehaviour
{
    public Transform endPoint;

    private NavMeshAgent _navMeshAgent;

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        _navMeshAgent.SetDestination(endPoint.position);
    }
}
