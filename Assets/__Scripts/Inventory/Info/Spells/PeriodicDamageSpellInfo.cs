using __Scripts.CharacterEntity;
using UnityEngine;

namespace __Scripts.Inventory.Info.Spells
{
    [CreateAssetMenu(fileName = "PeriodicDamageSpellInfo", menuName = "Gameplay/Spells/Create New PeriodicDamageSpellInfo")]
    public class PeriodicDamageSpellInfo : ScriptableObject
    {
        [SerializeField] private MainCharacterParameter periodicDamagedParameter;
        [SerializeField] private bool periodicDamageIsFlat;
        [SerializeField] private float periodicDamagingValue;
        [SerializeField] private float periodicDamageDuration;

        public MainCharacterParameter PeriodicDamagedParameter => periodicDamagedParameter;
        public bool PeriodicDamageIsFlat => periodicDamageIsFlat;
        public float PeriodicDamagingValue => periodicDamagingValue;
        public float PeriodicDamageDuration => periodicDamageDuration;
    }
}