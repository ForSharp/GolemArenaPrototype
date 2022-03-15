using __Scripts.Inventory.Abstracts;
using Inventory;
using Inventory.Abstracts;
using Inventory.Info;

namespace __Scripts.Inventory.Items.ConsumableItems
{
    public class ConsumableBuffItem : IInventoryItem, IConsumableBuffItem
    {
        public IInventoryItemInfo Info { get; }
        public IInventoryItemState State { get; }
        public ConsumableBuffInfo ConsumableBuffInfo { get; }
        public string Id => Info.Id;

        public ConsumableBuffItem(IInventoryItemInfo info, ConsumableBuffInfo consumableBuffInfo)
        {
            Info = info;
            ConsumableBuffInfo = consumableBuffInfo;
            State = new InventoryItemState();
        }
        
        public IInventoryItem Clone()
        {
            var item = new ConsumableBuffItem(Info, ConsumableBuffInfo);
            item.State.Amount = State.Amount;
            return item;
        }
    }
}