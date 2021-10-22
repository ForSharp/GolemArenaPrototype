using System;
using DragAndDrop;
using Inventory.Abstracts;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class UIInventorySlot : UISlot
    {
        [SerializeField] private UIInventoryItem uiInventoryItem;
        public IInventorySlot Slot { get; private set; }

        private UIInventory _uiInventory;

        private void Awake()
        {
            _uiInventory = GetComponentInParent<UIInventory>();
        }

        public void SetSlot(IInventorySlot newSlot)
        {
            Slot = newSlot;
        }

        public override void OnDrop(PointerEventData eventData)
        {
            var otherItemUI = eventData.pointerDrag.GetComponent<UIInventoryItem>();
            var otherSlotUI = otherItemUI.GetComponentInParent<UIInventorySlot>();
            var otherSlot = otherSlotUI.Slot;
            var inventory = _uiInventory.Inventory;

            inventory.TransitFromSlotToSlot(this, otherSlot, Slot);
            
            Refresh();
            otherSlotUI.Refresh();
        }

        public void Refresh()
        {
            if (Slot != null)
            {
                uiInventoryItem.Refresh(Slot);
            }
        }
    }
}
