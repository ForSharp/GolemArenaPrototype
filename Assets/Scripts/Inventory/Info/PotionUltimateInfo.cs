using GolemEntity;
using UnityEngine;

namespace Inventory.Info
{
    [CreateAssetMenu(fileName = "PotionUltimateInfo", menuName = "Gameplay/Items/Create New PotionUltimateInfo")]
    public class PotionUltimateInfo : ScriptableObject
    {
        [SerializeField] private MainCharacterParameter mainCharacterParameter;

        public MainCharacterParameter MainCharacterParameter => mainCharacterParameter;
    }
}