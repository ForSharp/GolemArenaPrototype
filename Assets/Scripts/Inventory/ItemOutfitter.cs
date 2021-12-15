using System.Collections.Generic;
using System.Linq;
using CharacterEntity.CharacterState;
using CharacterEntity.State;
using Inventory.Abstracts;

namespace Inventory
{
    public static class ItemOutfitter
    {
        public static void EquipItems(CharacterState character)
        {
            UnEquipAllItems(character);
            var inventory = character.InventoryHelper.InventoryOrganization.Inventory;
            // var allCorrectItems = inventory.GetAllItems().Where(item =>
            //         item is IArtefactItem || item is IConsumableBuffItem || item is IConsumableHealingItem)
            //     .OrderByDescending(item => item.Info.Price);
            var allCorrectItems = inventory.GetAllItems().Where(item =>
                    item is IArtefactItem).OrderByDescending(item => item.Info.Price).ToArray();

            const int equippingSlotsCount = 6;
            
            for (var i = 0; i < equippingSlotsCount; i++)
            {
                if (i >= allCorrectItems.Length)
                    return;
                
                if (inventory.GetAllEmptyEquippingSlots().Length == 0)
                    return;
                
                inventory.TransitFromSlotToSlot(character, inventory.GetSlotByItem(allCorrectItems[i]), inventory.GetAllEmptyEquippingSlots()[0]);
            }
            
            
        }

        private static void UnEquipAllItems(CharacterState character)
        {
            var inventory = character.InventoryHelper.InventoryOrganization.Inventory;
            foreach (var slot in inventory.GetAllEquippingSlotsWithItems())
            {
                inventory.TransitFromSlotToSlot(character, slot, inventory.GetAllNonEquippingSlots()[0]);
            }
        }
    }
}