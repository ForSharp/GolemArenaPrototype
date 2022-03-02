using System.Linq;
using __Scripts.Inventory.Abstracts;
using __Scripts.Inventory.UI;
using Inventory;
using Inventory.Abstracts;
using UnityEngine;

namespace __Scripts.Inventory
{
    public class InventoryHelper : MonoBehaviour
    {
        [SerializeField] private GameObject inventoryPrefab;
        private InventoryWithSlots _inventory;
        private GameObject _inventoryObject;
        public UIInventorySlot[] UISlots { get; private set; }
        public UIInventorySlot[] NonEquippingSlots => UISlots.Where(slot => slot.IsEquippingSlot == false).ToArray();

        public InventoryOrganization InventoryOrganization { get; private set; }

        private void Awake()
        {
            CreateNewInventory();
        }

        private void CreateNewInventory()
        {
            _inventoryObject = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, GameObject.Find("InventoryContainer").transform);
            InventoryOrganization = _inventoryObject.GetComponent<InventoryOrganization>();

            UISlots = _inventoryObject.GetComponentsInChildren<UIInventorySlot>();
            _inventory = new InventoryWithSlots(UISlots.Length);
            
            InventoryOrganization.inventory = _inventory;
            
            _inventory.InventoryStateChanged += OnInventoryStateChanged;
            InventoryOrganization.transform.localPosition = Vector3.zero;
            
            SetupInventoryUI(_inventory);
            
            InventoryOrganization.HideAllInventory();
        }

        private void SetupInventoryUI(InventoryWithSlots inventory)
        {
            var allSlots = inventory.GetAllNonEquippingSlots();
            var allSlotsCount = allSlots.Length;
            for (int i = 0; i < allSlotsCount; i++)
            {
                var slot = allSlots[i];
                var uiSlot = UISlots[i];
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
            foreach (var slot in UISlots)
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