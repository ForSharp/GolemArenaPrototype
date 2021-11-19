using System;
using Inventory.Abstracts;
using Inventory.Abstracts.Spells;
using Inventory.Info.Spells;

namespace Inventory.Items.SpellItems
{
    public class SnowstormItem : IInventoryItem, ISpellItem, IPeriodicDamageSpell, IDebuffSpell
    {
        public IInventoryItemInfo Info { get; }
        public IInventoryItemState State { get; }
        public SpellInfo SpellInfo { get; }
        public PeriodicDamageSpellInfo PeriodicDamageSpellInfo { get; }
        public DebuffSpellInfo DebuffSpellInfo { get; }
        public Type Type => GetType();

        public SnowstormItem(IInventoryItemInfo info, SpellInfo spellInfo, PeriodicDamageSpellInfo periodicDamageSpellInfo,
            DebuffSpellInfo debuffSpellInfo)
        {
            Info = info;
            SpellInfo = spellInfo;
            PeriodicDamageSpellInfo = periodicDamageSpellInfo;
            DebuffSpellInfo = debuffSpellInfo;
            State = new InventoryItemState();
        }

        public IInventoryItem Clone()
        {
            var clonedSnowstormItem = new SnowstormItem(Info, SpellInfo, PeriodicDamageSpellInfo, DebuffSpellInfo);
            clonedSnowstormItem.State.Amount = State.Amount;
            return clonedSnowstormItem;
        }
    }
}