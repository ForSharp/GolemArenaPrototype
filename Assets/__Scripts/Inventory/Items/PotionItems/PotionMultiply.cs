using System;
using __Scripts.Inventory.Abstracts;
using Inventory.Abstracts;
using Inventory.Info;

namespace Inventory.Items.PotionItems
{
    public class PotionMultiply : IInventoryItem, IPotionMultiplyItem
    {
        public IInventoryItemInfo Info { get; }
        public IInventoryItemState State { get; }
        public PotionMultiplyInfo PotionMultiplyInfo { get; }
        public Type Type => GetType();

        public PotionMultiply(IInventoryItemInfo info, PotionMultiplyInfo potionMultiplyInfo)
        {
            Info = info;
            PotionMultiplyInfo = potionMultiplyInfo;
            State = new InventoryItemState();
        }
        
        public IInventoryItem Clone()
        {
            var clonedPotionMultiply = new PotionMultiply(Info, PotionMultiplyInfo);
            clonedPotionMultiply.State.Amount = State.Amount;
            return clonedPotionMultiply;
        }
    }
}