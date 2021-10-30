using GolemEntity;
using UnityEngine;

namespace Inventory.Info
{
    [CreateAssetMenu(fileName = "PotionMultiplyInfo", menuName = "Gameplay/Items/Create New PotionMultiplyInfo")]
    public class PotionUltimateInfo : ScriptableObject
    {
        [SerializeField] private MainCharacterParameter mainCharacterParameter;

        public MainCharacterParameter MainCharacterParameter => mainCharacterParameter;
    }
}