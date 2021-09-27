using BehaviourStrategy.Abstracts;
using UnityEngine;

namespace BehaviourStrategy
{
    [CreateAssetMenu(fileName = "SpellInfo", menuName = "Gameplay/Spells/Create New Spell")]
    public class SpellInfo : ScriptableObject, ISpellInfo
    {
        [SerializeField] private SpellType spellType;
        [SerializeField] private float manaCost;
        [SerializeField] private float cooldown;
        [SerializeField] private int spellLvl;

        public SpellType SpellType => spellType;
        public float ManaCost => manaCost;
        public float Cooldown => cooldown;
        public int SpellLvl => spellLvl;
    }
}