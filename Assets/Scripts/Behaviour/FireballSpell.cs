using System;
using Behaviour.Abstracts;
using Behaviour.SpellEffects;
using CharacterEntity;
using CharacterEntity.CharacterState;
using CharacterEntity.State;
using Inventory.Abstracts.Spells;
using Inventory.Items.SpellItems;
using UnityEngine;

namespace Behaviour
{
    public class FireballSpell : MonoBehaviour, ICastable
    {
        private FireBallItem _info;
        private CharacterState _character;
        private CharacterState _target;
        private Animator _animator;
        private GameObject _spellEffect;

        public void SpellConstructor(ISpellItem info, CharacterState character, Animator animator)
        {
            _info = (FireBallItem)info;
            _animator = animator;
            _spellEffect = _info.SpellInfo.SpellEffect;
            _character = character;
        }

        public void CastSpell(CharacterState target)
        {
            if (_character.TrySpendMana(_info.SpellInfo.ManaCost))
            {
                transform.LookAt(target.transform);
                AnimationChanger.SetCastFireBall(_animator);
                _target = target;
            }
        }

        #region AnimationEvents
        
        private void OnSpellCasted()
        {
            transform.LookAt(_target.transform);
            var fireBall = Instantiate(_spellEffect, transform.position + Vector3.forward + Vector3.up, transform.rotation);
            
            var fireballEffect = fireBall.GetComponent<FireballEffect>();
            fireballEffect.CustomConstructor(_character, _info);
        }

        #endregion
    }
}