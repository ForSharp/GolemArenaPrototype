﻿using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class WalkBehaviour : IMoveable
{
    private Animator _animator;
    private NavMeshAgent _agent;
    private Action<Animator>[] _walkAnimationSetters;
    private float _stopDistance;

    public WalkBehaviour(float stopDistance, Animator animator, NavMeshAgent agent, params Action<Animator>[] walkAnimationSetters)
    {
        _stopDistance = stopDistance;
        _animator = animator;
        _agent = agent;
        _walkAnimationSetters = walkAnimationSetters;
    }
    
    public virtual void Move(float moveSpeed, Vector3 targetPos)
    {
        _agent.stoppingDistance = _stopDistance;
        _walkAnimationSetters[Random.Range(0, _walkAnimationSetters.Length)].Invoke(_animator);
        _agent.speed = moveSpeed;
        _agent.SetDestination(targetPos);
    }
}