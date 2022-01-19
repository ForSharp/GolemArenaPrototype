using System;
using System.Collections;
using System.Collections.Generic;
using Behaviour;
using Behaviour.Abstracts;
using GameLoop;
using Inventory;
using Inventory.Abstracts;
using Inventory.Abstracts.Spells;
using Inventory.Items.SpellItems;
using UnityEngine;

namespace SpellSystem
{
    public class SpellManager
    {
        private readonly List<ISpellItem> _learnedSpells = new List<ISpellItem>();
        private readonly Animator _animator;
        private readonly CharacterEntity.State.CharacterState _character;
        private readonly SpellContainer _spellContainer;
        private readonly SpellsPanel _spellsPanel;
        
        private ICastable _spellFirst;
        private ICastable _spellSecond;
        private ICastable _spellThird;

        private ActiveUISpell _spellFirstUI;
        private ActiveUISpell _spellSecondUI;
        private ActiveUISpell _spellThirdUI;
        
        public void LearnSpell(ISpellItem spell)
        {
            var inventoryItem = (IInventoryItem)spell;
            
            for (var i = 0; i < _learnedSpells.Count; i++)
            {
                var containedItem = (IInventoryItem)_learnedSpells[i];
                if (containedItem.Info.Id == inventoryItem.Info.Id)
                {
                    if (_learnedSpells[i].SpellInfo.SpellLvl < 3)
                    {
                        _learnedSpells[i] = ItemContainer.Instance.GetUpgradedSpell(_learnedSpells[i], _learnedSpells[i].SpellInfo.SpellLvl);
                        _spellsPanel.UpdateLearnedSpells(_learnedSpells[i]);
                        DeleteSpellItemAfterLearning(inventoryItem);
                        
                        return;
                    }
                    //максимальный уровень спелла уже изучен - ничего не делаем
                    return;
                }
            }
            
            _learnedSpells.Add(spell);
            _spellsPanel.AddLearnedSpell(spell);
            DeleteSpellItemAfterLearning(inventoryItem);
        }

        private void DeleteSpellItemAfterLearning(IInventoryItem inventoryItem)
        {
            inventoryItem.State.Amount--;
            var inventory = _character.InventoryHelper.InventoryOrganization.inventory;

            if (inventoryItem.State.Amount == 0)
            {
                var slot = inventory.GetSlotByItem(inventoryItem);
                slot.Clear();
            }

            inventory.OnInventoryStateChanged(_character);
        }

        public SpellManager(Animator animator, CharacterEntity.State.CharacterState characterState, SpellContainer spellContainer)
        {
            _animator = animator;
            _character = characterState;
            _spellContainer = spellContainer;
            _spellsPanel = _character.SpellPanelHelper.SpellsPanel;
            _spellFirstUI = _spellsPanel.SpellButtonFirst;
            _spellSecondUI = _spellsPanel.SpellButtonSecond;
            _spellThirdUI = _spellsPanel.SpellButtonThird;
        }

        public void CheckCanCastSpell(int spellNumb)
        {
            switch (spellNumb)
            {
                case 1:
                    // if (!_spellFirstUI.IsActive)
                    //     return;
                    if (!_spellFirstUI.InCooldown && _spellFirstUI.SpellItem.SpellInfo.ManaCost <= _character.CurrentMana
                                                      && Game.Stage == Game.GameStage.Battle)
                    {
                        //show correct targets
                        //mark spell choice
                        //set target and then cast spell
                    }
                    break;
                case 2:
                    break;
                case 3:
                    break;
            }
            
            
        }

        private void ShowTargets(ISpellItem spellItem)//mark enemies or friends
        {
            switch (spellItem.SpellInfo.SpellType)
            {
                case SpellType.Heal:
                    break;
                case SpellType.Buff:
                    break;
                case SpellType.Debuff:
                    break;
                case SpellType.Damage:
                    break;
                case SpellType.Summon:
                    break;
                case SpellType.Polymorph:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        public void CastSpellFirst(CharacterEntity.State.CharacterState targetState)
        {
            _spellFirst.CastSpell(targetState);
        }

        private IEnumerator EndSpellCooldown(int spellNumb, float duration)
        {
            yield return new WaitForSeconds(duration);
            
            
        }

        private void EndCooldownAllSpells()
        {
            //по сути, преждевременно прервать перезарядку, обнулить ее
        }

        public void ActivateSpell(ISpellItem spellItem, int numb) 
        {
            switch (numb)
            {
                case 1:
                    SetupSpell(out _spellFirst, spellItem);
                    break;
                case 2:
                    SetupSpell(out _spellSecond, spellItem);
                    break;
                case 3:
                    SetupSpell(out _spellThird, spellItem);
                    break;
            }
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
        
        private void SetupSpell(out ICastable spellSlot, ISpellItem spellItem)
        {
            switch (spellItem)
            {
                case FireBallItem _:
                    spellSlot = _spellContainer.FireballSpell;
                    _spellContainer.FireballSpell.SpellConstructor(spellItem, _character, _animator);
                    break;
                case FreezingItem _:
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