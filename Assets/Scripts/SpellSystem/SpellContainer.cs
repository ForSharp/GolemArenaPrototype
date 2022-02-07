using Behaviour;
using UnityEngine;

namespace SpellSystem
{
    public class SpellContainer : MonoBehaviour
    {
        public FireballSpell FireballSpell { get; private set; }
        
        public FreezingSpell FreezingSpell { get; private set; }
        
        public GraceSpell GraceSpell { get; private set; }
        
        public SnowstormSpell SnowstormSpell { get; private set; }
        
        private void Start()
        {
            FireballSpell = GetComponent<FireballSpell>();
            FreezingSpell = GetComponent<FreezingSpell>();
            GraceSpell = GetComponent<GraceSpell>();
            SnowstormSpell = GetComponent<SnowstormSpell>();

        }

        
    }
}