using GolemEntity.ExtraStats;
using UnityEngine;

namespace Inventory.Info.Spells
{
    [CreateAssetMenu(fileName = "BuffSpellInfo", menuName = "Gameplay/Spells/Create New BuffSpellInfo")]
    public class BuffSpellInfo : ScriptableObject
    {
        [SerializeField] private ExtraStatsParameter[] affectsExtraStats;
        [SerializeField] private float buffDuration;

        public ExtraStatsParameter[] AffectsExtraStats => affectsExtraStats;
        public float BuffDuration => buffDuration;
    }
}