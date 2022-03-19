using __Scripts.Inventory.Abstracts.Spells;
using CharacterEntity.State;

namespace __Scripts.Behaviour.Abstracts
{
    public interface ICastable
    {
        void CastSpell(CharacterState target);

        void SpellConstructor(ISpellItem info);

    }
}