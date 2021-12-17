using Behaviour;
using UnityEngine;

namespace CharacterEntity
{
    public class SpellContainer : MonoBehaviour
    {
        public FireballSpell FireballSpell { get; private set; }
        

        private void Start()
        {
            FireballSpell = GetComponent<FireballSpell>();
        }

        
    }
}