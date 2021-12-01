using System;
using Behaviour;
using Behaviour.Abstracts;
using Inventory;
using Inventory.Abstracts.Spells;
using Inventory.Items.SpellItems;
using UnityEngine;

namespace CharacterEntity
{
    public class SpellContainer : MonoBehaviour
    {
        [HideInInspector] public FireballSpell fireballSpell;
        public static SpellContainer Instance;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            fireballSpell = GetComponent<FireballSpell>();
        }

        public ICastable GetSpell(ISpellItem spellItem)
        {
            switch (spellItem)
            {
                case FireBallItem fireBallItem:
                    return fireballSpell;
                case FreezingItem freezingItem:
                    break;
                case GraceBuffItem graceBuffItem:
                    break;
                case SnowstormItem snowstormItem:
                    break;
                case SummonSpiderItem summonSpiderItem:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(spellItem));
            }

            throw new Exception();
        }
    }
}