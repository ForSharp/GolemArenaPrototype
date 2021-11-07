using System;
using Inventory.Abstracts;
using Inventory.Abstracts.Spells;
using Inventory.Info;
using Inventory.Info.Spells;

namespace Inventory.Items.SpellItems
{
    public class FreezingItem : IInventoryItem, ISpellItem, IPolymorphSpell
    {
        public IInventoryItemInfo Info { get; }
        public IInventoryItemState State { get; }
        public SpellInfo SpellInfo { get; }
        public PolymorphSpellInfo PolymorphSpellInfo { get; }
        public Type Type => GetType();

        public FreezingItem(IInventoryItemInfo info, SpellInfo spellInfo, PolymorphSpellInfo polymorphSpellInfo)
        {
            Info = info;
            SpellInfo = spellInfo;
            PolymorphSpellInfo = polymorphSpellInfo;
            State = new InventoryItemState();
        }

        public IInventoryItem Clone()
        {
            var clonedFreezingItem = new FreezingItem(Info, SpellInfo, PolymorphSpellInfo);
            clonedFreezingItem.State.Amount = State.Amount;
            return clonedFreezingItem;
        }
    }
}