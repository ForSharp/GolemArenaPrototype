﻿using System;
using FightState;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviourStrategy
{
    public class RunNavMeshBehaviour: WalkNavMeshBehaviour
    {
        private GameCharacterState _state;
        private bool _isWastingEnergy;
    
        public RunNavMeshBehaviour(GameCharacterState thisState, float stopDistance, Animator animator, NavMeshAgent agent,
            bool isWastingEnergy = true, params Action<Animator>[] walkAnimationSetters) : base(stopDistance, animator, agent, walkAnimationSetters)
        {
            _state = thisState;
            _isWastingEnergy = isWastingEnergy;
        }

        public override void Move(float moveSpeed, Vector3 targetPos)
        {
            base.Move(moveSpeed, targetPos);
            if (_isWastingEnergy)
            {
                WasteEnergy();
            }
        }

        private void WasteEnergy()
        {
            _state.SpendStamina(default);
        }
    }
}