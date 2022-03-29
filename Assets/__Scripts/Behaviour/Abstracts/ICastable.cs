using __Scripts.CharacterEntity.State;
using __Scripts.Inventory.Abstracts.Spells;

namespace __Scripts.Behaviour.Abstracts
{
    public interface ICastable
    {
        void CastSpell(CharacterState target);

        void SpellConstructor(ISpellItem info);

    }
}