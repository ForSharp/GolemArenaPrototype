﻿using System.Collections.Generic;
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

            _activeItemEffects = new Dictionary<IInventorySlot, IExtraStatsProvider>();

            _inventory.InventoryItemEquipped += HandleItemEquipping;
            _inventory.InventoryItemUnEquipped += HandleItemUnEquipping;
            Game.EndGame += RemoveAllListeners;
        }

        private void HandleItemEquipping(IInventorySlot slot, IInventoryItem item)
        {
            if (item is IArtefactItem artefact)
            {
                var effect = _character.Golem.AddExtraStatsByItems(artefact.ArtefactInfo.AffectsExtraStats);
                _activeItemEffects.Add(slot, effect);
                EventContainer.OnGolemStatsChanged(_character);
            }
        }
        
        private void HandleItemUnEquipping(IInventorySlot slot, IInventoryItem item)
        {
            if (item is IArtefactItem artefact)
            {
                _character.Golem.RemoveExtraStatsByItems(_activeItemEffects[slot]);
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