using __Scripts.Inventory.Abstracts;
using Inventory;
using Inventory.Abstracts;
using Inventory.Info;

namespace __Scripts.Inventory.Items.ArtefactItems
{
    public class ArtefactItem : IInventoryItem, IArtefactItem
    {
        public IInventoryItemInfo Info { get; }
        public IInventoryItemState State { get; }
        public ArtefactInfo ArtefactInfo { get; }
        public string Id => Info.Id;

        public ArtefactItem(IInventoryItemInfo info, ArtefactInfo artefactInfo)
        {
            Info = info;
            ArtefactInfo = artefactInfo;
            State = new InventoryItemState();
        }
        
        public IInventoryItem Clone()
        {
            var clonedItem = new ArtefactItem(Info, ArtefactInfo);
            clonedItem.State.Amount = State.Amount;
            return clonedItem;
        }
    }
}