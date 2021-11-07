using Inventory.Info.Spells;

namespace Inventory.Abstracts.Spells
{
    public interface ISpellItem
    {
        SpellInfo SpellInfo { get; }
    }
}