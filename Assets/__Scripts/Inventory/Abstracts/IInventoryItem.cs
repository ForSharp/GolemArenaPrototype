using System;
using Inventory.Abstracts;

namespace __Scripts.Inventory.Abstracts
{
    public interface IInventoryItem 
    {
        IInventoryItemInfo Info { get; }
        IInventoryItemState State { get; }
        Type Type { get; }
        IInventoryItem Clone();
    }
}
