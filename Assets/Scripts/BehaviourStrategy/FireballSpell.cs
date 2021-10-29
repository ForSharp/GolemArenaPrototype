using System;
using BehaviourStrategy.Abstracts;
using BehaviourStrategy.SpellEffects;
using FightState;
using Inventory.Abstracts;
using Inventory.Abstracts.Spells;
using Inventory.Items;
using UnityEngine;

namespace BehaviourStrategy
{
    public class FireballSpell : MonoBehaviour, ICastable
    {
        private FireBallItem _info;
        private Transform _target;
        private Animator _animator;
        private Action<Animator> _castAnimation;
        private GameObject _spellEffect;
        private GameCharacterState _state;
        
        public void CustomConstructor(FireBallItem info, Transform target, Animator animator, 
            Action<Animator> castAnimation, GameCharacterState state)
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