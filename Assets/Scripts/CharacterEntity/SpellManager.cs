using System;
using System.Collections.Generic;
using Behaviour.Abstracts;
using Inventory.Abstracts.Spells;
using Inventory.Items.SpellItems;
using UnityEngine;

namespace CharacterEntity
{
    public class SpellManager
    {
        private readonly HashSet<ISpellItem> _learnedSpells = new HashSet<ISpellItem>();
        private readonly Animator _animator;
        private readonly CharacterState.CharacterState _character;
        private readonly SpellContainer _spellContainer;

        private ICastable _spellFirst;
        private ICastable _spellSecond;
        private ICastable _spellThird;

        public bool LearnSpell(ISpellItem spell)//должен сработать при клике на предмет спелл
        {
            if (_learnedSpells.Contains(spell))
            {
                if (spell.SpellInfo.SpellLvl < 3)
                {
                    //spell.SpellInfo.SpellLvl++;
                    //delete item
                    return true;
                }

                return false;
            }
            //delete item
            _learnedSpells.Add(spell);
            return true;
        }

        public SpellManager(Animator animator, CharacterState.CharacterState characterState, SpellContainer spellContainer)
        {
            _animator = animator;
            _character = characterState;
            _spellContainer = spellContainer;
        }
        
        public void CastSpellFirst(CharacterState.CharacterState targetState)
        {
            _spellFirst.CastSpell(targetState);
        }

        public void SetupSpellFirst(ISpellItem spellItem) //экипировать спелл
        {
            SetupSpell(out _spellFirst, spellItem);
        }

        private void SetupSpell(out ICastable spellSlot, ISpellItem spellItem)
        {
            switch (spellItem)
            {
                case FireBallItem fireBallItem:
                    spellSlot = _spellContainer.fireballSpell;
                    _spellContainer.fireballSpell.SpellConstructor(spellItem, _character, _animator);
                    break;
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

            spellSlot = null;
        }
    }
}