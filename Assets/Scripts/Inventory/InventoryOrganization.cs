using System;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class InventoryOrganization : MonoBehaviour
    {
        [SerializeField] private Text switchButtonText;
        [SerializeField] private GameObject[] inventorySlots;
        [SerializeField] private GameObject[] equippingSlots;
        [SerializeField] private GameObject inventoryBackGround;
        [SerializeField] private Button switchButton;

        [HideInInspector] public InventoryWithSlots Inventory;
        
        private bool _isHide = false;

        public void OnSwitchButtonClicked()
        {
            if (_isHide)
            {
                _isHide = false;
                switchButtonText.text = "Hide";

                foreach (var slot in inventorySlots)
                {
                    slot.SetActive(true);
                }
            }
            else
            {
                foreach (var slot in inventorySlots)
                {
                    slot.SetActive(false);
                }
                
                _isHide = true;
                switchButtonText.text = "Show";
            }
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
    }
}