using System;

namespace Inventory.Abstracts
{
    public interface IInventoryItem 
    {
        IInventoryItemInfo Info { get; }
        IInventoryItemState State { get; }
        Type Type { get; }
        IInventoryItem Clone();
    }
}
