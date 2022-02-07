using System.Collections;
using CharacterEntity.ExtraStats;
using CharacterEntity.State;
using GameLoop;
using Inventory.Items.SpellItems;
using UnityEngine;

namespace Behaviour.SpellEffects
{
    public class SnowstormFreezingEffect : MonoBehaviour
    {
        [SerializeField] private AudioClip freezeSound;
        
        private CharacterState _state;
        private CharacterState _target;
        private SnowstormItem _info;
        private ExtraStatsParameter[] _debuff;
        private AudioSource _audioSource;
        
        public void Initialize(CharacterState state, CharacterState target, SnowstormItem info)
        {
            _state = state;
            _target = target;
            _info = info;
            _audioSource = _target.GetComponent<AudioSource>();
        }

        public void StartSnowstormFreezing()
        {
            _audioSource.PlayOneShot(freezeSound);
            
            var image = _info.Info.SpriteIcon;
            var id = _info.Info.Id;
            _target.OnStateEffectAdded(image, 10000,true, false, id);
            _debuff = _info.DebuffSpellInfo.AffectsExtraStats;
            //можно перед этим изменить значения в зависимости от магической мощи
            
            _target.ConsumablesEater.AddSpellEffect(_debuff, 10000);
            
            StartCoroutine(SetPeriodicDamage(_state, _target, _info.PeriodicDamageSpellInfo.PeriodicDamagingValue));
        }

        private IEnumerator SetPeriodicDamage(CharacterState attacker, CharacterState target, float periodicDamage)
        {
            yield return new WaitForSeconds(1);
            
            target.TakeDamage(periodicDamage, attacker.roundStatistics);
            EventContainer.OnMagicDamageReceived(attacker, target, periodicDamage, true);

            StartCoroutine(SetPeriodicDamage(attacker, target, periodicDamage));
        }
        
        public void EndSnowStormFreezing()
        {
            var image = _info.Info.SpriteIcon;
            var id = _info.Info.Id;
            _target.OnStateEffectAdded(image, 0.01f,true, false, id);
            _target.ConsumablesEater.AddSpellEffect(_debuff, 0.01f);
            Destroy(gameObject);
        }
    }
}