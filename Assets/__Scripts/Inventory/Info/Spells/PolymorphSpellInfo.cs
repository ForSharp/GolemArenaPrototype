using UnityEngine;

namespace __Scripts.Inventory.Info.Spells
{
    [CreateAssetMenu(fileName = "PolymorphSpellInfo", menuName = "Gameplay/Spells/Create New PolymorphSpellInfo")]
    public class PolymorphSpellInfo : ScriptableObject
    {
        [SerializeField] private float polymorphDuration;

        public float PolymorphDuration => polymorphDuration;

    }
}