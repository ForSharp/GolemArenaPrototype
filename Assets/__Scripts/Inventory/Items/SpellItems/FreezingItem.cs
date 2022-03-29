using __Scripts.Inventory.Abstracts;
using __Scripts.Inventory.Abstracts.Spells;
using __Scripts.Inventory.Info.Spells;

namespace __Scripts.Inventory.Items.SpellItems
{
    public class FreezingItem : IInventoryItem, ISpellItem, IPolymorphSpell
    {
        public IInventoryItemInfo Info { get; }
        public IInventoryItemState State { get; }
        public SpellInfo SpellInfo { get; }
        public PolymorphSpellInfo PolymorphSpellInfo { get; }
        public string Id => Info.Id;

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