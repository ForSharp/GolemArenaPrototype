using System.Collections.Generic;
using CharacterEntity.ExtraStats;
using GameLoop;
using Inventory;
using Inventory.Abstracts;

namespace CharacterEntity
{
    public class ExtraStatsEditorWithItems
    {
        private State.CharacterState _character;
        private InventoryWithSlots _inventory;
        private Dictionary<IInventorySlot, ExtraStatsParameter[]> _activeItemEffects;

        public ExtraStatsEditorWithItems(State.CharacterState character)
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
                _character.Character.AddExtraStatsByItems(artefact.ArtefactInfo.AffectsExtraStats);
                _activeItemEffects.Add(slot, artefact.ArtefactInfo.AffectsExtraStats);
                EventContainer.OnGolemStatsChanged(_character);
            }
        }
        
        private void HandleItemUnEquipping(IInventorySlot slot, IInventoryItem item)
        {
            if (item is IArtefactItem)
            {
                _character.Character.RemoveExtraStatsByItems(_activeItemEffects[slot]);
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