using System;
using Inventory.Abstracts;
using Inventory.UI;
using UnityEngine;

namespace Inventory
{
    public class InventoryHelper : MonoBehaviour
    {
        private InventoryWithSlots _inventory;
        [SerializeField] private GameObject inventoryPrefab;
        private GameObject _inventoryObject;
        [HideInInspector]public InventoryOrganization inventoryOrganization;
        private UIInventorySlot[] _uiSlots;
        private void Start()
        {
            CreateNewInventory();
        }

        private void CreateNewInventory()
        {
            _inventoryObject = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, GameObject.Find("InventoryContainer").transform);
            inventoryOrganization = _inventoryObject.GetComponent<InventoryOrganization>();

            _uiSlots = _inventoryObject.GetComponentsInChildren<UIInventorySlot>();
            _inventory = new InventoryWithSlots(_uiSlots.Length);
            
            inventoryOrganization.Inventory = _inventory;
            
            _inventory.InventoryStateChanged += OnInventoryStateChanged;
            inventoryOrganization.transform.localPosition = Vector3.zero;
            
            SetupInventoryUI(_inventory);
            
            inventoryOrganization.HideAllInventory();
        }

        public void Refresh()
        {
            foreach (var slot in _uiSlots)
            {
                slot.Refresh();
            }
        }

        private void SetupInventoryUI(InventoryWithSlots inventory)
        {
            var allSlots = inventory.GetAllNonEquippingSlots();
            var allSlotsCount = allSlots.Length;
            for (int i = 0; i < allSlotsCount; i++)
            {
                var slot = allSlots[i];
                var uiSlot = _uiSlots[i];
                uiSlot.SetSlot(slot);
                uiSlot.Refresh();
            }
        }

        public void AddItem(IInventoryItem item)
        {
            _inventory.TryToAdd(this, item);
        }
        
        private void OnInventoryStateChanged(object sender)
        {
            foreach (var slot in _uiSlots)
            {
                slot.Refresh();
            }
        }

        private void OnDestroy()
        {
            _inventory.InventoryStateChanged -= OnInventoryStateChanged;
        }
    }
}