using System;
using BehaviourStrategy.Abstracts;
using UnityEngine;

namespace BehaviourStrategy
{
    public class DefaultSpell : MonoBehaviour, ICastable
    {
        public ISpellInfo Info { get; }
        protected Transform Target;
        protected Animator Animator;
        protected Action CastAnimation;
        protected float Timer;
        
        public void CustomConstructor()
        {
            
        }
        
        public virtual void CastSpell()
        {
            
        }

        
    }
}