using CharacterEntity.ExtraStats;
using UnityEngine;

namespace Inventory.Info.Spells
{
    [CreateAssetMenu(fileName = "DebuffSpellInfo", menuName = "Gameplay/Spells/Create New DebuffSpellInfo")]
    public class DebuffSpellInfo : ScriptableObject
    {
        [SerializeField] private ExtraStatsParameter[] affectsExtraStats;
        [SerializeField] private float debuffDuration;

        public ExtraStatsParameter[] AffectsExtraStats => affectsExtraStats;
        public float DebuffDuration => debuffDuration;
    }
}