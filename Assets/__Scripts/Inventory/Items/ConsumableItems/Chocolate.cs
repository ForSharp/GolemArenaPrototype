using System;
using __Scripts.Inventory.Abstracts;
using Inventory.Abstracts;
using Inventory.Info;

namespace Inventory.Items.ConsumableItems
{
    public class Chocolate : IInventoryItem, IConsumableBuffItem
    {
        public IInventoryItemInfo Info { get; }
        public IInventoryItemState State { get; }
        public ConsumableBuffInfo ConsumableBuffInfo { get; }
        public string Id => Info.Id;

        public Chocolate(IInventoryItemInfo info, ConsumableBuffInfo consumableBuffInfo)
        {
            Info = info;
            ConsumableBuffInfo = consumableBuffInfo;
            State = new InventoryItemState();
        }
        
        public IInventoryItem Clone()
        {
            var clonedChocolate = new Chocolate(Info, ConsumableBuffInfo);
            clonedChocolate.State.Amount = State.Amount;
            return clonedChocolate;
        }
    }
}