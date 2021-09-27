using UnityEngine;

namespace BehaviourStrategy
{
    public abstract class AbstractSpell : MonoBehaviour, ICastable
    {
        public SpellType spellType;
        
        public void CastSpell()
        {
            
        }
    }
}