using GolemEntity.ExtraStats;
using UnityEngine;

namespace Inventory.Info.Spells
{
    [CreateAssetMenu(fileName = "SummonInfo", menuName = "Gameplay/Spells/Create New SummonInfo")]
    public class SummonSpellInfo : ScriptableObject
    {
        [SerializeField] private GolemExtraStats summonStats;
        [SerializeField] private float summonDuration;
        [SerializeField] private int summonedQuantity;
        
        public GolemExtraStats SummonStats => summonStats;
        public float SummonDuration => summonDuration;
        public int SummonedQuantity => summonedQuantity;
    }
}