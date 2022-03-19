using __Scripts.Behaviour.Abstracts;
using __Scripts.CharacterEntity.State;
using __Scripts.Inventory.Abstracts.Spells;
using Behaviour.Abstracts;
using Behaviour.SpellEffects;
using CharacterEntity;
using CharacterEntity.State;
using GameLoop;
using Inventory.Abstracts.Spells;
using Inventory.Items.SpellItems;
using UnityEngine;

namespace Behaviour
{
    public class SummonSpiderSpell : MonoBehaviour, ICastable
    {
        private ChampionState _owner;
        private SummonSpiderItem _info;
        private CharacterState _target;
        private Animator _animator;
        private GameObject _spellEffect;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _owner = GetComponent<ChampionState>();
        }
        public void SpellConstructor(ISpellItem info)
        {
            _info = (SummonSpiderItem)info;
            _spellEffect = _info.SpellInfo.SpellEffect;
        }
        
        
        public void CastSpell(CharacterState target)
        {
            if (_owner.TrySpendMana(_info.SpellInfo.ManaCost))
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
            
            var effect = Instantiate(_spellEffect, _target.transform.position, Quaternion.identity);
            var summonSpiderEffect = effect.GetComponent<SummonSpiderEffect>();
            summonSpiderEffect.Initialize(_info, _owner, _target);
            
        }
    }
}