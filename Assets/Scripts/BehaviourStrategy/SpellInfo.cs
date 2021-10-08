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
        [SerializeField] private float damage;
        [SerializeField] private float periodicDamage;
        [SerializeField] private int effectDuration;
        [SerializeField] private float hill;
        
        public SpellType SpellType => spellType;
        public float ManaCost => manaCost;
        public float Cooldown => cooldown;
        public int SpellLvl => spellLvl;
        public float Damage => damage;
        public float PeriodicDamage => periodicDamage;
        public int EffectDuration => effectDuration;
        public float Hill => hill;
    }
}