using __Scripts.Inventory.Abstracts;
using __Scripts.Inventory.Abstracts.Spells;
using __Scripts.Inventory.Info.Spells;

namespace __Scripts.Inventory.Items.SpellItems
{
    public class GraceItem : IInventoryItem, ISpellItem, IHealingSpell, IBuffSpell
    {
        public IInventoryItemInfo Info { get; }
        public IInventoryItemState State { get; }
        public SpellInfo SpellInfo { get; }
        public HealSpellInfo HealSpellInfo { get; }
        public BuffSpellInfo BuffSpellInfo { get; }
        public string Id => Info.Id;

        public GraceItem(IInventoryItemInfo info, SpellInfo spellInfo, HealSpellInfo healSpellInfo,
            BuffSpellInfo buffSpellInfo)
        {
            Info = info;
            SpellInfo = spellInfo;
            HealSpellInfo = healSpellInfo;
            BuffSpellInfo = buffSpellInfo;
            State = new InventoryItemState();
        }
        
        public IInventoryItem Clone()
        {
            var clonedGraceItem = new GraceItem(Info, SpellInfo, HealSpellInfo, BuffSpellInfo);
            clonedGraceItem.State.Amount = State.Amount;
            return clonedGraceItem;
        }
    }
}