using __Scripts.Behaviour.Abstracts;
using __Scripts.Behaviour.SpellEffects;
using __Scripts.CharacterEntity;
using __Scripts.CharacterEntity.State;
using __Scripts.Inventory.Abstracts.Spells;
using __Scripts.Inventory.Items.SpellItems;
using UnityEngine;

namespace __Scripts.Behaviour
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
            AnimationChanger.SetCastSpell(_animator);
            _target = target;

            transform.LookAt(_target.transform);
            transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);

            Invoke(nameof(ContinueCast), 1.5f);
        }


        private void ContinueCast()
        {
            //тут решить, прошел каст или нет (маг точность, маг уклонение)

            var effect = Instantiate(_spellEffect, _target.transform.position, Quaternion.identity, _target.transform);
            var freezingEffect = effect.GetComponent<FreezingEffect>();
            freezingEffect.Initialize(_character, _target, _info);
        }
    }
}