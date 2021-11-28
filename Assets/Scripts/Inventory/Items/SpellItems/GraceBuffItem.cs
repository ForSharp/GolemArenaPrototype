using System;
using Inventory.Abstracts;
using Inventory.Abstracts.Spells;
using Inventory.Info.Spells;

namespace Inventory.Items.SpellItems
{
    public class GraceBuffItem : IInventoryItem, ISpellItem, IHealingSpell, IBuffSpell
    {
        public IInventoryItemInfo Info { get; }
        public IInventoryItemState State { get; }
        public SpellInfo SpellInfo { get; }
        public HealSpellInfo HealSpellInfo { get; }
        public BuffSpellInfo BuffSpellInfo { get; }
        public Type Type => GetType();

        public GraceBuffItem(IInventoryItemInfo info, SpellInfo spellInfo, HealSpellInfo healSpellInfo,
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
            var clonedGraceBuffItem = new GraceBuffItem(Info, SpellInfo, HealSpellInfo, BuffSpellInfo);
            clonedGraceBuffItem.State.Amount = State.Amount;
            return clonedGraceBuffItem;
        }
    }
}