using Behaviour;
using UnityEngine;

namespace CharacterEntity
{
    public class SpellContainer : MonoBehaviour
    {
        [HideInInspector] public FireballSpell fireballSpell;
        

        private void Start()
        {
            fireballSpell = GetComponent<FireballSpell>();
        }

        
    }
}