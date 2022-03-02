using System;
using __Scripts.Inventory.Abstracts;
using Inventory.Abstracts;
using Inventory.Abstracts.Spells;
using Inventory.Info.Spells;

namespace Inventory.Items.SpellItems
{
    public class FireBallItem : IInventoryItem, ISpellItem, IDamagingSpell, IPeriodicDamageSpell
    {
        public IInventoryItemInfo Info { get; }
        public IInventoryItemState State { get; }
        public Type Type => GetType();
        public SpellInfo SpellInfo { get; }
        public DamageSpellInfo DamageSpellInfo { get; }
        public PeriodicDamageSpellInfo PeriodicDamageSpellInfo { get; }

        public FireBallItem(IInventoryItemInfo info, SpellInfo spellInfo, DamageSpellInfo damageSpellInfo, 
            PeriodicDamageSpellInfo periodicDamageSpellInfo)
        {
            Info = info;
            State = new InventoryItemState();
            SpellInfo = spellInfo;
            DamageSpellInfo = damageSpellInfo;
            PeriodicDamageSpellInfo = periodicDamageSpellInfo;
        }
        
        public IInventoryItem Clone()
        {
            var clonedFireBallItem = new FireBallItem(Info, SpellInfo, DamageSpellInfo, PeriodicDamageSpellInfo);
            clonedFireBallItem.State.Amount = State.Amount;
            return clonedFireBallItem;
        }
    }
}