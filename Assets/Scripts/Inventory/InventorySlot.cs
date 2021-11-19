using System;
using Inventory.Abstracts;

namespace Inventory
{
    public class InventorySlot : IInventorySlot
    {
        public bool IsFull => !IsEmpty && Amount == Capacity;

        public bool IsEmpty => Item == null;

        public bool IsEquippingSlot { get; set; }

        public IInventoryItem Item { get; private set; }

        public Type ItemType => Item.Type;

        public int Amount => IsEmpty ? 0 : Item.State.Amount;

        public int Capacity { get; private set; }

        public void SetInventoryItem(IInventoryItem item)
        {
            if (!IsEmpty)
                return;

            Item = item;
            Capacity = item.Info.MaxItemsInInventorySlot;
        }

        public void Clear()
        {
            if (IsEmpty)
                return;

            Item.State.Amount = 0;
            Item = null;
        }
    }
}