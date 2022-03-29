using __Scripts.CharacterEntity;
using UnityEngine;

namespace __Scripts.Inventory.Info.Spells
{
    [CreateAssetMenu(fileName = "HealSpellInfo", menuName = "Gameplay/Spells/Create New HealSpellInfo")]
    public class HealSpellInfo : ScriptableObject
    {
        [SerializeField] private MainCharacterParameter healedParameter;
        [SerializeField] private bool healIsFlat;
        [SerializeField] private float healingValue;

        public MainCharacterParameter HealedParameter => healedParameter;
        public bool HealIsFlat => healIsFlat;
        public float HealingValue => healingValue;
    }
}