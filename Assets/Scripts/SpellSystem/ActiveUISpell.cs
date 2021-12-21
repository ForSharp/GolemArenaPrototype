﻿using System;
using Inventory.Abstracts;
using Inventory.Abstracts.Spells;
using UnityEngine;
using UnityEngine.UI;

namespace SpellSystem
{
    public class ActiveUISpell: MonoBehaviour
    {
        [SerializeField] private Image reminder;
        [SerializeField] private Button spellChanger;
        [SerializeField] private Image spellIcon;
        public bool IsActive { get; private set; }
        public ISpellItem SpellItem { get; private set; }
        
        private void Start()
        {
            spellChanger.gameObject.SetActive(false);
        }

        public void ActivateSpell(ISpellItem spellItem)
        {
            SpellItem = spellItem;
            IsActive = true;
            reminder.gameObject.SetActive(false);
            var item = (IInventoryItem)spellItem;
            spellIcon.sprite = item.Info.SpriteIcon;
            spellChanger.gameObject.SetActive(true);
        }

        public void DeactivateSpell()
        {
            IsActive = false;
            reminder.gameObject.SetActive(true);
            spellChanger.gameObject.SetActive(false);
            SpellItem = null;
        }
    }
}