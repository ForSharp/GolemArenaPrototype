using CharacterEntity.State;
using Inventory.Abstracts.Spells;
using UnityEngine;

namespace SpellSystem
{
    public class SpellsPanel : MonoBehaviour
    {
        [SerializeField] private ActiveUISpell spellButtonFirst;
        [SerializeField] private ActiveUISpell spellButtonSecond;
        [SerializeField] private ActiveUISpell spellButtonThird;

        [SerializeField] private LearnedSpellsPanel learnedSpellsPanel;

        private int _spellNumberToChange;
        
        [HideInInspector] public CharacterState character;

        public void HideLearnedSpellsPanel()
        {
            learnedSpellsPanel.gameObject.SetActive(false);
        }

        public void HideAll()
        {
            spellButtonFirst.gameObject.SetActive(false);
            spellButtonSecond.gameObject.SetActive(false);
            spellButtonThird.gameObject.SetActive(false);
            learnedSpellsPanel.gameObject.SetActive(false);
        }

        public void ActivateSpell()
        {
            
        }

        public void RefreshActiveSpell(int spellNumb)
        {
            _spellNumberToChange = spellNumb;
            
            switch (spellNumb)
            {
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
            }
        }

        public void SetupActiveSpell(ISpellItem spellItem)
        {
            learnedSpellsPanel.LearnSpell(spellItem);
        }
        
        public void ShowLearnedSpellsPanel()
        {
            learnedSpellsPanel.gameObject.SetActive(true);
            character.InventoryHelper.InventoryOrganization.HideNonEquippingSlots();
        }

        public void ShowActiveSpells()
        {
            spellButtonFirst.gameObject.SetActive(true);
            spellButtonSecond.gameObject.SetActive(true);
            spellButtonThird.gameObject.SetActive(true);
        }

        public void AddLearnedSpell(ISpellItem learnedSpell)
        {
            
        }
        
        public void UpdateLearnedSpells()
        {
            
        }
    }
}