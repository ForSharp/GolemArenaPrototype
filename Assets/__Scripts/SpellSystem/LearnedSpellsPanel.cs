using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using __Scripts.Inventory.Abstracts;
using Inventory;
using Inventory.Abstracts;
using Inventory.Abstracts.Spells;
using Inventory.Info.Spells;
using UnityEngine;
using UnityEngine.UI;

namespace SpellSystem
{
    public class LearnedSpellsPanel : MonoBehaviour
    {
        [SerializeField] private UISpell uiSpellPrefab;
        [SerializeField] private GameObject uiSpellsContainer;
        [SerializeField] private GameObject closeButton;
        private readonly List<UISpell> _learnedSpells = new List<UISpell>();

        private void Start()
        {
            for (int i = 0; i < 10; i++)
            {
                CreateUISpell();
            }
        }

        private void CreateUISpell()
        {
            if (uiSpellsContainer.activeSelf)
            {
                var spellObj = Instantiate(uiSpellPrefab, uiSpellsContainer.transform);
                _learnedSpells.Add(spellObj);
            }
            else
            {
                uiSpellsContainer.SetActive(true);
                var spellObj = Instantiate(uiSpellPrefab, uiSpellsContainer.transform);
                _learnedSpells.Add(spellObj);
                uiSpellsContainer.SetActive(false);
            }
        }

        public void LearnSpell(ISpellItem spellItem)
        {
            AddLearnedSpell(spellItem);
        }

        private void AddLearnedSpell(ISpellItem spellItem)
        {
            if (_learnedSpells.Count(spell => !spell.IsLearned) > 0)
            {
                _learnedSpells.First(spell => !spell.IsLearned).SetupSpellData(spellItem);
            }
            else
            {
                CreateUISpell();
                _learnedSpells.Last().SetupSpellData(spellItem);
            }
        }


        public void UpgradeSpell(ISpellItem spellItem)
        {
            var item = (IInventoryItem)spellItem;
            var id = item.Info.Id;
            foreach (var spell in _learnedSpells)
            {
                if (spell.SpellId == id)
                {
                    spell.UpgradeSpell(spellItem);
                    return;
                }
            }
        }

        public void ActivateSpell(string spellId)
        {
            foreach (var spell in _learnedSpells)
            {
                if (spell.SpellId == spellId)
                {
                    spell.ActivateSpell();
                    return;
                }
            }
        }

        public void DeactivateSpell(string spellId)
        {
            foreach (var spell in _learnedSpells)
            {
                if (spell.SpellId == spellId)
                {
                    spell.DeactivateSpell();
                    return;
                }
            }
        }

        public void HideLearnedSpells()
        {
            uiSpellsContainer.SetActive(false);
            closeButton.SetActive(false);
        }

        public void ShowLearnedSpells()
        {
            uiSpellsContainer.SetActive(true);
            closeButton.SetActive(true);
        }
    }
}