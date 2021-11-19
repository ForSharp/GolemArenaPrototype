using Inventory.Info.Spells;

namespace Inventory.Abstracts.Spells
{
    public interface ISummonSpell
    {
        SummonSpellInfo SummonSpellInfo { get; }
    }
}