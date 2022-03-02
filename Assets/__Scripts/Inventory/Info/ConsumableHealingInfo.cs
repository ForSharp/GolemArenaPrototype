using CharacterEntity;
using UnityEngine;

namespace Inventory.Info
{
    [CreateAssetMenu(fileName = "ConsumableHealingInfo", menuName = "Gameplay/Items/Create New ConsumableHealingInfo")]
    public class ConsumableHealingInfo : ScriptableObject
    {
        [SerializeField] private MainCharacterParameter healedParameter;
        [SerializeField] private bool healIsFlat;
        [SerializeField] private float healingValue;

        public MainCharacterParameter HealedParameter => healedParameter;
        public bool HealIsFlat => healIsFlat;
        public float HealingValue => healingValue;
    }
}