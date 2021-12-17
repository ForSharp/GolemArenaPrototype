using CharacterEntity.State;
using UnityEngine;

namespace UI
{
    public class SpellsPanel : MonoBehaviour
    {
        [SerializeField] private GameObject spellButtonFirst;
        [SerializeField] private GameObject spellButtonSecond;
        [SerializeField] private GameObject spellButtonThird;

        [SerializeField] private LearnedSpellsPanel learnedSpellsPanel;

        private int _spellNumberToChange;
        
        public CharacterState character;

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

        public void AddLearnedSpell()
        {
            
        }
        
        public void UpdateLearnedSpells()
        {
            
        }
    }
}