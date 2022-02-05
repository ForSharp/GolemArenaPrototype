using System.Collections;
using System.Collections.Generic;
using CharacterEntity.ExtraStats;
using GameLoop;
using Inventory;
using Inventory.Abstracts;
using Optimization;
using UnityEngine;

namespace CharacterEntity
{
    public class ConsumablesEater
    {
        private readonly State.CharacterState _character;
        private readonly InventoryWithSlots _inventory;
        private readonly Dictionary<ExtraStatsParameter[], Coroutine> _effects = new Dictionary<ExtraStatsParameter[], Coroutine>();

        public ConsumablesEater(State.CharacterState character)
        {
            _character = character;
            _inventory = character.InventoryHelper.InventoryOrganization.inventory;

            _inventory.ConsumableItemUsed += InventoryOnConsumableItemUsed;
            EventContainer.NewRound += RemoveAllTemporaryEffects;
            Game.EndGame += RemoveAllListeners;
        }

        private void InventoryOnConsumableItemUsed(IInventorySlot slot, IInventoryItem item)
        {
            if (_character.IsDead)
                return;
            
            switch (item)
            {
                case IConsumableBuffItem buffItem:
                    var effect = buffItem.ConsumableBuffInfo.AffectsExtraStats;
                    if (_effects.ContainsKey(effect))
                    {
                        CoroutineManager.StopRoutine(_effects[effect]);
                        _effects.Remove(effect);
                        _character.Character.RemoveTempExtraStats(effect);
                        _character.StateBar.StopEffect(item.Info.Id);
                    }
                    _character.OnStateEffectAdded(item.Info.SpriteIcon, buffItem.ConsumableBuffInfo.BuffDuration, true, item.Info.Id);
                    _character.Character.AddTempExtraStats(effect);
                    item.State.Amount--;
                    EventContainer.OnCharacterStatsChanged(_character);
                    var coroutine = CoroutineManager.StartRoutine(RemoveEffectAfterDelay(effect, buffItem.ConsumableBuffInfo.BuffDuration));
                    _effects.Add(effect, coroutine);
                    break;
                case IConsumableHealingItem healingItem:
                    if (healingItem.ConsumableHealingInfo.HealIsFlat)
                    {
                        _character.HealCurrentsFlat(healingItem.ConsumableHealingInfo.HealedParameter, healingItem.ConsumableHealingInfo.HealingValue);
                    }
                    else
                    {
                        _character.HealCurrentsMultiply(healingItem.ConsumableHealingInfo.HealedParameter, healingItem.ConsumableHealingInfo.HealingValue);
                    }
                    item.State.Amount--;
                    break;
            }
            
            if (item.State.Amount == 0)
            {
                slot.Clear();
            }
            
            _inventory.OnInventoryStateChanged(_character);
        }

        private IEnumerator RemoveEffectAfterDelay(ExtraStatsParameter[] effect, float duration)
        {
            yield return new WaitForSeconds(duration);
            
            _character.Character.RemoveTempExtraStats(effect);
            EventContainer.OnCharacterStatsChanged(_character);
            _effects.Remove(effect);
        }
        
        private void RemoveAllTemporaryEffects()
        {
            _character.Character.RemoveAllTempExtraStats();
            EventContainer.OnCharacterStatsChanged(_character);
            _effects.Clear();
        }

        private void RemoveAllListeners()
        {
            _inventory.ConsumableItemUsed -= InventoryOnConsumableItemUsed;
            EventContainer.NewRound -= RemoveAllTemporaryEffects;
            Game.EndGame -= RemoveAllListeners;
        }
    }
}