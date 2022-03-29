using __Scripts.Inventory.Info.Spells;

namespace __Scripts.Inventory.Abstracts.Spells
{
    public interface IDebuffSpell
    {
        DebuffSpellInfo DebuffSpellInfo { get; }
    }
}