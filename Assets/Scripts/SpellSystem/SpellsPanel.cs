﻿using System;
using CharacterEntity.State;
using Inventory.Abstracts;
using Inventory.Abstracts.Spells;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SpellSystem
{
    public class SpellsPanel : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
    {
        [SerializeField] private ActiveUISpell spellButtonFirst;
        [SerializeField] private ActiveUISpell spellButtonSecond;
        [SerializeField] private ActiveUISpell spellButtonThird;

        [SerializeField] private LearnedSpellsPanel learnedSpellsPanel;
        private int _spellNumberToChange;
        private bool _learnedSpellPanelEnabled;
        [HideInInspector] public CharacterState character;
        
        public ActiveUISpell SpellButtonFirst => spellButtonFirst;
        public ActiveUISpell SpellButtonSecond => spellButtonSecond;
        public ActiveUISpell SpellButtonThird => spellButtonFirst;
        public bool InPanel { get; private set; }

        public void EndCooldownAllSpells()
        {
            
        }

        public void HideLearnedSpellsPanel()
        {
            learnedSpellsPanel.HideLearnedSpells();
            _learnedSpellPanelEnabled = false;
        }

        public void HideAll()
        {
            spellButtonFirst.gameObject.SetActive(false);
            spellButtonSecond.gameObject.SetActive(false);
            spellButtonThird.gameObject.SetActive(false);
            learnedSpellsPanel.HideLearnedSpells();
            _learnedSpellPanelEnabled = false;
        }

        public void UseActiveSpell(int spellNumb)
        {
            if (SpellIsNull(spellNumb))
            {
                ChangeActiveSpell(spellNumb);
            }
            else
            {
                StartCastSpell(spellNumb);
            }
        }

        private void StartCastSpell(int spellNumb)
        {
            switch (spellNumb)
            {
                case 1:
                    if (spellButtonFirst.InCooldown)
                        return;
                    character.SpellManager.CheckCanCastSpell(1);
                    //spellButtonFirst.StartCooldown();
                    break;
                case 2:
                    if (spellButtonSecond.InCooldown)
                        return;
                    //spellButtonSecond.StartCooldown();
                    break;
                case 3:
                    if (spellButtonThird.InCooldown)
                        return;
                    //spellButtonThird.StartCooldown();
                    break;
            }
        }

        public void ChangeActiveSpell(int spellNumb)
        {
            if (_learnedSpellPanelEnabled)
                return;
            
            _spellNumberToChange = spellNumb;
            
            ShowLearnedSpellsPanel();
        }

        private bool SpellIsNull(int spellNumb)
        {
            switch (spellNumb)
            {
                case 1:
                    return spellButtonFirst.SpellItem == null;
                case 2:
                    return spellButtonSecond.SpellItem == null;
                case 3:
                    return spellButtonThird.SpellItem == null;
                default:
                    return false;
            }
        }
        
        public void SetupActiveSpell(ISpellItem spellItem)
        {
            var item = (IInventoryItem)spellItem;
            var id = item.Info.Id;
            switch (_spellNumberToChange)
            {
                case 1:
                    if (spellButtonFirst.SpellItem != null)
                    {
                        var spellToDeactivate = (IInventoryItem)spellButtonFirst.SpellItem;
                        var currentId = spellToDeactivate.Info.Id;
                        if (id != currentId)
                        {
                            learnedSpellsPanel.DeactivateSpell(currentId);
                            learnedSpellsPanel.ActivateSpell(id);
                        }
                    }
                    spellButtonFirst.ActivateSpell(spellItem);
                    character.SpellManager.ActivateSpell(spellItem, _spellNumberToChange);
                    learnedSpellsPanel.ActivateSpell(id);
                    break;
                case 2:
                    if (spellButtonSecond.SpellItem != null)
                    {
                        var spellToDeactivate = (IInventoryItem)spellButtonSecond.SpellItem;
                        var currentId = spellToDeactivate.Info.Id;
                        if (id != currentId)
                        {
                            learnedSpellsPanel.DeactivateSpell(currentId);
                            learnedSpellsPanel.ActivateSpell(id);
                        }
                    }
                    spellButtonSecond.ActivateSpell(spellItem);
                    character.SpellManager.ActivateSpell(spellItem, _spellNumberToChange);
                    learnedSpellsPanel.ActivateSpell(id);
                    break;
                case 3:
                    if (spellButtonThird.SpellItem != null)
                    {
                        var spellToDeactivate = (IInventoryItem)spellButtonThird.SpellItem;
                        var currentId = spellToDeactivate.Info.Id;
                        if (id != currentId)
                        {
                            learnedSpellsPanel.DeactivateSpell(currentId);
                            learnedSpellsPanel.ActivateSpell(id);
                        }
                    }
                    spellButtonThird.ActivateSpell(spellItem);
                    character.SpellManager.ActivateSpell(spellItem, _spellNumberToChange);
                    learnedSpellsPanel.ActivateSpell(id);
                    break;
            }
            
        }

        private void ShowLearnedSpellsPanel()
        {
            learnedSpellsPanel.ShowLearnedSpells();
            character.InventoryHelper.InventoryOrganization.HideNonEquippingSlots();
            _learnedSpellPanelEnabled = true;
        }

        public void ShowActiveSpells()
        {
            spellButtonFirst.gameObject.SetActive(true);
            spellButtonSecond.gameObject.SetActive(true);
            spellButtonThird.gameObject.SetActive(true);
        }

        public void AddLearnedSpell(ISpellItem learnedSpell)
        {
            learnedSpellsPanel.LearnSpell(learnedSpell);
        }
        
        public void UpdateLearnedSpells(ISpellItem learnedSpell)
        {
            var item = (IInventoryItem)learnedSpell;
            var id = item.Info.Id;
            learnedSpellsPanel.UpgradeSpell(learnedSpell);
            if (SpellIsActive(id, out var spellNumb))
            {
                _spellNumberToChange = spellNumb;
                SetupActiveSpell(learnedSpell);
            }
        }

        private bool SpellIsActive(string spellId, out int spellNumb)
        {
            if (spellButtonFirst.SpellId == spellId)
            {
                spellNumb = 1;
                return true;
            }

            if (spellButtonSecond.SpellId == spellId)
            {
                spellNumb = 2;
                return true;
            }

            if (spellButtonThird.SpellId == spellId)
            {
                spellNumb = 3;
                return true;
            }

            spellNumb = 0;
            return false;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            InPanel = false;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            InPanel = true;
        }
    }
}