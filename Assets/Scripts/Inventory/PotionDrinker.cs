using System;
using System.Linq;
using FightState;
using Inventory.Abstracts;

namespace Inventory
{
    public static class PotionDrinker
    {
        public static void DrinkAllPotions(GameCharacterState character)
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
        }

        private static void DrinkPotionOfType(GameCharacterState character, IInventoryItem potion, InventoryWithSlots inventory)
        {
            switch (potion)
            {
                case IPotionFlatItem potionFlatItem:
                {
                    var stats = potionFlatItem.PotionFlatInfo.GolemBaseStats;
                    while (potion.State.Amount > 0)
                    {
                        character.Golem.ChangeBaseStatsFlatPermanent(stats);
                        potion.State.Amount--;
                    }

                    break;
                }
                case IPotionMultiplyItem potionMultiplyItem:
                {
                    var stats = potionMultiplyItem.PotionMultiplyInfo.GolemBaseStats;
                    while (potion.State.Amount > 0)
                    {
                        character.Golem.ChangeBaseStatsProportionallyPermanent(stats);
                        potion.State.Amount--;
                    }

                    break;
                }
                case IPotionUltimateItem potionUltimateItem:
                {
                    while (potion.State.Amount > 0)
                    {
                        var stats = potionUltimateItem.PotionUltimateInfo.GolemBaseStats;
                        character.Golem.ChangeBaseStatsUltimatePermanent(stats);
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
    }
}