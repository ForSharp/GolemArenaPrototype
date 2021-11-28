using CharacterEntity.ExtraStats;
using UnityEngine;

namespace Inventory.Info.Spells
{
    [CreateAssetMenu(fileName = "SummonInfo", menuName = "Gameplay/Spells/Create New SummonInfo")]
    public class SummonSpellInfo : ScriptableObject
    {
        [SerializeField] private CharacterExtraStats summonStats;
        [SerializeField] private float summonDuration;
        [SerializeField] private int summonedQuantity;
        
        public CharacterExtraStats SummonStats => summonStats;
        public float SummonDuration => summonDuration;
        public int SummonedQuantity => summonedQuantity;
    }
}