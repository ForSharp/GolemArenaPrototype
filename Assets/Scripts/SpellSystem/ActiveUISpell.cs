using System;
using Inventory.Abstracts;
using Inventory.Abstracts.Spells;
using UnityEngine;
using UnityEngine.UI;

namespace SpellSystem
{
    public class ActiveUISpell: MonoBehaviour
    {
        [SerializeField] private Button spellChanger;
        [SerializeField] private Image spellIcon;
        [SerializeField] private Text spellLvl;
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
            var item = (IInventoryItem)spellItem;
            spellIcon.sprite = item.Info.SpriteIcon;
            spellChanger.gameObject.SetActive(true);
            spellLvl.text = GetStringSpellLvl(spellItem);
        }

        private string GetStringSpellLvl(ISpellItem spellItem)
        {
            switch (spellItem.SpellInfo.SpellLvl)
            {
                case 1:
                    return "<color=white>★</color><color=black>★★</color>";
                case 2:
                    return "<color=white>★★</color><color=black>★</color>";
                case 3:
                    return "<color=white>★★★</color>";
                default:
                    return "";
            }
        }

        public void DeactivateSpell()
        {
            IsActive = false;
            spellChanger.gameObject.SetActive(false);
            SpellItem = null;
        }
    }
}