using System;
using CharacterEntity.CharacterState;
using CharacterEntity.State;
using UnityEngine;
using UnityEngine.AI;

namespace Behaviour
{
    public class RunNavMeshBehaviour: WalkNavMeshBehaviour
    {
        private CharacterState _state;
        private bool _isWastingEnergy;
    
        public RunNavMeshBehaviour(CharacterState thisState, float stopDistance, Animator animator, NavMeshAgent agent,
             params Action<Animator>[] walkAnimationSetters) : base(stopDistance, animator, agent, walkAnimationSetters)
        {
            _state = thisState;
        }

        public override void Move(float moveSpeed, Vector3 targetPos)
        {
            base.Move(moveSpeed, targetPos);
            
        }

        
    }
}