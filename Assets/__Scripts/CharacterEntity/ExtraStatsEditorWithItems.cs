using System.Collections.Generic;
using __Scripts.CharacterEntity.ExtraStats;
using __Scripts.CharacterEntity.State;
using __Scripts.GameLoop;
using __Scripts.Inventory;
using __Scripts.Inventory.Abstracts;

namespace __Scripts.CharacterEntity
{
    public class ExtraStatsEditorWithItems
    {
        private ChampionState _character;
        private InventoryWithSlots _inventory;
        private Dictionary<IInventorySlot, ExtraStatsParameter[]> _activeItemEffects;

        public ExtraStatsEditorWithItems(ChampionState character)
        {
            _character = character;
            _inventory = character.InventoryHelper.InventoryOrganization.inventory;

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
                EventContainer.OnCharacterStatsChanged(_character);
            }
        }
        
        private void HandleItemUnEquipping(IInventorySlot slot, IInventoryItem item)
        {
            if (item is IArtefactItem)
            {
                _character.Character.RemoveExtraStatsByItems(_activeItemEffects[slot]);
                _activeItemEffects.Remove(slot);
                EventContainer.OnCharacterStatsChanged(_character);
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