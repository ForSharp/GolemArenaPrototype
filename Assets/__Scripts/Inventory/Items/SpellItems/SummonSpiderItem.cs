using System;
using __Scripts.Inventory.Abstracts;
using Inventory.Abstracts;
using Inventory.Abstracts.Spells;
using Inventory.Info.Spells;

namespace Inventory.Items.SpellItems
{
    public class SummonSpiderItem : IInventoryItem, ISpellItem, ISummonSpell
    {
        public IInventoryItemInfo Info { get; }
        public IInventoryItemState State { get; }
        public SpellInfo SpellInfo { get; }
        public SummonSpellInfo SummonSpellInfo { get; }
        public Type Type => GetType();

        public SummonSpiderItem(IInventoryItemInfo info, SpellInfo spellInfo, SummonSpellInfo summonSpellInfo)
        {
            Info = info;
            SpellInfo = spellInfo;
            SummonSpellInfo = summonSpellInfo;
            State = new InventoryItemState();
        }

        public IInventoryItem Clone()
        {
            var clonedSummonSpiderItem = new SummonSpiderItem(Info, SpellInfo, SummonSpellInfo);
            clonedSummonSpiderItem.State.Amount = State.Amount;
            return clonedSummonSpiderItem;
        }
    }
}