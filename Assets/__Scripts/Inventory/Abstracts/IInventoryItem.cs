using System;

namespace __Scripts.Inventory.Abstracts
{
    public interface IInventoryItem 
    {
        IInventoryItemInfo Info { get; }
        IInventoryItemState State { get; }
        string Id { get; }
        IInventoryItem Clone();
    }
}
