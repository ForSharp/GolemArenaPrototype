using __Scripts.CharacterEntity.ExtraStats;
using UnityEngine;

namespace __Scripts.Inventory.Info
{
    [CreateAssetMenu(fileName = "ArtefactInfo", menuName = "Gameplay/Items/Create New ArtefactInfo")]
    public class ArtefactInfo : ScriptableObject
    {
        [SerializeField] private ExtraStatsParameter[] affectsExtraStats;

        public ExtraStatsParameter[] AffectsExtraStats => affectsExtraStats;
    }
}