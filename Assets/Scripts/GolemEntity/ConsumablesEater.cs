using System.Collections;
using System.Collections.Generic;
using System.Timers;
using FightState;
using GameLoop;
using GolemEntity.ExtraStats;
using Inventory;
using Inventory.Abstracts;
using Inventory.Info;
using UnityEngine;

namespace GolemEntity
{
    public class ConsumablesEater
    {
        private GameCharacterState _character;
        private InventoryWithSlots _inventory;
        private Dictionary<IInventorySlot, ExtraStatsParameter[]> _activeConsumablesEffects;
        
        public ConsumablesEater(GameCharacterState character)
        {
            _character = character;
            _inventory = character.InventoryHelper.inventoryOrganization.Inventory;

            _activeConsumablesEffects = new Dictionary<IInventorySlot, ExtraStatsParameter[]>();
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
                    _character.Golem.AddTempExtraStats(effect);
                    RemoveEffectAfterDelay(effect, buffItem.ConsumableBuffInfo.BuffDuration);
                    item.State.Amount--;
                    EventContainer.OnGolemStatsChanged(_character);
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
        }
        
        private void RemoveAllTemporaryEffects()
        {
            _character.Golem.RemoveAllTempExtraStats();
        }

        private void RemoveAllListeners()
        {
            _inventory.ConsumableItemUsed -= InventoryOnConsumableItemUsed;
            EventContainer.NewRound -= RemoveAllTemporaryEffects;
            Game.EndGame -= RemoveAllListeners;
        }
    }
}