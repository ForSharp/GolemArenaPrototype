using System;
using Inventory.Abstracts;
using Inventory.UI;
using UnityEngine;

namespace Inventory
{
    public class InventoryHelper : MonoBehaviour
    {
        [SerializeField] private GameObject inventoryPrefab;
        private InventoryWithSlots _inventory;
        private GameObject _inventoryObject;
        private UIInventorySlot[] _uiSlots;

        public InventoryOrganization InventoryOrganization { get; private set; }

        private void Awake()
        {
            CreateNewInventory();
        }

        private void CreateNewInventory()
        {
            _inventoryObject = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, GameObject.Find("InventoryContainer").transform);
            InventoryOrganization = _inventoryObject.GetComponent<InventoryOrganization>();

            _uiSlots = _inventoryObject.GetComponentsInChildren<UIInventorySlot>();
            _inventory = new InventoryWithSlots(_uiSlots.Length);
            
            InventoryOrganization.Inventory = _inventory;
            
            _inventory.InventoryStateChanged += OnInventoryStateChanged;
            InventoryOrganization.transform.localPosition = Vector3.zero;
            
            SetupInventoryUI(_inventory);
            
            InventoryOrganization.HideAllInventory();
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