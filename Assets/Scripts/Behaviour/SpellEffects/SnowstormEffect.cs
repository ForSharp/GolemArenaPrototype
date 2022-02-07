using System;
using System.Collections.Generic;
using System.Linq;
using CharacterEntity.CharacterState;
using CharacterEntity.State;
using Inventory.Items.SpellItems;
using UnityEngine;

namespace Behaviour.SpellEffects
{
    public class SnowstormEffect : MonoBehaviour
    {   
        
        [SerializeField] private GameObject snowstormFreezingEffect;
        private CharacterState _state;
        private CharacterState _target;
        private int _ownerGroupNumber;
        private SnowstormItem _info;

        private Dictionary<CharacterState, SnowstormFreezingEffect> _snowstormFreezingEffects =
            new Dictionary<CharacterState, SnowstormFreezingEffect>();

        public void Initialize(CharacterState character, CharacterState target, SnowstormItem info)
        {
            _state = character;
            _target = target;
            _ownerGroupNumber = character.Group;
            _info = info;
            
            Invoke(nameof(EndAllEffects), _info.DebuffSpellInfo.DebuffDuration);
        }

        private void EndAllEffects()
        {
            foreach (var effect in _snowstormFreezingEffects)
            {
                effect.Value.EndSnowStormFreezing();
            }

            Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider collision)
        {
            
            if (collision.gameObject.TryGetComponent<CharacterState>(out var state))
            {
                
                if (state.Group != _ownerGroupNumber)
                {
                    if (_snowstormFreezingEffects.ContainsKey(state))
                    {
                        return;
                    }
                    
                    var obj = Instantiate(snowstormFreezingEffect, state.transform.position, Quaternion.identity,
                        state.transform);
                    var stormFreezing = obj.GetComponent<SnowstormFreezingEffect>();
                    stormFreezing.Initialize(_state, state, _info);
                    stormFreezing.StartSnowstormFreezing();
                    _snowstormFreezingEffects.Add(state, stormFreezing);
                }
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.TryGetComponent<CharacterState>(out var state))
            {
                if (state.Group != _ownerGroupNumber)
                {
                    if (!_snowstormFreezingEffects.ContainsKey(state))
                    {
                        return;
                    }
        
                    _snowstormFreezingEffects.TryGetValue(state, out var stormFreezing);
                    if (stormFreezing != null) stormFreezing.EndSnowStormFreezing();
                    _snowstormFreezingEffects.Remove(state);
                }
            }
        }

    }
}