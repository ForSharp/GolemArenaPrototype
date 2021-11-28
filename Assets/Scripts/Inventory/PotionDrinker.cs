using System;
using System.Linq;
using CharacterEntity.CharacterState;
using GameLoop;
using Inventory.Abstracts;

namespace Inventory
{
    public static class PotionDrinker
    {
        public static void DrinkPotion(IInventory inventory, IPotionFlatItem potion)
        {
            var character = GetCharacterByInventory(inventory);
            var stats = potion.PotionFlatInfo.CharacterBaseStats;
            character.Character.ChangeBaseStatsFlatPermanent(stats);
            ChangeInventoryState((IInventoryItem)potion, character);
            EventContainer.OnGolemStatsChanged(character);
        }
        public static void DrinkPotion(IInventory inventory, IPotionMultiplyItem potion)
        {
            var character = GetCharacterByInventory(inventory);
            var stats = potion.PotionMultiplyInfo.CharacterBaseStats;
            character.Character.ChangeBaseStatsProportionallyPermanent(stats);
            ChangeInventoryState((IInventoryItem)potion, character);
            EventContainer.OnGolemStatsChanged(character);
        }
        public static void DrinkPotion(IInventory inventory, IPotionUltimateItem potion)
        {
            var character = GetCharacterByInventory(inventory);
            var stats = potion.PotionUltimateInfo.CharacterBaseStats;
            character.Character.ChangeBaseStatsUltimatePermanent(stats);
            ChangeInventoryState((IInventoryItem)potion, character);
            EventContainer.OnGolemStatsChanged(character);
        }

        public static void DrinkAllPotions(CharacterState character)
        {
            var inventory = character.InventoryHelper.inventoryOrganization.Inventory;

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
            EventContainer.OnGolemStatsChanged(character);
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

        private static void ChangeInventoryState(IInventoryItem item, CharacterState character)
        {
            item.State.Amount--;
            var inventory = character.InventoryHelper.inventoryOrganization.Inventory;

            if (item.State.Amount == 0)
            {
                var slot = inventory.GetSlotByItem(item);
                slot.Clear();
            }

            inventory.OnInventoryStateChanged(character);
        }

        private static CharacterState GetCharacterByInventory(IInventory inventory)
        {
            return Game.AllCharactersInSession.Find(character =>
                character.InventoryHelper.inventoryOrganization.Inventory == inventory);
        }
    }
}