using Inventory.Info.Spells;

namespace Inventory.Abstracts.Spells
{
    public interface IHealingSpell
    {
        HealSpellInfo HealSpellInfo { get; }
    }
}