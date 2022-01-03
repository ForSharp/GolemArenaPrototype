using System.Globalization;
using Inventory.Abstracts;
using Inventory.Abstracts.Spells;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SpellSystem
{
    public class UISpell : MonoBehaviour
    {
        [SerializeField] private Text spellTitle;
        [SerializeField] private Text manaCost;
        [SerializeField] private Text spellLvl;
        [SerializeField] private Image spellIcon;
        [SerializeField] private Image activeSpellIdentifier;
        
        private SpellsPanel _mainSpellPanel;
        private ISpellItem _spellItem;
        private IInventoryItem _item;
        private bool _isActive;
        public string SpellId { get; private set; }
        public bool IsLearned { get; private set; }
        
        private void Awake()
        {
            SetComponentsVisibility(false);
            activeSpellIdentifier.gameObject.SetActive(false);
            _mainSpellPanel = GetComponentInParent<SpellsPanel>();
        }

        public void SetupSpellData(ISpellItem spellItem)
        {
            SetComponentsVisibility(true);
            _spellItem = spellItem;
            _item = (IInventoryItem)_spellItem;
            spellTitle.text = _item.Info.Title;
            manaCost.text = _spellItem.SpellInfo.ManaCost.ToString(CultureInfo.InvariantCulture);
            spellLvl.text = _spellItem.SpellInfo.SpellLvl.ToString();
            spellIcon.sprite = _item.Info.SpriteIcon;

            SpellId = _item.Info.Id;
            IsLearned = true;
        }

        public void UpgradeSpell(string lvl, string cost)
        {
            spellLvl.text = lvl;
            manaCost.text = cost;
        }

        public void ActivateSpell()
        {
            activeSpellIdentifier.gameObject.SetActive(true);
            _isActive = true;
        }

        public void DeactivateSpell()
        {
            activeSpellIdentifier.gameObject.SetActive(false);
            _isActive = false;
        }

        private void SetComponentsVisibility(bool state)
        {
            spellTitle.gameObject.SetActive(state);
            manaCost.gameObject.SetActive(state);
            spellLvl.gameObject.SetActive(state);
            spellIcon.gameObject.SetActive(state);

            if (state == false)
            {
                activeSpellIdentifier.gameObject.SetActive(false);
            }

            if (state && IsLearned)
            {
                activeSpellIdentifier.gameObject.SetActive(true);
            }
        }

        public void OnLearnedSpellClick()
        {
            if (_spellItem != null && !_isActive)
            {
                _mainSpellPanel.SetupActiveSpell(_spellItem);
                
            }
        }
    }
}