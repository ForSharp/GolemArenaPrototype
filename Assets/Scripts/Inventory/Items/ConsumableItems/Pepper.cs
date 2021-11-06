using System;
using Inventory.Abstracts;

namespace Inventory.Items
{
    public class Pepper : IInventoryItem
    {
        public IInventoryItemInfo Info { get; }
        public IInventoryItemState State { get; }
        public Type Type => GetType();

        public Pepper(IInventoryItemInfo info)
        {
            Info = info;
            State = new InventoryItemState();
        }
        public IInventoryItem Clone()
        {
            var clonedPepper = new Pepper(Info);
            clonedPepper.State.Amount = State.Amount;
            return clonedPepper;
        }
    }
}