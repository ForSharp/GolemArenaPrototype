using System;
using __Scripts.Behaviour.Abstracts;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace __Scripts.Behaviour
{
    public class WalkNavMeshBehaviour : IMoveable
    {
        private readonly Animator _animator;
        private readonly NavMeshAgent _agent;
        private readonly Action<Animator>[] _walkAnimationSetters;
        private readonly float _stopDistance;

        public WalkNavMeshBehaviour(float stopDistance, Animator animator, NavMeshAgent agent, params Action<Animator>[] walkAnimationSetters)
        {
            _stopDistance = stopDistance;
            _animator = animator;
            _agent = agent;
            _walkAnimationSetters = walkAnimationSetters;
        }
    
        public virtual void Move(float moveSpeed, Vector3 targetPos)
        {
            if (_agent && _agent.isActiveAndEnabled)
            {
                _agent.stoppingDistance = _stopDistance;
                _walkAnimationSetters[Random.Range(0, _walkAnimationSetters.Length)].Invoke(_animator);
                _agent.speed = moveSpeed;
                _agent.SetDestination(targetPos);
            }
        }
    }
}