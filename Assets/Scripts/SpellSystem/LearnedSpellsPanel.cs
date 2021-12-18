using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Inventory.Abstracts.Spells;
using Inventory.Info.Spells;
using UI;
using UnityEngine;

namespace SpellSystem
{
    public class LearnedSpellsPanel : MonoBehaviour
    {
        [SerializeField] private UISpell uiSpellPrefab;
        [SerializeField] private GameObject uiSpellsContainer;
        
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
            var spellObj = Instantiate(uiSpellPrefab, uiSpellsContainer.transform);
            _learnedSpells.Add(spellObj);
        }

        public void LearnSpell(ISpellItem spellItem)
        {
            for (var i = 0; i < _learnedSpells.Count; i++)
            {
                if (_learnedSpells[i].IsLearned == false)
                {
                    _learnedSpells[i].SetupSpellData(spellItem);
                    return;
                }
            }
            
            CreateUISpell();
            _learnedSpells.Last().SetupSpellData(spellItem);
        }

        public void UpgradeSpell(SpellInfo spellInfo, string spellId)
        {
            foreach (var spell in _learnedSpells)
            {
                if (spell.SpellId == spellId)
                {
                    spell.UpgradeSpell(spellInfo.SpellLvl.ToString(), spellInfo.ManaCost.ToString(CultureInfo.InvariantCulture));
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
    }
}