using __Scripts.CharacterEntity;
using __Scripts.CharacterEntity.BaseStats;
using UnityEngine;

namespace __Scripts.Inventory.Info
{
    [CreateAssetMenu(fileName = "PotionUltimateInfo", menuName = "Gameplay/Items/Create New PotionUltimateInfo")]
    public class PotionUltimateInfo : ScriptableObject
    {
        [SerializeField] private MainCharacterParameter mainCharacterParameter;

        public MainCharacterParameter MainCharacterParameter => mainCharacterParameter;
        
        public CharacterBaseStats CharacterBaseStats { get; set; }
    }
}