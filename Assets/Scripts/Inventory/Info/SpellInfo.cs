using BehaviourStrategy;
using UnityEngine;

namespace Inventory.Info
{
    [CreateAssetMenu(fileName = "SpellInfo", menuName = "Gameplay/Items/Create New SpellInfo")]
    public class SpellInfo : ScriptableObject
    {
        [SerializeField] private SpellType spellType;
        [SerializeField] private GameObject spellEffect;
        
        public SpellType SpellType => spellType;
        public GameObject SpellEffect => spellEffect;
    }
}