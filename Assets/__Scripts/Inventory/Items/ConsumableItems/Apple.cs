using System;
using __Scripts.Inventory.Abstracts;
using Inventory;
using Inventory.Abstracts;
using Inventory.Info;

namespace __Scripts.Inventory.Items.ConsumableItems
{
    public class Apple : IInventoryItem, IConsumableHealingItem
    {
        public IInventoryItemInfo Info { get; }
        public IInventoryItemState State { get; }
        public ConsumableHealingInfo ConsumableHealingInfo { get; }
        public string Id => Info.Id;


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
