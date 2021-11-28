using CharacterEntity;
using UnityEngine;

namespace Inventory.Info.Spells
{
    [CreateAssetMenu(fileName = "DamageSpellInfo", menuName = "Gameplay/Spells/Create New DamageSpellInfo")]
    public class DamageSpellInfo : ScriptableObject
    {
        [SerializeField] private MainCharacterParameter damagedParameter;
        [SerializeField] private bool damageIsFlat;
        [SerializeField] private float damagingValue;

        public MainCharacterParameter DamagedParameter => damagedParameter;
        public bool DamageIsFlat => damageIsFlat;
        public float DamagingValue => damagingValue;
    }
}