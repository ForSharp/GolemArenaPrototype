using __Scripts.Behaviour.Abstracts;
using __Scripts.Behaviour.SpellEffects;
using __Scripts.CharacterEntity;
using __Scripts.CharacterEntity.State;
using __Scripts.Inventory.Abstracts.Spells;
using __Scripts.Inventory.Items.SpellItems;
using UnityEngine;

namespace __Scripts.Behaviour
{
    public class SnowstormSpell : MonoBehaviour, ICastable
    {
        private SnowstormItem _info;
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
            _info = (SnowstormItem)info;
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
            
            // var effect = Instantiate(_spellEffect, new Vector3(_target.transform.position.x, 
            //     _target.transform.position.y + 1, _target.transform.position.x), Quaternion.identity);
            var effect = Instantiate(_spellEffect, _target.transform.position, Quaternion.identity);
            var snowstormEffect = effect.GetComponent<SnowstormEffect>();
            snowstormEffect.Initialize(_character, _info);
            
        }
    }
}