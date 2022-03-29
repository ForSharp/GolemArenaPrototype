using __Scripts.Inventory.Info.Spells;

namespace __Scripts.Inventory.Abstracts.Spells
{
    public interface ISummonSpell
    {
        SummonSpellInfo SummonSpellInfo { get; }
    }
}