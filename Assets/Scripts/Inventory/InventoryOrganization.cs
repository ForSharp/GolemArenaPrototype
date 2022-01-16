using GameLoop;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Inventory
{
    public class InventoryOrganization : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
    {
        [SerializeField] private Text switchButtonText;
        [SerializeField] private GameObject[] inventorySlots;
        [SerializeField] private GameObject[] equippingSlots;
        [SerializeField] private GameObject inventoryBackGround;
        [SerializeField] private Button switchButton;

        public InventoryWithSlots inventory;
        public bool InPanel { get; private set; }
        private bool _isHide = false;

        public void OnSwitchButtonClicked()
        {
            if (_isHide)
            {
                ShowNonEquippingSlots();
            }
            else
            {
                HideNonEquippingSlots();
            }
        }

        private void ShowNonEquippingSlots()
        {
            _isHide = false;
            switchButtonText.text = "Hide";

            foreach (var slot in inventorySlots)
            {
                slot.SetActive(true);
            }

            inventoryBackGround.SetActive(true);


            foreach (var character in Game.AllCharactersInSession)
            {
                character.SpellPanelHelper.SpellsPanel.HideLearnedSpellsPanel();
            }
        }

        public void HideNonEquippingSlots()
        {
            foreach (var slot in inventorySlots)
            {
                slot.SetActive(false);
            }

            inventoryBackGround.SetActive(false);
            _isHide = true;
            switchButtonText.text = "Show";
        }

        public void HideAllInventory()
        {
            _isHide = true;
            switchButtonText.text = "Show";
            switchButton.gameObject.SetActive(false);

            foreach (var slot in inventorySlots)
            {
                slot.SetActive(false);
            }

            foreach (var slot in equippingSlots)
            {
                slot.SetActive(false);
            }

            inventoryBackGround.SetActive(false);
        }

        public void ShowInventory()
        {
            _isHide = true;
            switchButtonText.text = "Show";
            switchButton.gameObject.SetActive(true);

            foreach (var slot in equippingSlots)
            {
                slot.SetActive(true);
            }
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