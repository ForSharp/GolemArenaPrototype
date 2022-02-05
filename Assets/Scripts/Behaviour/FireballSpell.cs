using Behaviour.Abstracts;
using Behaviour.SpellEffects;
using CharacterEntity;
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

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _character = GetComponent<CharacterState>();
        }

        public void SpellConstructor(ISpellItem info)
        {
            _info = (FireBallItem)info;
            _spellEffect = _info.SpellInfo.SpellEffect;
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
            var fireBall = Instantiate(_spellEffect, transform.localPosition + Vector3.forward + Vector3.up, transform.localRotation);
            
            var fireballEffect = fireBall.GetComponent<FireballEffect>();
            fireballEffect.CustomConstructor(_character, _info, _target);
        }

        #endregion
    }
}