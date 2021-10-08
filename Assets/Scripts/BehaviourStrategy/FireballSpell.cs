using BehaviourStrategy.SpellEffects;
using UnityEngine;

namespace BehaviourStrategy
{
    public class FireballSpell : AbstractSpell
    {
        public override void CastSpell()
        {
            if (State.TrySpendMana(Info.ManaCost))
            {
                transform.LookAt(Target);
                CastAnimation.Invoke(Animator);
            }
        }

        #region AnimationEvents
        
        private void OnSpellCasted()
        {
            transform.LookAt(Target);
            var fireBall = Instantiate(SpellEffect, transform.position + Vector3.forward + Vector3.up, transform.rotation);
            
            var fireballEffect = fireBall.GetComponent<FireballEffect>();
            fireballEffect.CustomConstructor(State, Info);
        }

        #endregion
    }
}