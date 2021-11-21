using System;
using Inventory.Abstracts;
using Inventory.DragAndDrop;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Inventory.UI
{
    public class UIInventorySlot : UISlot, IPointerClickHandler
    {
        [SerializeField] private UIInventoryItem uiInventoryItem;
        [SerializeField] private bool isEquippingSlot;
        private IInventorySlot Slot { get; set; }

        private InventoryOrganization _uiInventory;

        private void Awake()
        {
            _uiInventory = GetComponentInParent<InventoryOrganization>();
            
        }

        private void Start()
        {
            _uiInventory.Inventory.InventoryStateChanged += InventoryOnInventoryStateChanged;
        }

        private void OnDestroy()
        {
            _uiInventory.Inventory.InventoryStateChanged -= InventoryOnInventoryStateChanged;
        }
        
        public void SetSlot(IInventorySlot newSlot)
        {
            Slot = newSlot;
            Slot.IsEquippingSlot = isEquippingSlot;
        }

        public override void OnDrop(PointerEventData eventData)
        {
            try
            {
                var otherItemUI = eventData.pointerDrag.GetComponent<UIInventoryItem>();
                var otherSlotUI = otherItemUI.GetComponentInParent<UIInventorySlot>();
                var otherSlot = otherSlotUI.Slot;
                var inventory = _uiInventory.Inventory;

                inventory.TransitFromSlotToSlot(this, otherSlot, Slot);

                Refresh();
                otherSlotUI.Refresh();
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private void InventoryOnInventoryStateChanged(object obj)
        {
            Refresh();
        }

        public void Refresh()
        {
            if (Slot != null)
            {
                uiInventoryItem.Refresh(Slot);
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (Slot.IsEmpty)
                return;

            HandleClick();
        }

        private void HandleClick()
        {
            switch (uiInventoryItem.Item)
            {
                case IConsumableBuffItem item:
                    _uiInventory.Inventory.OnConsumableItemUsed(Slot, (IInventoryItem)item);
                    break;
                case IConsumableHealingItem item:
                    _uiInventory.Inventory.OnConsumableItemUsed(Slot, (IInventoryItem)item);
                    break;
                case IPotionFlatItem item:
                    PotionDrinker.DrinkPotion(_uiInventory.Inventory, item);
                    break;
                case IPotionMultiplyItem item:
                    PotionDrinker.DrinkPotion(_uiInventory.Inventory, item);
                    break;
                case IPotionUltimateItem item:
                    PotionDrinker.DrinkPotion(_uiInventory.Inventory, item);
                    break;
            }
        }
        
    }
}