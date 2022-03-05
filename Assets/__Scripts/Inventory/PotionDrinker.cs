﻿using System;
using System.Linq;
using __Scripts.GameLoop;
using __Scripts.Inventory;
using __Scripts.Inventory.Abstracts;
using CharacterEntity.CharacterState;
using CharacterEntity.State;
using GameLoop;
using Inventory.Abstracts;

namespace Inventory
{
    public static class PotionDrinker
    {
        public static void DrinkPotion(IInventory inventory, IPotionFlatItem potion)
        {
            var character = Game.GetCharacterByInventory(inventory);
            var stats = potion.PotionFlatInfo.CharacterBaseStats;
            character.Character.ChangeBaseStatsFlatPermanent(stats);
            ChangeInventoryState((IInventoryItem)potion, character);
            EventContainer.OnCharacterStatsChanged(character);
        }
        public static void DrinkPotion(IInventory inventory, IPotionMultiplyItem potion)
        {
            var character = Game.GetCharacterByInventory(inventory);
            var stats = potion.PotionMultiplyInfo.CharacterBaseStats;
            character.Character.ChangeBaseStatsProportionallyPermanent(stats);
            ChangeInventoryState((IInventoryItem)potion, character);
            EventContainer.OnCharacterStatsChanged(character);
        }
        public static void DrinkPotion(IInventory inventory, IPotionUltimateItem potion)
        {
            var character = Game.GetCharacterByInventory(inventory);
            var stats = potion.PotionUltimateInfo.CharacterBaseStats;
            character.Character.ChangeBaseStatsUltimatePermanent(stats);
            ChangeInventoryState((IInventoryItem)potion, character);
            EventContainer.OnCharacterStatsChanged(character);
        }

        public static void DrinkAllPotions(ChampionState character)
        {
            var inventory = character.InventoryHelper.InventoryOrganization.inventory;

            var allCorrectItems = inventory.GetAllItems().Where(item =>
                    item is IPotionFlatItem || item is IPotionMultiplyItem || item is IPotionUltimateItem).ToArray();

            if (allCorrectItems.Length > 0)
            {
                foreach (var item in allCorrectItems)
                {
                    DrinkPotionOfType(character, item, inventory);
                }
                inventory.OnInventoryStateChanged(character);
            }
            EventContainer.OnCharacterStatsChanged(character);
        }

        private static void DrinkPotionOfType(CharacterState character, IInventoryItem potion, InventoryWithSlots inventory)
        {
            switch (potion)
            {
                case IPotionFlatItem potionFlatItem:
                {
                    var stats = potionFlatItem.PotionFlatInfo.CharacterBaseStats;
                    while (potion.State.Amount > 0)
                    {
                        character.Character.ChangeBaseStatsFlatPermanent(stats);
                        potion.State.Amount--;
                    }

                    break;
                }
                case IPotionMultiplyItem potionMultiplyItem:
                {
                    var stats = potionMultiplyItem.PotionMultiplyInfo.CharacterBaseStats;
                    while (potion.State.Amount > 0)
                    {
                        character.Character.ChangeBaseStatsProportionallyPermanent(stats);
                        potion.State.Amount--;
                    }

                    break;
                }
                case IPotionUltimateItem potionUltimateItem:
                {
                    while (potion.State.Amount > 0)
                    {
                        var stats = potionUltimateItem.PotionUltimateInfo.CharacterBaseStats;
                        character.Character.ChangeBaseStatsUltimatePermanent(stats);
                        potion.State.Amount--;
                    }

                    break;
                }
                default:
                    throw new Exception();
            }
            
            if (potion.State.Amount == 0)
            {
                inventory.GetSlotByItem(potion).Clear();
            }
        }

        private static void ChangeInventoryState(IInventoryItem item, ChampionState character)
        {
            item.State.Amount--;
            var inventory = character.InventoryHelper.InventoryOrganization.inventory;

            if (item.State.Amount == 0)
            {
                var slot = inventory.GetSlotByItem(item);
                slot.Clear();
            }

            inventory.OnInventoryStateChanged(character);
        }
    }
}