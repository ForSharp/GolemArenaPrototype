using System;
using __Scripts.Inventory.Abstracts;
using Inventory.Abstracts;
using Inventory.Info;

namespace Inventory.Items.PotionItems
{
    public class PotionFlat : IInventoryItem, IPotionFlatItem
    {
        public IInventoryItemInfo Info { get; }
        public IInventoryItemState State { get; }
        public PotionFlatInfo PotionFlatInfo { get; }
        public string Id => Info.Id;

        public PotionFlat(IInventoryItemInfo info, PotionFlatInfo potionFlatInfo)
        {
            Info = info;
            PotionFlatInfo = potionFlatInfo;
            State = new InventoryItemState();
        }

        public IInventoryItem Clone()
        {
            var clonedPotionFlat = new PotionFlat(Info, PotionFlatInfo);
            clonedPotionFlat.State.Amount = State.Amount;
            return clonedPotionFlat;
        }
    }
}