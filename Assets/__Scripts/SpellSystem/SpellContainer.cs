using __Scripts.Behaviour;
using UnityEngine;

namespace __Scripts.SpellSystem
{
    public class SpellContainer : MonoBehaviour
    {
        public FireballSpell FireballSpell { get; private set; }
        
        public FreezingSpell FreezingSpell { get; private set; }
        
        public GraceSpell GraceSpell { get; private set; }
        
        public SnowstormSpell SnowstormSpell { get; private set; }
        
        public SummonSpiderSpell SummonSpiderSpell { get; private set; }
        
        private void Start()
        {
            FireballSpell = GetComponent<FireballSpell>();
            FreezingSpell = GetComponent<FreezingSpell>();
            GraceSpell = GetComponent<GraceSpell>();
            SnowstormSpell = GetComponent<SnowstormSpell>();
            SummonSpiderSpell = GetComponent<SummonSpiderSpell>();
        }

        
    }
}