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
        private Dictionary<IInventorySlot, ExtraStatsParameter[]> _activeItemEffects;

        public ExtraStatsEditorWithItems(GameCharacterState character)
        {
            _character = character;
            _inventory = character.InventoryHelper.inventoryOrganization.Inventory;

            _activeItemEffects = new Dictionary<IInventorySlot, ExtraStatsParameter[]>();

            _inventory.InventoryItemEquipped += HandleItemEquipping;
            _inventory.InventoryItemUnEquipped += HandleItemUnEquipping;
            Game.EndGame += RemoveAllListeners;
        }

        private void HandleItemEquipping(IInventorySlot slot, IInventoryItem item)
        {
            if (item is IArtefactItem artefact)
            {
                _character.Golem.AddExtraStatsByItems(artefact.ArtefactInfo.AffectsExtraStats);
                _activeItemEffects.Add(slot, artefact.ArtefactInfo.AffectsExtraStats);
                EventContainer.OnGolemStatsChanged(_character);
            }
        }
        
        private void HandleItemUnEquipping(IInventorySlot slot, IInventoryItem item)
        {
            if (item is IArtefactItem)
            {
                _character.Golem.RemoveExtraStatsByItems(_activeItemEffects[slot]);
                _activeItemEffects.Remove(slot);
                EventContainer.OnGolemStatsChanged(_character);
            }
        }

        private void RemoveAllListeners()
        {
            _inventory.InventoryItemEquipped -= HandleItemEquipping;
            _inventory.InventoryItemUnEquipped -= HandleItemUnEquipping;
            Game.EndGame -= RemoveAllListeners;
        }
    }
}