using __Scripts.Behaviour.SpellEffects;
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
        private ChampionState _character;
        private CharacterState _target;
        private Animator _animator;
        private GameObject _spellEffect;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _character = GetComponent<ChampionState>();
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
                AnimationChanger.SetCastSpell(_animator);
                _target = target;
                
                Invoke(nameof(ContinueCast), 1);
            }
        }
        
        private void ContinueCast()
        {
            transform.LookAt(_target.transform);
            var fireBall = Instantiate(_spellEffect, new Vector3(transform.localPosition.x + 1, 
                transform.localPosition.y + 1, transform.localPosition.z), Quaternion.identity, transform);
            
            var fireballEffect = fireBall.GetComponent<FireballEffect>();
            fireballEffect.Initialize(_character, _target, _info);
        }

    }
}