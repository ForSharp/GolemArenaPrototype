using Behaviour.Abstracts;
using Behaviour.SpellEffects;
using CharacterEntity;
using CharacterEntity.State;
using Inventory.Abstracts.Spells;
using Inventory.Items.SpellItems;
using UnityEngine;

namespace Behaviour
{
    public class FreezingSpell : MonoBehaviour, ICastable
    {

        private FreezingItem _info;
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
            _info = (FreezingItem)info;
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
            
            //тут решить, прошел каст или нет (маг точность, маг уклонение)
            
            
            var effect = Instantiate(_spellEffect, _target.transform.position, Quaternion.identity, _target.transform);
            var freezingEffect = effect.GetComponent<FreezingEffect>();
            freezingEffect.Initialize(_character, _target, _info);
            
        }
        
    }
}