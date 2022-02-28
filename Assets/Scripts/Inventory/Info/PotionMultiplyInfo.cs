using CharacterEntity.BaseStats;
using UnityEngine;

namespace Inventory.Info
{
    [CreateAssetMenu(fileName = "PotionMultiplyInfo", menuName = "Gameplay/Items/Create New PotionMultiplyInfo")]
    public class PotionMultiplyInfo : ScriptableObject
    {
        [SerializeField] [Range(0, 25)] private int strength;
        [SerializeField] [Range(0, 25)] private int agility;
        [SerializeField] [Range(0, 25)] private int intelligence;

        public CharacterBaseStats CharacterBaseStats => new CharacterBaseStats()
            { strength = strength, agility = agility, intelligence = intelligence };
    }
}