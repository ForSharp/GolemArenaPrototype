using System;

namespace __Scripts.Inventory.Abstracts
{
    public interface IInventorySlot
    {
        bool IsFull { get; }
        bool IsEmpty { get; }
        bool IsEquippingSlot { get; set; }
        IInventoryItem Item { get; }
        string ItemId { get; }
        int Amount { get; }
        int Capacity { get; }

        void SetInventoryItem(IInventoryItem item);
        void Clear();
    }
}