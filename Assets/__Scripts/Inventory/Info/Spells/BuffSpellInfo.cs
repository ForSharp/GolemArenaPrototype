using __Scripts.CharacterEntity.ExtraStats;
using UnityEngine;

namespace __Scripts.Inventory.Info.Spells
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