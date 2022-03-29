using __Scripts.Inventory.Info.Spells;

namespace __Scripts.Inventory.Abstracts.Spells
{
    public interface ISpellItem
    {
        SpellInfo SpellInfo { get; }
    }
}