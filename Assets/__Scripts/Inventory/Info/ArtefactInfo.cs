using CharacterEntity.ExtraStats;
using UnityEngine;

namespace Inventory.Info
{
    [CreateAssetMenu(fileName = "ArtefactInfo", menuName = "Gameplay/Items/Create New ArtefactInfo")]
    public class ArtefactInfo : ScriptableObject
    {
        [SerializeField] private ExtraStatsParameter[] affectsExtraStats;

        public ExtraStatsParameter[] AffectsExtraStats => affectsExtraStats;
    }
}