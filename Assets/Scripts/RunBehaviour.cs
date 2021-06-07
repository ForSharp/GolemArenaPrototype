using System;
using UnityEngine;
using UnityEngine.AI;

public class RunBehaviour: WalkBehaviour
{
    private GameCharacterState _state;
    private bool _isWastingEnergy;
    
    public RunBehaviour(GameCharacterState thisState, Animator animator, NavMeshAgent agent,
        bool isWastingEnergy = true, params Action<Animator>[] walkAnimationSetters) : base(animator, agent, walkAnimationSetters)
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