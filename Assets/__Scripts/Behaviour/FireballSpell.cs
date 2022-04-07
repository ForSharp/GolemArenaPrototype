using __Scripts.Behaviour.Abstracts;
using __Scripts.Behaviour.SpellEffects;
using __Scripts.CharacterEntity;
using __Scripts.CharacterEntity.State;
using __Scripts.Inventory.Abstracts.Spells;
using __Scripts.Inventory.Items.SpellItems;
using UnityEngine;

namespace __Scripts.Behaviour
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
                AnimationChanger.SetCastSpell(_animator);
                _target = target;
                
                transform.LookAt(_target.transform);
                transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);
                
                Invoke(nameof(ContinueCast), 1.5f);
            }
        }
        
        private void ContinueCast()
        {
            var fireBall = Instantiate(_spellEffect, new Vector3(transform.localPosition.x + 1, 
                transform.localPosition.y + 1, transform.localPosition.z), Quaternion.identity, transform);
            
            var fireballEffect = fireBall.GetComponent<FireballEffect>();
            fireballEffect.Initialize(_character, _target, _info);
        }

    }
}