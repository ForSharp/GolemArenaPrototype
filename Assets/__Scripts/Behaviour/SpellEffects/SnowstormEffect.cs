using System;
using System.Collections.Generic;
using __Scripts.CharacterEntity.State;
using __Scripts.Inventory.Items.SpellItems;
using UnityEngine;

namespace __Scripts.Behaviour.SpellEffects
{
    public class SnowstormEffect : MonoBehaviour
    {   
        
        [SerializeField] private GameObject snowstormFreezingEffect;
        private ChampionState _state;
        private int _ownerGroupNumber;
        private SnowstormItem _info;

        private Dictionary<CharacterState, SnowstormFreezingEffect> _snowstormFreezingEffects =
            new Dictionary<CharacterState, SnowstormFreezingEffect>();

        public void Initialize(ChampionState character, SnowstormItem info)
        {
            _state = character;
            _ownerGroupNumber = character.Group;
            _info = info;
            
            Invoke(nameof(EndAllEffects), _info.DebuffSpellInfo.DebuffDuration);
        }

        private void EndAllEffects()
        {
            foreach (var effect in _snowstormFreezingEffects)
            {
                if (effect.Value)
                {
                    effect.Value.EndSnowStormFreezing();
                }
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
                    
                    try
                    {
                        stormFreezing.StartSnowstormFreezing();
                    }
                    catch (Exception e)
                    {
                        // ignored
                    }

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