using Inventory.Info.Spells;

namespace Inventory.Abstracts.Spells
{
    public interface ISpell
    {
        SpellInfo SpellInfo { get; }
    }
}