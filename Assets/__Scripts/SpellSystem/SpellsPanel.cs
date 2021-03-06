using __Scripts.CharacterEntity.State;
using __Scripts.Environment;
using __Scripts.Inventory.Abstracts;
using __Scripts.Inventory.Abstracts.Spells;
using __Scripts.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace __Scripts.SpellSystem
{
    public class SpellsPanel : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
    {
        [SerializeField] private ActiveUISpell spellButtonFirst;
        [SerializeField] private ActiveUISpell spellButtonSecond;
        [SerializeField] private ActiveUISpell spellButtonThird;

        [SerializeField] private LearnedSpellsPanel learnedSpellsPanel;
        private int _spellNumberToChange;
        private bool _learnedSpellPanelEnabled;
        
        private Store _store;
        private ItemInfoPanel _itemInfoPanel;
        
        public ChampionState Character { get; set; }
        
        public ActiveUISpell SpellButtonFirst => spellButtonFirst;
        public ActiveUISpell SpellButtonSecond => spellButtonSecond;
        public ActiveUISpell SpellButtonThird => spellButtonThird;
        public bool InPanel { get; private set; }

        private void Awake()
        {
            _store = FindObjectOfType<Store>();
            _itemInfoPanel = FindObjectOfType<ItemInfoPanel>();
        }
        
        public void HideLearnedSpellsPanel()
        {
            learnedSpellsPanel.HideLearnedSpells();
            _learnedSpellPanelEnabled = false;
            _itemInfoPanel.Close();
        }

        public void HideAll()
        {
            spellButtonFirst.GetComponent<RectTransform>().localScale = new Vector3(0, 0, 0);
            spellButtonSecond.GetComponent<RectTransform>().localScale = new Vector3(0, 0, 0);
            spellButtonThird.GetComponent<RectTransform>().localScale = new Vector3(0, 0, 0);
            learnedSpellsPanel.HideLearnedSpells();
            _learnedSpellPanelEnabled = false;
        }

        public void UseActiveSpell(int spellNumb)
        {
            if (SpellIsNull(spellNumb))
            {
                ChangeActiveSpell(spellNumb);
            }
            else
            {
                StartCastSpell(spellNumb);
            }
        }

        private void StartCastSpell(int spellNumb)
        {
            Character.SpellManager.CheckCanCastSpell(spellNumb);
        }

        public void ChangeActiveSpell(int spellNumb)
        {
            if (_learnedSpellPanelEnabled)
                return;
            
            _spellNumberToChange = spellNumb;
            
            ShowLearnedSpellsPanel();
        }

        private bool SpellIsNull(int spellNumb)
        {
            switch (spellNumb)
            {
                case 1:
                    return spellButtonFirst.SpellItem == null;
                case 2:
                    return spellButtonSecond.SpellItem == null;
                case 3:
                    return spellButtonThird.SpellItem == null;
                default:
                    return false;
            }
        }
        
        public void SetupActiveSpell(ISpellItem spellItem)
        {
            var item = (IInventoryItem)spellItem;
            var id = item.Info.Id;
            switch (_spellNumberToChange)
            {
                case 1:
                    if (spellButtonFirst.SpellItem != null)
                    {
                        var spellToDeactivate = (IInventoryItem)spellButtonFirst.SpellItem;
                        var currentId = spellToDeactivate.Info.Id;
                        if (id != currentId)
                        {
                            learnedSpellsPanel.DeactivateSpell(currentId);
                            learnedSpellsPanel.ActivateSpell(id);
                        }
                    }
                    spellButtonFirst.ActivateSpell(spellItem);
                    Character.SpellManager.ActivateSpell(spellItem, _spellNumberToChange);
                    learnedSpellsPanel.ActivateSpell(id);
                    break;
                case 2:
                    if (spellButtonSecond.SpellItem != null)
                    {
                        var spellToDeactivate = (IInventoryItem)spellButtonSecond.SpellItem;
                        var currentId = spellToDeactivate.Info.Id;
                        if (id != currentId)
                        {
                            learnedSpellsPanel.DeactivateSpell(currentId);
                            learnedSpellsPanel.ActivateSpell(id);
                        }
                    }
                    spellButtonSecond.ActivateSpell(spellItem);
                    Character.SpellManager.ActivateSpell(spellItem, _spellNumberToChange);
                    learnedSpellsPanel.ActivateSpell(id);
                    break;
                case 3:
                    if (spellButtonThird.SpellItem != null)
                    {
                        var spellToDeactivate = (IInventoryItem)spellButtonThird.SpellItem;
                        var currentId = spellToDeactivate.Info.Id;
                        if (id != currentId)
                        {
                            learnedSpellsPanel.DeactivateSpell(currentId);
                            learnedSpellsPanel.ActivateSpell(id);
                        }
                    }
                    spellButtonThird.ActivateSpell(spellItem);
                    Character.SpellManager.ActivateSpell(spellItem, _spellNumberToChange);
                    learnedSpellsPanel.ActivateSpell(id);
                    break;
            }
            
        }
        
        public void SetupActiveSpell(ISpellItem spellItem, int numb)
        {
            var item = (IInventoryItem)spellItem;
            var id = item.Info.Id;
            switch (numb)
            {
                case 1:
                    if (spellButtonFirst.SpellItem != null)
                    {
                        var spellToDeactivate = (IInventoryItem)spellButtonFirst.SpellItem;
                        var currentId = spellToDeactivate.Info.Id;
                        if (id != currentId)
                        {
                            learnedSpellsPanel.DeactivateSpell(currentId);
                            learnedSpellsPanel.ActivateSpell(id);
                        }
                    }
                    spellButtonFirst.ActivateSpell(spellItem);
                    Character.SpellManager.ActivateSpell(spellItem, numb);
                    learnedSpellsPanel.ActivateSpell(id);
                    break;
                case 2:
                    if (spellButtonSecond.SpellItem != null)
                    {
                        var spellToDeactivate = (IInventoryItem)spellButtonSecond.SpellItem;
                        var currentId = spellToDeactivate.Info.Id;
                        if (id != currentId)
                        {
                            learnedSpellsPanel.DeactivateSpell(currentId);
                            learnedSpellsPanel.ActivateSpell(id);
                        }
                    }
                    spellButtonSecond.ActivateSpell(spellItem);
                    Character.SpellManager.ActivateSpell(spellItem, numb);
                    learnedSpellsPanel.ActivateSpell(id);
                    break;
                case 3:
                    if (spellButtonThird.SpellItem != null)
                    {
                        var spellToDeactivate = (IInventoryItem)spellButtonThird.SpellItem;
                        var currentId = spellToDeactivate.Info.Id;
                        if (id != currentId)
                        {
                            learnedSpellsPanel.DeactivateSpell(currentId);
                            learnedSpellsPanel.ActivateSpell(id);
                        }
                    }
                    spellButtonThird.ActivateSpell(spellItem);
                    Character.SpellManager.ActivateSpell(spellItem, numb);
                    learnedSpellsPanel.ActivateSpell(id);
                    break;
            }
            
        }

        private void ShowLearnedSpellsPanel()
        {
            learnedSpellsPanel.ShowLearnedSpells();
            Character.InventoryHelper.InventoryOrganization.HideNonEquippingSlots();
            _learnedSpellPanelEnabled = true;
            
            _itemInfoPanel.Close();
            _store.HideStore();
        }

        public void ShowActiveSpells()
        {
            spellButtonFirst.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            spellButtonSecond.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            spellButtonThird.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        }

        public void AddLearnedSpell(ISpellItem learnedSpell)
        {
            learnedSpellsPanel.LearnSpell(learnedSpell);
        }
        
        public void UpdateLearnedSpells(ISpellItem learnedSpell)
        {
            var item = (IInventoryItem)learnedSpell;
            var id = item.Info.Id;
            learnedSpellsPanel.UpgradeSpell(learnedSpell);
            if (SpellIsActive(id, out var spellNumb))
            {
                _spellNumberToChange = spellNumb;
                SetupActiveSpell(learnedSpell);
            }
        }

        private bool SpellIsActive(string spellId, out int spellNumb)
        {
            if (spellButtonFirst.SpellId == spellId)
            {
                spellNumb = 1;
                return true;
            }

            if (spellButtonSecond.SpellId == spellId)
            {
                spellNumb = 2;
                return true;
            }

            if (spellButtonThird.SpellId == spellId)
            {
                spellNumb = 3;
                return true;
            }

            spellNumb = 0;
            return false;
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