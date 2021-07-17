using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace BehaviourStrategy
{
    public class NoMoveBehaviour: IMoveable
    {
        private Animator _animator;
        private Action<Animator>[] _idleAnimationSetters;
        private NavMeshAgent _navMeshAgent;

        public NoMoveBehaviour(Animator animator, NavMeshAgent agent = default, params Action<Animator>[] idleAnimationSetters)
        {
            _animator = animator;
            _idleAnimationSetters = idleAnimationSetters;
            _navMeshAgent = agent;
        }
    
        public void Move(float moveSpeed = default, Vector3 targetPos = default)
        {
            _idleAnimationSetters[Random.Range(0, _idleAnimationSetters.Length)].Invoke(_animator);

            // if (_navMeshAgent)
            // {
            //     _navMeshAgent.speed = 0f;
            //     _navMeshAgent.stoppingDistance = 0f;
            // }
        }
    
    }
}