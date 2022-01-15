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
        
        [HideInInspector] public CharacterState character;
        
        public bool InPanel { get; private set; }
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

        public void RefreshActiveSpell(int spellNumb)
        {
            _spellNumberToChange = spellNumb;
            
            ShowLearnedSpellsPanel();
        }

        public void SetupActiveSpell(ISpellItem spellItem)
        {
            var item = (IInventoryItem)spellItem;
            learnedSpellsPanel.ActivateSpell(item.Info.Id);

            switch (_spellNumberToChange)
            {
                case 1:
                    if (spellButtonFirst.SpellItem != null)
                    {
                        var spellToDeactivate = (IInventoryItem)spellButtonFirst.SpellItem;
                        learnedSpellsPanel.DeactivateSpell(spellToDeactivate.Info.Id);
                    }
                    spellButtonFirst.ActivateSpell(spellItem);
                    character.SpellManager.ActivateSpell(spellItem, _spellNumberToChange);
                    break;
                case 2:
                    if (spellButtonSecond.SpellItem != null)
                    {
                        var spellToDeactivate = (IInventoryItem)spellButtonFirst.SpellItem;
                        learnedSpellsPanel.DeactivateSpell(spellToDeactivate.Info.Id);
                    }
                    spellButtonSecond.ActivateSpell(spellItem);
                    character.SpellManager.ActivateSpell(spellItem, _spellNumberToChange);
                    break;
                case 3:
                    if (spellButtonThird.SpellItem != null)
                    {
                        var spellToDeactivate = (IInventoryItem)spellButtonFirst.SpellItem;
                        learnedSpellsPanel.DeactivateSpell(spellToDeactivate.Info.Id);
                    }
                    spellButtonThird.ActivateSpell(spellItem);
                    character.SpellManager.ActivateSpell(spellItem, _spellNumberToChange);
                    break;
            }
        }

        private void ShowLearnedSpellsPanel()
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
            learnedSpellsPanel.LearnSpell(learnedSpell);
        }
        
        public void UpdateLearnedSpells()
        {
            
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