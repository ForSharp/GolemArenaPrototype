using System;
using Inventory.Abstracts;

namespace Inventory.Items
{
    public class Beer : IInventoryItem
    {
        public IInventoryItemInfo Info { get; }
        public IInventoryItemState State { get; }
        public Type Type => GetType();
        
        public Beer(IInventoryItemInfo info)
        {
            Info = info;
            State = new InventoryItemState();
        }
        public IInventoryItem Clone()
        {
            var clonedBeer = new Beer(Info);
            clonedBeer.State.Amount = State.Amount;
            return clonedBeer;
        }
    }
}