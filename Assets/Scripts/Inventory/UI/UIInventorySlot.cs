using Inventory.Abstracts;
using Inventory.DragAndDrop;
using UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Inventory.UI
{
    public class UIInventorySlot : UISlot
    {
        [SerializeField] private UIInventoryItem uiInventoryItem;
        [SerializeField] private bool isEquippingSlot;
        public bool IsEquippingSlot => isEquippingSlot;
        public IInventorySlot Slot { get; private set; }

        private InventoryOrganization _uiInventory;

        private void Awake()
        {
            _uiInventory = GetComponentInParent<InventoryOrganization>();
        }

        public void SetSlot(IInventorySlot newSlot)
        {
            Slot = newSlot;
            Slot.IsEquippingSlot = isEquippingSlot;
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
