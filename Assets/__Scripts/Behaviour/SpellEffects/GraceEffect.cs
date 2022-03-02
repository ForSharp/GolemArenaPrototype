using System.Collections;
using CharacterEntity.State;
using Inventory.Items.SpellItems;
using UnityEngine;

namespace Behaviour.SpellEffects
{
    public class GraceEffect : MonoBehaviour
    {
        [SerializeField] private AudioClip graceSound;
        private CharacterState _owner;
        private CharacterState _target;
        private GraceItem _info;
        private AudioSource _audioSource;

        public void Initialize(CharacterState character, CharacterState target, GraceItem info)
        {
            _owner = character;
            _target = target;
            _info = info;
            
            _audioSource = _target.GetComponent<AudioSource>();
            
            AddEffect(_info.BuffSpellInfo.BuffDuration);
        }
        
        private void AddEffect(float duration)
        {
            _audioSource.PlayOneShot(graceSound);

            var image = _info.Info.SpriteIcon;
            var id = _info.Info.Id;
            _target.OnStateEffectAdded(image, duration,true, false, id);
            
            Heal();
            AddBuff();

            StartCoroutine(RemoveEffect(duration));
        }

        private void Heal()
        {
            //можно перед этим изменить значения в зависимости от магической мощи
            
            if (_info.HealSpellInfo.HealIsFlat)
            {
                _target.HealCurrentsFlat(_info.HealSpellInfo.HealedParameter, _info.HealSpellInfo.HealingValue);
            }
            else
            {
                _target.HealCurrentsMultiply(_info.HealSpellInfo.HealedParameter, _info.HealSpellInfo.HealingValue);
            }
        }

        private void AddBuff()
        {
            //можно перед этим изменить значения в зависимости от магической мощи
            
            _target.ConsumablesEater.AddSpellEffect(_info.BuffSpellInfo.AffectsExtraStats, _info.BuffSpellInfo.BuffDuration);
        }
        
        private IEnumerator RemoveEffect(float effectDuration)
        {
            yield return new WaitForSeconds(effectDuration);
            
            Destroy(gameObject);
        }
    }
}