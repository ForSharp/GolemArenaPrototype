using CharacterEntity.CharacterState;
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
        public CharacterState character;

        public void HideLearnedSpellsPanel()
        {
            learnedSpellsPanel.gameObject.SetActive(false);
        }

        public void HideAll()
        {
            //gameObject.SetActive(false);
            
            spellButtonFirst.gameObject.SetActive(false);
            spellButtonSecond.gameObject.SetActive(false);
            spellButtonThird.gameObject.SetActive(false);
            learnedSpellsPanel.gameObject.SetActive(false);
        }

        public void ShowLearnedSpellsPanel()
        {
            learnedSpellsPanel.gameObject.SetActive(true);
            character.InventoryHelper.inventoryOrganization.HideNonEquippingSlots();
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