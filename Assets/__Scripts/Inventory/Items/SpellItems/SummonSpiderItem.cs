using __Scripts.Inventory.Abstracts;
using __Scripts.Inventory.Abstracts.Spells;
using __Scripts.Inventory.Info.Spells;

namespace __Scripts.Inventory.Items.SpellItems
{
    public class SummonSpiderItem : IInventoryItem, ISpellItem, ISummonSpell
    {
        public IInventoryItemInfo Info { get; }
        public IInventoryItemState State { get; }
        public SpellInfo SpellInfo { get; }
        public SummonSpellInfo SummonSpellInfo { get; }
        public string Id => Info.Id;

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