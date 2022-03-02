using Inventory.Info.Spells;

namespace Inventory.Abstracts.Spells
{
    public interface IPeriodicDamageSpell
    {
        PeriodicDamageSpellInfo PeriodicDamageSpellInfo { get; }
    }
}