using Behaviour.Abstracts;
using CharacterEntity.State;
using Inventory.Abstracts.Spells;
using UnityEngine;

namespace Behaviour
{
    public class SummonSpiderSpell : MonoBehaviour, ICastable
    {
        public void SpellConstructor(ISpellItem info)
        {
            throw new System.NotImplementedException();
        }

        public void CastSpell(CharacterState target)
        {
            throw new System.NotImplementedException();
        }
    }
}