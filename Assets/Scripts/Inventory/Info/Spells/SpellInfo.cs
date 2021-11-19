using BehaviourStrategy;
using UnityEngine;

namespace Inventory.Info.Spells
{
    [CreateAssetMenu(fileName = "SpellInfo", menuName = "Gameplay/Spells/Create New SpellInfo")]
    public class SpellInfo : ScriptableObject
    {
        [SerializeField] private SpellType spellType;
        [SerializeField] private GameObject spellEffect;
        [SerializeField] private float manaCost;
        [SerializeField] private float cooldown;
        [SerializeField] private int spellLvl;
        
        public SpellType SpellType => spellType;
        public GameObject SpellEffect => spellEffect;
        public float ManaCost => manaCost;
        public float Cooldown => cooldown;
        public int SpellLvl => spellLvl;
    }
}