using System;
using GolemEntity.ExtraStats;
using Inventory.Abstracts;
using Inventory.Info;

namespace Inventory.Items
{
    public class WarHelmet : IInventoryItem, IArtefactItem
    {
        public IInventoryItemInfo Info { get; }
        public IInventoryItemState State { get; }
        public ArtefactInfo ArtefactInfo { get; }
        public Type Type => GetType();

        public WarHelmet(IInventoryItemInfo info, ArtefactInfo artefactInfo)
        {
            Info = info;
            ArtefactInfo = artefactInfo;
            State = new InventoryItemState();
        }
        
        public IInventoryItem Clone()
        {
            var clonedItem = new WarHelmet(Info, ArtefactInfo);
            clonedItem.State.Amount = State.Amount;
            return clonedItem;
        }
    }
}