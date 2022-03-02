using System;
using __Scripts.Inventory.Abstracts;

namespace Inventory.Abstracts
{
    public interface IInventory
    {
        int Capacity { get; set; }
        bool IsFull { get; }
        
        IInventoryItem GetItem(Type itemType);
        IInventoryItem[] GetAllItems();
        IInventoryItem[] GetAllItems(Type itemType);
        IInventoryItem[] GetEquippedItems();
        int GetItemAmount(Type itemType);
        bool TryToAdd(object sender, IInventoryItem item);
        bool TryToRemove(object sender, Type item, int amount = 1);
        bool HasItem(Type itemType, out IInventoryItem item);
    }
}