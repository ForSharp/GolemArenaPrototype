﻿using System;
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
        private InventoryOrganization _inventoryOrganization;
        private UIInventorySlot[] _uiSlots;
        private void Start()
        {
            CreateNewInventory();
        }

        private void CreateNewInventory()
        {
            _inventoryObject = Instantiate(inventoryPrefab, new Vector3(0, 225, 0), Quaternion.identity, GameObject.Find("Canvas").transform);
            _inventoryOrganization = _inventoryObject.GetComponent<InventoryOrganization>();

            _uiSlots = _inventoryObject.GetComponentsInChildren<UIInventorySlot>();
            _inventory = new InventoryWithSlots(_uiSlots.Length);
            _inventoryOrganization.Inventory = _inventory;
            _inventory.InventoryStateChanged += OnInventoryStateChanged;
            
            SetupInventoryUI(_inventory);
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
            _inventory.TryToAddToSlot(this, _inventory.GetAllNonEquippingSlots()[0], item);
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