using System;
using BehaviourStrategy.Abstracts;
using FightState;
using UnityEngine;

namespace BehaviourStrategy
{
    public abstract class AbstractSpell : MonoBehaviour, ICastable
    {
        protected ISpellInfo Info;
        protected Transform Target;
        protected Animator Animator;
        protected Action<Animator> CastAnimation;
        protected GameObject SpellEffect;
        protected GameCharacterState State;
        
        public void CustomConstructor(ISpellInfo info, Transform target, Animator animator, 
            Action<Animator> castAnimation, GameObject spellEffect, GameCharacterState state)
        {
            Info = info;
            Target = target;
            Animator = animator;
            CastAnimation = castAnimation;
            SpellEffect = spellEffect;
            State = state;
        }

        public abstract void CastSpell();

    }
}