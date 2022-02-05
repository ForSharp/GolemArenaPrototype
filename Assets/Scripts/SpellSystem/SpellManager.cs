using System;
using System.Collections.Generic;
using Behaviour;
using Behaviour.Abstracts;
using CharacterEntity.State;
using Controller;
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
        private readonly PlayerController _playerController;

        private ICastable _spellFirst;
        private ICastable _spellSecond;
        private ICastable _spellThird;

        private readonly ActiveUISpell _spellFirstUI;
        private readonly ActiveUISpell _spellSecondUI;
        private readonly ActiveUISpell _spellThirdUI;

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
                        _learnedSpells[i] = ItemContainer.Instance.GetUpgradedSpell(_learnedSpells[i],
                            _learnedSpells[i].SpellInfo.SpellLvl);
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
            _playerController = GameObject.Find("Tester").GetComponent<PlayerController>();
        }

        public void CheckCanCastSpell(int spellNumb)
        {
            switch (spellNumb)
            {
                case 1:
                    if (!_character.IsDead && !_spellFirstUI.InCooldown &&
                        _spellFirstUI.SpellItem.SpellInfo.ManaCost <= _character.CurrentMana
                        && Game.Stage == Game.GameStage.Battle)
                    {
                        var spellItem = _spellFirstUI.SpellItem;
                        ShowTargets(spellItem);
                        _playerController.StartChoosingTarget(GetChoosingTargetMode(spellItem), spellItem, spellNumb);

                        _spellFirstUI.MarkSpell();
                        
                        _character.OnStartSpellCast();
                    }

                    break;
                case 2:
                    if (!_character.IsDead && !_spellSecondUI.InCooldown &&
                        _spellSecondUI.SpellItem.SpellInfo.ManaCost <= _character.CurrentMana
                        && Game.Stage == Game.GameStage.Battle)
                    {
                        var spellItem = _spellSecondUI.SpellItem;
                        ShowTargets(spellItem);
                        _playerController.StartChoosingTarget(GetChoosingTargetMode(spellItem), spellItem, spellNumb);

                        _spellSecondUI.MarkSpell();
                        
                        _character.OnStartSpellCast();
                    }

                    break;
                case 3:
                    if (!_character.IsDead && !_spellThirdUI.InCooldown && 
                        _spellThirdUI.SpellItem.SpellInfo.ManaCost <= _character.CurrentMana
                        && Game.Stage == Game.GameStage.Battle)
                    {
                        var spellItem = _spellThirdUI.SpellItem;
                        ShowTargets(spellItem);
                        _playerController.StartChoosingTarget(GetChoosingTargetMode(spellItem), spellItem, spellNumb);

                        _spellThirdUI.MarkSpell();
                        
                        _character.OnStartSpellCast();
                    }

                    break;
            }
        }

        public void CastSpell(ISpellItem spellItem, CharacterState target, int spellNumb)
        {
            switch (spellNumb)
            {
                case 1:
                    CastSpellFirst(target, spellItem);
                    break;
                case 2:
                    CastSpellSecond(target, spellItem);
                    break;
                case 3:
                    CastSpellThird(target, spellItem);
                    break;
            }

            CancelShowingTargets(spellItem);
        }

        public void CancelCast(ISpellItem spellItem, int spellNumb)
        {
            Debug.Log("no cast");
            CancelShowingTargets(spellItem);
            _character.OnCancelSpellCast();
            
            switch (spellNumb)
            {
                case 1:
                    _spellFirstUI.StopMarkSpell();
                    break;
                case 2:
                    _spellSecondUI.StopMarkSpell();
                    break;
                case 3:
                    _spellThirdUI.StopMarkSpell();
                    break;
            }
        }

        private ChoosingTargetMode GetChoosingTargetMode(ISpellItem spellItem)
        {
            switch (spellItem.SpellInfo.SpellType)
            {
                case SpellType.Heal:
                    return ChoosingTargetMode.Friend;
                case SpellType.Buff:
                    return ChoosingTargetMode.Friend;
                case SpellType.Debuff:
                    return ChoosingTargetMode.Enemy;
                case SpellType.Damage:
                    return ChoosingTargetMode.Enemy;
                case SpellType.Summon:
                    return ChoosingTargetMode.Enemy;
                case SpellType.Polymorph:
                    return ChoosingTargetMode.Enemy;
                default:
                    throw new ArgumentOutOfRangeException();
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
            var enemies = Game.GetEnemies(_character);
            foreach (var enemy in enemies)
            {
                enemy.characterEffectsContainer.PlayTargetEnemy();
            }
        }

        private void StopPlayingTargetEnemiesEffect()
        {
            var enemies = Game.GetEnemies(_character);
            foreach (var enemy in enemies)
            {
                enemy.characterEffectsContainer.StopPlayingTargetEnemy();
            }
        }

        private void ShowTargetsFriends()
        {
            var friends = Game.GetFriends(_character);
            foreach (var friend in friends)
            {
                friend.characterEffectsContainer.PlayTargetFriend();
            }
        }

        private void StopPlayingTargetFriendsEffect()
        {
            var friends = Game.GetFriends(_character);
            foreach (var friend in friends)
            {
                friend.characterEffectsContainer.StopPlayingTargetFriend();
            }
        }

        private void CastSpellFirst(CharacterState targetState, ISpellItem spellItem)
        {
            Debug.Log($"Target: {targetState.Type}, Spell: {_spellFirstUI.SpellItem.SpellInfo.SpellType}");
            _spellFirstUI.StopMarkSpell();
            _spellFirstUI.StartCooldown();
            _character.TrySpendMana(spellItem.SpellInfo.ManaCost);

            _spellFirst.CastSpell(targetState);
        }

        private void CastSpellSecond(CharacterState targetState, ISpellItem spellItem)
        {
            Debug.Log($"Target: {targetState.Type}, Spell: {_spellSecondUI.SpellItem.SpellInfo.SpellType}");
            _spellSecondUI.StopMarkSpell();
            _spellSecondUI.StartCooldown();
            _character.TrySpendMana(spellItem.SpellInfo.ManaCost);

            _spellSecond.CastSpell(targetState);
        }

        private void CastSpellThird(CharacterState targetState, ISpellItem spellItem)
        {
            Debug.Log($"Target: {targetState.Type}, Spell: {_spellThirdUI.SpellItem.SpellInfo.SpellType}");
            _spellThirdUI.StopMarkSpell();
            _spellThirdUI.StartCooldown();
            _character.TrySpendMana(spellItem.SpellInfo.ManaCost);

            _spellThird.CastSpell(targetState);
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

        private void SetupSpell(out ICastable spellSlot, ISpellItem spellItem)
        {
            switch (spellItem)
            {
                case FireBallItem _:
                    spellSlot = _spellContainer.FireballSpell;
                    _spellContainer.FireballSpell.SpellConstructor(spellItem);
                    break;
                case FreezingItem _: //
                    spellSlot = _spellContainer.FireballSpell;
                    _spellContainer.FireballSpell.SpellConstructor(spellItem);
                    break;
                case GraceItem graceBuffItem: //
                    spellSlot = _spellContainer.FireballSpell;
                    _spellContainer.FireballSpell.SpellConstructor(spellItem);
                    break;
                case SnowstormItem snowstormItem: //
                    spellSlot = _spellContainer.FireballSpell;
                    _spellContainer.FireballSpell.SpellConstructor(spellItem);
                    break;
                case SummonSpiderItem summonSpiderItem: //
                    spellSlot = _spellContainer.FireballSpell;
                    _spellContainer.FireballSpell.SpellConstructor(spellItem);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(spellItem));
            }
        }
    }
}