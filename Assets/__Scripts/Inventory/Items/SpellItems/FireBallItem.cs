using __Scripts.Inventory.Abstracts;
using __Scripts.Inventory.Abstracts.Spells;
using __Scripts.Inventory.Info.Spells;

namespace __Scripts.Inventory.Items.SpellItems
{
    public class FireBallItem : IInventoryItem, ISpellItem, IDamagingSpell, IPeriodicDamageSpell
    {
        public IInventoryItemInfo Info { get; }
        public IInventoryItemState State { get; }
        public string Id => Info.Id;
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