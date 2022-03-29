using System;
using __Scripts.Inventory.Abstracts;
using __Scripts.Inventory.Info;

namespace __Scripts.Inventory.Items.ConsumableItems
{
    public class ConsumableHealingItem : IInventoryItem, IConsumableHealingItem
    {
        public IInventoryItemInfo Info { get; }
        public IInventoryItemState State { get; }
        public ConsumableHealingInfo ConsumableHealingInfo { get; }
        public string Id => Info.Id;


        public ConsumableHealingItem(IInventoryItemInfo info, ConsumableHealingInfo consumableHealingInfo)
        {
            Info = info;
            State = new InventoryItemState();
            ConsumableHealingInfo = consumableHealingInfo;
        }

        public IInventoryItem Clone()
        {
            var item = new ConsumableHealingItem(Info, ConsumableHealingInfo);
            item.State.Amount = State.Amount;
            return item;
        }
    }
}
