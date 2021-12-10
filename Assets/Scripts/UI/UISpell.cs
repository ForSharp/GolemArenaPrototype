using System;
using System.Globalization;
using Inventory.Info.Spells;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UISpell : MonoBehaviour
    {
        [SerializeField] private Text spellTitle;
        [SerializeField] private Text manaCost;
        [SerializeField] private Text spellLvl;
        [SerializeField] private Image spellIcon;
        [SerializeField] private Image activeSpellIdentifier;

        public string SpellId { get; private set; }
        public bool IsLearned { get; private set; }
        
        private void Start()
        {
            SetComponentsVisibility(false);
            activeSpellIdentifier.gameObject.SetActive(false);
        }

        public void SetupSpellData(SpellInfo spellInfo, string title, Sprite icon, string spellId)
        {
            SetComponentsVisibility(true);
            spellTitle.text = title;
            manaCost.text = spellInfo.ManaCost.ToString(CultureInfo.InvariantCulture);
            spellLvl.text = spellInfo.SpellLvl.ToString();
            spellIcon.sprite = icon;

            SpellId = spellId;
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
        }

        public void DeactivateSpell()
        {
            activeSpellIdentifier.gameObject.SetActive(false);
        }

        private void SetComponentsVisibility(bool state)
        {
            spellTitle.gameObject.SetActive(state);
            manaCost.gameObject.SetActive(state);
            spellLvl.gameObject.SetActive(state);
            spellIcon.gameObject.SetActive(state);
        }
    }
}