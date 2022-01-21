using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Behaviour;
using Behaviour.Abstracts;
using CharacterEntity;
using CharacterEntity.State;
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
        private readonly CharacterState _character;
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

        public SpellManager(Animator animator, CharacterState characterState, SpellContainer spellContainer)
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
                        var spellItem = _spellFirstUI.SpellItem;
                        ShowTargets(spellItem);
                        //mark spell choice
                        var target = GetTarget(spellItem);
                        if (target)
                        {
                            CastSpellFirst(target);
                            
                            CancelShowingTargets(spellItem);
                        }
                        else
                        {
                            Debug.Log("no cast");
                            CancelShowingTargets(spellItem);
                        }
                        
                    }
                    break;
                case 2:
                    break;
                case 3:
                    break;
            }
            
        }

        private CharacterState GetTarget(ISpellItem spellItem)
        {
            if (Camera.main is null)
            {
                return null;
            }
            var ray = Camera.main.ScreenPointToRay(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            if (!Physics.Raycast(ray, out var hit))
            {
                return null;
            }
            var coll = hit.collider;
            if (coll.TryGetComponent(out CharacterState state))
            {
                switch (spellItem.SpellInfo.SpellType)
                {
                    case SpellType.Heal:
                        if (CheckFriend(state)) return state;
                        break;
                    case SpellType.Buff:
                        if (CheckFriend(state)) return state;
                        break;
                    case SpellType.Debuff:
                        if (CheckEnemy(state)) return state;
                        break;
                    case SpellType.Damage:
                        if (CheckEnemy(state)) return state;
                        break;
                    case SpellType.Summon:
                        if (CheckEnemy(state)) return state;
                        break;
                    case SpellType.Polymorph:
                        if (CheckEnemy(state)) return state;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            
            
            return null;

            bool CheckFriend(CharacterState characterState)
            {
                foreach (var friend in GetFriends())
                {
                    if (friend == characterState)
                    {
                        return true;
                    }
                }

                return false;
            }

            bool CheckEnemy(CharacterState characterState)
            {
                foreach (var enemy in GetEnemies())
                {
                    if (enemy == characterState)
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        private void CancelShowingTargets(ISpellItem spellItem)
        {
            switch (spellItem.SpellInfo.SpellType)
            {
                case SpellType.Heal:
                    StopPlayingTargetFriendsEffect();
                    break;
                case SpellType.Buff:
                    StopPlayingTargetFriendsEffect();
                    break;
                case SpellType.Debuff:
                    StopPlayingTargetEnemiesEffect();
                    break;
                case SpellType.Damage:
                    StopPlayingTargetEnemiesEffect();
                    break;
                case SpellType.Summon:
                    StopPlayingTargetEnemiesEffect();
                    break;
                case SpellType.Polymorph:
                    StopPlayingTargetEnemiesEffect();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        private void ShowTargets(ISpellItem spellItem)
        {
            switch (spellItem.SpellInfo.SpellType)
            {
                case SpellType.Heal:
                    ShowTargetsFriends();
                    break;
                case SpellType.Buff:
                    ShowTargetsFriends();
                    break;
                case SpellType.Debuff:
                    ShowTargetsEnemies();
                    break;
                case SpellType.Damage:
                    ShowTargetsEnemies();
                    break;
                case SpellType.Summon:
                    ShowTargetsEnemies();
                    break;
                case SpellType.Polymorph:
                    ShowTargetsEnemies();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void ShowTargetsEnemies()
        {
            var enemies = GetEnemies();
            foreach (var enemy in enemies)
            {
                enemy.characterEffectsContainer.PlayTargetEnemy();
            }
        }

        private void StopPlayingTargetEnemiesEffect()
        {
            var enemies = GetEnemies();
            foreach (var enemy in enemies)
            {
                enemy.characterEffectsContainer.StopPlayingTargetEnemy();
            }
        }

        private void ShowTargetsFriends()
        {
            var friends = GetFriends();
            foreach (var friend in friends)
            {
                friend.characterEffectsContainer.PlayTargetFriend();
            }
        }

        private void StopPlayingTargetFriendsEffect()
        {
            var friends = GetFriends();
            foreach (var friend in friends)
            {
                friend.characterEffectsContainer.StopPlayingTargetFriend();
            }
        }

        private IEnumerable<CharacterState> GetEnemies()
        {
            return Game.AllCharactersInSession.Where(character => !character.IsDead)
                .Where(group => group.Group != _character.Group);
        }

        private IEnumerable<CharacterState> GetFriends()
        {
            return Game.AllCharactersInSession.Where(character => !character.IsDead)
                .Where(group => group.Group == _character.Group);
            ;
        }

        public void CastSpellFirst(CharacterState targetState)
        {
            Debug.Log($"Target: {targetState.Type}, Spell: {_spellFirstUI.SpellItem.SpellInfo.SpellType}");
            
            //_spellFirst.CastSpell(targetState);
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