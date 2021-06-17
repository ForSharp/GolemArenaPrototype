using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class NoMoveBehaviour: IMoveable
{
    private Animator _animator;
    private Action<Animator>[] _idleAnimationSetters;

    public NoMoveBehaviour(Animator animator, params Action<Animator>[] idleAnimationSetters)
    {
        _animator = animator;
        _idleAnimationSetters = idleAnimationSetters;
    }
    
    public void Move(float moveSpeed = default, Vector3 targetPos = default)
    {
        _idleAnimationSetters[Random.Range(0, _idleAnimationSetters.Length)].Invoke(_animator);
    }
}