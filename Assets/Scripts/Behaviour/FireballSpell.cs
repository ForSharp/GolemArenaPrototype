using System;
using Behaviour.Abstracts;
using Behaviour.SpellEffects;
using CharacterEntity.CharacterState;
using Inventory.Items;
using Inventory.Items.SpellItems;
using UnityEngine;

namespace Behaviour
{
    public class FireballSpell : MonoBehaviour, ICastable
    {
        private FireBallItem _info;
        private Transform _target;
        private Animator _animator;
        private Action<Animator> _castAnimation;
        private GameObject _spellEffect;
        private CharacterState _state;
        
        public void CustomConstructor(FireBallItem info, Transform target, Animator animator, 
            Action<Animator> castAnimation, CharacterState state)
        {
            _info = info;
            _target = target;
            _animator = animator;
            _castAnimation = castAnimation;
            _spellEffect = _info.SpellInfo.SpellEffect;
            _state = state;
        }
        
        public void CastSpell()
        {
            if (_state.TrySpendMana(_info.SpellInfo.ManaCost))
            {
                transform.LookAt(_target);
                _castAnimation.Invoke(_animator);
            }
        }

        #region AnimationEvents
        
        private void OnSpellCasted()
        {
            transform.LookAt(_target);
            var fireBall = Instantiate(_spellEffect, transform.position + Vector3.forward + Vector3.up, transform.rotation);
            
            var fireballEffect = fireBall.GetComponent<FireballEffect>();
            fireballEffect.CustomConstructor(_state, _info);
        }

        #endregion
    }
}