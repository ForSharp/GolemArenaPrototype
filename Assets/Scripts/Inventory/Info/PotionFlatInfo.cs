using GolemEntity.BaseStats;
using UnityEngine;

namespace Inventory.Info
{
    [CreateAssetMenu(fileName = "PotionFlatInfo", menuName = "Gameplay/Items/Create New PotionFlatInfo")]
    public class PotionFlatInfo : ScriptableObject
    {
        [SerializeField] [Range(0, 15)] private int strength;
        [SerializeField] [Range(0, 15)] private int agility;
        [SerializeField] [Range(0, 15)] private int intelligence;

        public GolemBaseStats GolemBaseStats => new GolemBaseStats()
            { strength = strength, agility = agility, intelligence = intelligence };
    }
}