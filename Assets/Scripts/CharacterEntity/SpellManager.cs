using System;
using System.Collections.Generic;
using Behaviour;
using Behaviour.Abstracts;
using Inventory;
using Inventory.Abstracts;
using Inventory.Abstracts.Spells;
using Inventory.Items.SpellItems;
using UnityEngine;
using UnityEngine.UI;

namespace CharacterEntity
{
    public class SpellManager
    {
        private readonly List<ISpellItem> _learnedSpells = new List<ISpellItem>();
        private readonly Animator _animator;
        private readonly State.CharacterState _character;
        private readonly SpellContainer _spellContainer;

        private ICastable _spellFirst;
        private ICastable _spellSecond;
        private ICastable _spellThird;

        public void LearnSpell(ISpellItem spell)
        {
            var inventoryItem = (IInventoryItem)spell;
            
            for (var i = 0; i < _learnedSpells.Count; i++)
            {
                var containedItem = (IInventoryItem)_learnedSpells[i];
                if (containedItem.Info.Title == inventoryItem.Info.Title)
                {
                    if (_learnedSpells[i].SpellInfo.SpellLvl < 3)
                    {
                        _learnedSpells[i] = ItemContainer.GetUpgradedSpell(_learnedSpells[i], _learnedSpells[i].SpellInfo.SpellLvl);

                        DeleteSpellItemAfterLearning(inventoryItem);

                        return;
                    }
                    
                    return;
                }
            }
            
            _learnedSpells.Add(spell);
            DeleteSpellItemAfterLearning(inventoryItem);
        }

        private void DeleteSpellItemAfterLearning(IInventoryItem inventoryItem)
        {
            inventoryItem.State.Amount--;
            var inventory = _character.InventoryHelper.InventoryOrganization.Inventory;

            if (inventoryItem.State.Amount == 0)
            {
                var slot = inventory.GetSlotByItem(inventoryItem);
                slot.Clear();
            }

            inventory.OnInventoryStateChanged(_character);
        }

        public SpellManager(Animator animator, State.CharacterState characterState, SpellContainer spellContainer)
        {
            _animator = animator;
            _character = characterState;
            _spellContainer = spellContainer;
        }
        
        public void CastSpellFirst(State.CharacterState targetState)
        {
            _spellFirst.CastSpell(targetState);
        }

        public void SetupSpellFirst(ISpellItem spellItem) //экипировать спелл
        {
            SetupSpell(out _spellFirst, spellItem);
        }

        public Image GetImageSpellFirst()
        {   
            //var image = (ISpellItem)
            return null;
        }

        private ISpellItem GetSpellItem(ICastable spell)
        {
            switch (spell)
            {
                case FireballSpell fireballSpell:
                    return GetSpellItem<FireBallItem>();
                default:
                    throw new ArgumentOutOfRangeException(nameof(spell));
            }
        }

        private ISpellItem GetSpellItem<T>()where T : ISpellItem 
        {
            foreach (var spellItem in _learnedSpells)
            {
                if (spellItem is T)
                    return spellItem;
            }

            throw new Exception();
        }

        
        
        private int GetLearnedSpellLvl(string spellId)
        {
            foreach (var spell in _learnedSpells)
            {
                var spellItem = (IInventoryItem)spell;
                if (spellId == spellItem.Info.Id)
                {
                    return spell.SpellInfo.SpellLvl;
                }
            }

            return default;
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
                case GraceItem graceBuffItem:
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