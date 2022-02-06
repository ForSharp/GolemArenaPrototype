using Behaviour.Abstracts;
using CharacterEntity.State;
using Inventory.Abstracts.Spells;
using UnityEngine;

namespace Behaviour
{
    public class SnowstormSpell : MonoBehaviour, ICastable
    {
        //pf_vfx-ult_demo_psys_loop_snowstorm2
        public void CastSpell(CharacterState target)
        {
            throw new System.NotImplementedException();
        }

        public void SpellConstructor(ISpellItem info)
        {
            throw new System.NotImplementedException();
        }
    }
}