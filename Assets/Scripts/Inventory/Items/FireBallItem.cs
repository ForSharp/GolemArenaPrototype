using System;
using Inventory.Abstracts;
using Inventory.Info;

namespace Inventory.Items
{
    public class FireBallItem : IInventoryItem, ISpell
    {
        public IInventoryItemInfo Info { get; }
        public IInventoryItemState State { get; }
        public Type Type { get; }
        public IInventoryItem Clone()
        {
            throw new NotImplementedException();
        }

        public SpellInfo SpellInfo { get; }
    }
}