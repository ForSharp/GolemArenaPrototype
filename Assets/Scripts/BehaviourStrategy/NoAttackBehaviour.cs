using System;
using BehaviourStrategy.Abstracts;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BehaviourStrategy
{
    public class NoAttackBehaviour : IAttackable
    {
        private Animator _animator;
        private Action<Animator>[] _idleAnimationSetters;

        public NoAttackBehaviour(Animator animator, params Action<Animator>[] idleAnimationSetters)
        {
            _animator = animator;
            _idleAnimationSetters = idleAnimationSetters;
        }
    
        public void Attack()
        {
            _idleAnimationSetters[Random.Range(0, _idleAnimationSetters.Length)].Invoke(_animator);
        }
    }
}