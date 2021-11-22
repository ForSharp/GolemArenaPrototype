using System.Collections;
using System.Collections.Generic;
using FightState;
using GameLoop;
using GolemEntity.ExtraStats;
using Inventory;
using Inventory.Abstracts;
using Optimization;
using UnityEngine;

namespace GolemEntity
{
    public class ConsumablesEater
    {
        private readonly GameCharacterState _character;
        private readonly InventoryWithSlots _inventory;
        private readonly List<ExtraStatsParameter[]> _effects = new List<ExtraStatsParameter[]>();

        public ConsumablesEater(GameCharacterState character)
        {
            _character = character;
            _inventory = character.InventoryHelper.inventoryOrganization.Inventory;

            _inventory.ConsumableItemUsed += InventoryOnConsumableItemUsed;
            EventContainer.NewRound += RemoveAllTemporaryEffects;
            Game.EndGame += RemoveAllListeners;
        }

        private void InventoryOnConsumableItemUsed(IInventorySlot slot, IInventoryItem item)
        {
            switch (item)
            {
                case IConsumableBuffItem buffItem:
                    var effect = buffItem.ConsumableBuffInfo.AffectsExtraStats;
                    if (_effects.Contains(effect))
                    {
                        _character.Golem.RemoveTempExtraStats(effect);
                    }
                    _character.Golem.AddTempExtraStats(effect);
                    CoroutineStarter.StartRoutine(RemoveEffectAfterDelay(effect, buffItem.ConsumableBuffInfo.BuffDuration));
                    item.State.Amount--;
                    EventContainer.OnGolemStatsChanged(_character);
                    _effects.Add(effect);
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
            _character.Golem.RemoveTempExtraStats(effect);
            EventContainer.OnGolemStatsChanged(_character);
            _effects.Remove(effect);
        }
        
        private void RemoveAllTemporaryEffects()
        {
            _character.Golem.RemoveAllTempExtraStats();
            EventContainer.OnGolemStatsChanged(_character);
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