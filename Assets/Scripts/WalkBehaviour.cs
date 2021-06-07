using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class WalkBehaviour : MonoBehaviour, IMoveable
{
    private Animator _animator;
    private NavMeshAgent _agent;
    private Action<Animator>[] _walkAnimationSetters;

    public WalkBehaviour(Animator animator, NavMeshAgent agent, params Action<Animator>[] walkAnimationSetters)
    {
        _animator = animator;
        _agent = agent;
        _walkAnimationSetters = walkAnimationSetters;
    }
    
    public virtual void Move(float moveSpeed, Vector3 targetPos)
    {
        _walkAnimationSetters[Random.Range(0, _walkAnimationSetters.Length)].Invoke(_animator);
        _agent.speed = moveSpeed;
        _agent.SetDestination(targetPos);
    }
}