using Inventory.Info.Spells;

namespace Inventory.Abstracts.Spells
{
    public interface IDebuffSpell
    {
        DebuffSpellInfo DebuffSpellInfo { get; }
    }
}