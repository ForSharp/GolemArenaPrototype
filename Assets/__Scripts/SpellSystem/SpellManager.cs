using System;
using System.Collections.Generic;
using System.Linq;
using __Scripts.Behaviour;
using __Scripts.Behaviour.Abstracts;
using __Scripts.CharacterEntity.State;
using __Scripts.Controller;
using __Scripts.GameLoop;
using __Scripts.Inventory;
using __Scripts.Inventory.Abstracts;
using __Scripts.Inventory.Abstracts.Spells;
using __Scripts.Inventory.Items.SpellItems;
using UnityEngine;
using Random = UnityEngine.Random;

namespace __Scripts.SpellSystem
{
    public class SpellManager
    {
        private readonly Animator _animator;
        private readonly ChampionState _character;
        private readonly SpellContainer _spellContainer;
        private readonly SpellsPanel _spellsPanel;
        private readonly PlayerController _playerController;

        private ICastable _spellFirst;
        private ICastable _spellSecond;
        private ICastable _spellThird;

        public ActiveUISpell SpellFirstUI { get;}
        public ActiveUISpell SpellSecondUI { get;}
        public ActiveUISpell SpellThirdUI { get;}
        public List<ISpellItem> LearnedSpells { get; } = new List<ISpellItem>();

        public void LearnSpell(ISpellItem spell)
        {
            var inventoryItem = (IInventoryItem)spell;

            for (var i = 0; i < LearnedSpells.Count; i++)
            {
                var containedItem = (IInventoryItem)LearnedSpells[i];
                if (containedItem.Info.Id == inventoryItem.Info.Id)
                {
                    if (LearnedSpells[i].SpellInfo.SpellLvl < 3)
                    {
                        LearnedSpells[i] = ItemContainer.Instance.GetUpgradedSpell(LearnedSpells[i],
                            LearnedSpells[i].SpellInfo.SpellLvl);
                        _spellsPanel.UpdateLearnedSpells(LearnedSpells[i]);
                        DeleteSpellItemAfterLearning(inventoryItem);

                        return;
                    }

                    //максимальный уровень спелла уже изучен - ничего не делаем
                    return;
                }
            }

            LearnedSpells.Add(spell);
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

        public SpellManager(Animator animator, ChampionState characterState, SpellContainer spellContainer)
        {
            _animator = animator;
            _character = characterState;
            _spellContainer = spellContainer;
            _spellsPanel = _character.SpellPanelHelper.SpellsPanel;
            SpellFirstUI = _spellsPanel.SpellButtonFirst;
            SpellSecondUI = _spellsPanel.SpellButtonSecond;
            SpellThirdUI = _spellsPanel.SpellButtonThird;
            _playerController = GameObject.Find("Tester").GetComponent<PlayerController>();
        }

        public void CheckCanCastSpell(int spellNumb)
        {
            switch (spellNumb)
            {
                case 1:
                    if (!_character.IsDead && !SpellFirstUI.InCooldown &&
                        SpellFirstUI.SpellItem.SpellInfo.ManaCost <= _character.CurrentMana
                        && Game.Stage == Game.GameStage.Battle)
                    {
                        var spellItem = SpellFirstUI.SpellItem;
                        ShowTargets(spellItem);
                        _playerController.StartChoosingTarget(GetChoosingTargetMode(spellItem), spellItem, spellNumb);

                        SpellFirstUI.MarkSpell();
                        
                        _character.OnStartSpellCast();
                    }

                    break;
                case 2:
                    if (!_character.IsDead && !SpellSecondUI.InCooldown &&
                        SpellSecondUI.SpellItem.SpellInfo.ManaCost <= _character.CurrentMana
                        && Game.Stage == Game.GameStage.Battle)
                    {
                        var spellItem = SpellSecondUI.SpellItem;
                        ShowTargets(spellItem);
                        _playerController.StartChoosingTarget(GetChoosingTargetMode(spellItem), spellItem, spellNumb);

                        SpellSecondUI.MarkSpell();
                        
                        _character.OnStartSpellCast();
                    }

                    break;
                case 3:
                    if (!_character.IsDead && !SpellThirdUI.InCooldown && 
                        SpellThirdUI.SpellItem.SpellInfo.ManaCost <= _character.CurrentMana
                        && Game.Stage == Game.GameStage.Battle)
                    {
                        var spellItem = SpellThirdUI.SpellItem;
                        ShowTargets(spellItem);
                        _playerController.StartChoosingTarget(GetChoosingTargetMode(spellItem), spellItem, spellNumb);

                        SpellThirdUI.MarkSpell();
                        
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

        public void CastSpell(int spellNumb)
        {
            ISpellItem spell;

            switch (spellNumb)
            {
                case 1:
                    spell = SpellFirstUI.SpellItem;
                    break;
                case 2:
                    spell = SpellSecondUI.SpellItem;
                    break;
                case 3:
                    spell = SpellThirdUI.SpellItem;
                    break;
                default:
                    spell = null;
                    break;
            }

            var targetMode = GetChoosingTargetMode(spell);
            List<CharacterState> targets = new List<CharacterState>();

            switch (targetMode)
            {
                case ChoosingTargetMode.Friend:
                    targets = Game.GetFriends(_character).ToList();
                    break;
                case ChoosingTargetMode.Enemy:
                    targets = Game.GetEnemies(_character).ToList();
                    break;
                case ChoosingTargetMode.All:
                    targets = Game.AllCharactersInSession.ToList();
                    break;
            }
            
            switch (spellNumb)
            {
                case 1:
                    CastSpellFirst(targets[Random.Range(0, targets.Count)], spell);
                    break;
                case 2:
                    CastSpellSecond(targets[Random.Range(0, targets.Count)], spell);
                    break;
                case 3:
                    CastSpellThird(targets[Random.Range(0, targets.Count)], spell);
                    break;
            }
        }

        public void CancelCast(ISpellItem spellItem, int spellNumb)
        {
            Debug.Log("no cast");
            CancelShowingTargets(spellItem);
            _character.OnCancelSpellCast();
            
            switch (spellNumb)
            {
                case 1:
                    SpellFirstUI.StopMarkSpell();
                    break;
                case 2:
                    SpellSecondUI.StopMarkSpell();
                    break;
                case 3:
                    SpellThirdUI.StopMarkSpell();
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

        private void CastSpellFirst(CharacterState targetState, ISpellItem spellItem, bool stopMarking = true)
        {
            if (stopMarking)
            {
                SpellFirstUI.StopMarkSpell();
            }
            
            SpellFirstUI.StartCooldown();
            _character.TrySpendMana(spellItem.SpellInfo.ManaCost);

            _spellFirst.CastSpell(targetState);
        }

        private void CastSpellSecond(CharacterState targetState, ISpellItem spellItem, bool stopMarking = true)
        {
            if (stopMarking)
            {
                SpellSecondUI.StopMarkSpell();
            }
            
            SpellSecondUI.StartCooldown();
            _character.TrySpendMana(spellItem.SpellInfo.ManaCost);

            _spellSecond.CastSpell(targetState);
        }

        private void CastSpellThird(CharacterState targetState, ISpellItem spellItem, bool stopMarking = true)
        {
            if (stopMarking)
            {
                SpellThirdUI.StopMarkSpell();
            }
            
            SpellThirdUI.StartCooldown();
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
                case FreezingItem _: 
                    spellSlot = _spellContainer.FreezingSpell;
                    _spellContainer.FreezingSpell.SpellConstructor(spellItem);
                    break;
                case GraceItem _: 
                    spellSlot = _spellContainer.GraceSpell;
                    _spellContainer.GraceSpell.SpellConstructor(spellItem);
                    break;
                case SnowstormItem _: 
                    spellSlot = _spellContainer.SnowstormSpell;
                    _spellContainer.SnowstormSpell.SpellConstructor(spellItem);
                    break;
                case SummonSpiderItem _:
                    spellSlot = _spellContainer.SummonSpiderSpell;
                    _spellContainer.SummonSpiderSpell.SpellConstructor(spellItem);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(spellItem));
            }
        }
    }
}