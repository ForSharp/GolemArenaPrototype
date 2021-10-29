using Inventory.Info.Spells;

namespace Inventory.Abstracts.Spells
{
    public interface IDamagingSpell
    {
        DamageSpellInfo DamageSpellInfo { get; }
    }
}