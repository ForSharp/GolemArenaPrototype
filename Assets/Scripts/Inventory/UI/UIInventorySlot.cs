using System;
using System.Diagnostics;
using System.Timers;
using Inventory.Abstracts;
using Inventory.DragAndDrop;
using UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Inventory.UI
{
    public class UIInventorySlot : UISlot, IPointerClickHandler
    {
        [SerializeField] private UIInventoryItem uiInventoryItem;
        [SerializeField] private bool isEquippingSlot;
        public bool IsEquippingSlot => isEquippingSlot;
        public IInventorySlot Slot { get; private set; }

        private InventoryOrganization _uiInventory;
        private float _stopwatchValue;
        private bool _isStopwatchEnabled;
        private const float SecondDoubleClick = 1.1f;

        private void Awake()
        {
            _uiInventory = GetComponentInParent<InventoryOrganization>();
        }

        private void Update()
        {
            if (_isStopwatchEnabled && !Slot.IsEmpty)
            {
                _stopwatchValue = 0;
                _stopwatchValue += Time.deltaTime;
                if (_stopwatchValue > 2)
                {
                    _isStopwatchEnabled = false;
                }
            }
        }

        public void SetSlot(IInventorySlot newSlot)
        {
            Slot = newSlot;
            Slot.IsEquippingSlot = IsEquippingSlot;
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

            if (!_isStopwatchEnabled)
            {
                _isStopwatchEnabled = true;
            }
            else
            {
                if (_stopwatchValue < SecondDoubleClick)
                {
                    //double click
                    //HandleDoubleClick();
                }
                else
                {
                    _stopwatchValue = 0;
                }
            }
        }

        private void HandleDoubleClick()
        {
            switch (uiInventoryItem.Item)
            {
                case IConsumableBuffItem item:
                    //
                    break;
                case IConsumableHealingItem item:
                    //
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