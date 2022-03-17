using System.Linq;
using CharacterEntity.State;
using Inventory.Abstracts;

namespace __Scripts.Inventory
{
    public static class ItemOutfitter
    {
        public static void EquipItems(ChampionState character)
        {
            UnEquipAllItems(character);
            var inventory = character.InventoryHelper.InventoryOrganization.inventory;
            // var allCorrectItems = inventory.GetAllItems().Where(item =>
            //         item is IArtefactItem || item is IConsumableBuffItem || item is IConsumableHealingItem)
            //     .OrderByDescending(item => item.Info.Price);
            var allCorrectItems = inventory.GetAllItems().Where(item =>
                    item.Info.ItemType == ItemType.Artefact).OrderByDescending(item => item.Info.Price).ToArray();

            const int equippingSlotsCount = 6;
            
            for (var i = 0; i < equippingSlotsCount; i++)
            {
                if (i >= allCorrectItems.Length)
                    return;
                
                if (inventory.GetAllEmptyEquippingSlots().Length == 0)
                    return;
                
                inventory.TransitFromSlotToSlot(character, inventory.GetSlotByItem(allCorrectItems[i]), inventory.GetAllEmptyEquippingSlots()[0]);
                inventory.OnInventoryStateChanged(character);
            }
        }

        private static void UnEquipAllItems(ChampionState character)
        {
            var inventory = character.InventoryHelper.InventoryOrganization.inventory;
            foreach (var slot in inventory.GetAllEquippingSlotsWithItems())
            {
                inventory.TransitFromSlotToSlot(character, slot, inventory.GetAllNonEquippingSlots().FirstOrDefault(inventorySlot => inventorySlot.IsEmpty));
            }
        }
    }
}