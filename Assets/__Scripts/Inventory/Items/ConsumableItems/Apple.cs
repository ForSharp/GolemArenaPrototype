using System;
using __Scripts.Inventory.Abstracts;
using Inventory.Abstracts;
using Inventory.Info;

namespace Inventory.Items.ConsumableItems
{
    public class Apple : IInventoryItem, IConsumableHealingItem
    {
        public IInventoryItemInfo Info { get; }
        public IInventoryItemState State { get; }
        public ConsumableHealingInfo ConsumableHealingInfo { get; }
        public Type Type => GetType();


        public Apple(IInventoryItemInfo info, ConsumableHealingInfo consumableHealingInfo)
        {
            Info = info;
            State = new InventoryItemState();
            ConsumableHealingInfo = consumableHealingInfo;
        }

        public IInventoryItem Clone()
        {
            var clonedApple = new Apple(Info, ConsumableHealingInfo);
            clonedApple.State.Amount = State.Amount;
            return clonedApple;
        }
    }
}
