using System.Collections.Generic;
using FightState;
using GameLoop;
using GolemEntity.ExtraStats;
using Inventory;
using Inventory.Abstracts;
using UnityEngine;

namespace GolemEntity
{
    public class ExtraStatsEditorWithItems
    {
        private GameCharacterState _character;
        private InventoryWithSlots _inventory;
        private Dictionary<IInventorySlot, IExtraStatsProvider> _activeItemEffects;

        public ExtraStatsEditorWithItems(GameCharacterState character)
        {
            _character = character;
            _inventory = character.InventoryHelper.inventoryOrganization.Inventory;

            _inventory.InventoryItemEquipped += HandleItemEquipping;
            _inventory.InventoryItemUnEquipped += HandleItemUnEquipping;
            Game.EndGame += RemoveAllListeners;
        }

        private void HandleItemEquipping(IInventorySlot slot, IInventoryItem item)
        {
            if (item is IArtefactItem)
            {
                
            }
        }
        
        private void HandleItemUnEquipping(IInventorySlot slot, IInventoryItem item)
        {
            
        }

        private void RemoveAllListeners()
        {
            _inventory.InventoryItemEquipped -= HandleItemEquipping;
            _inventory.InventoryItemUnEquipped -= HandleItemUnEquipping;
            Game.EndGame -= RemoveAllListeners;
        }
    }
}