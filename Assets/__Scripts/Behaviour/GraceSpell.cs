using __Scripts.Behaviour.Abstracts;
using __Scripts.Behaviour.SpellEffects;
using __Scripts.CharacterEntity;
using __Scripts.CharacterEntity.State;
using __Scripts.Inventory.Abstracts.Spells;
using __Scripts.Inventory.Items.SpellItems;
using UnityEngine;

namespace __Scripts.Behaviour
{
    public class GraceSpell : MonoBehaviour, ICastable
    {
        private GraceItem _info;
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
            _info = (GraceItem)info;
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
            if (_target.GetComponentInChildren<GraceEffect>())
            {
                var oldEffect = _target.GetComponentInChildren<GraceEffect>();
                Destroy(oldEffect.gameObject);
            }
            var effect = Instantiate(_spellEffect, _target.transform.position, Quaternion.identity, _target.transform);
            var freezingEffect = effect.GetComponent<GraceEffect>();
            freezingEffect.Initialize(_character, _target, _info);
            
        }
    }
}