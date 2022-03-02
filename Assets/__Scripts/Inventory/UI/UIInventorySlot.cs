using System;
using __Scripts.Inventory.Abstracts;
using GameLoop;
using Inventory;
using Inventory.Abstracts;
using Inventory.Abstracts.Spells;
using Inventory.DragAndDrop;
using Inventory.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace __Scripts.Inventory.UI
{
    public class UIInventorySlot : UISlot, IPointerClickHandler
    {
        [SerializeField] private UIInventoryItem uiInventoryItem;
        [SerializeField] private bool isEquippingSlot;
        private IInventorySlot Slot { get; set; }
        private InventoryOrganization _uiInventory;

        public bool IsEquippingSlot => isEquippingSlot;
        public IInventoryItem GetSlotItem()
        {
            return uiInventoryItem.Item;
        }
        
        private void Awake()
        {
            _uiInventory = GetComponentInParent<InventoryOrganization>();
            
        }

        private void Start()
        {
            _uiInventory.inventory.InventoryStateChanged += InventoryOnInventoryStateChanged;
        }

        private void OnDestroy()
        {
            _uiInventory.inventory.InventoryStateChanged -= InventoryOnInventoryStateChanged;
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
                var inventory = _uiInventory.inventory;

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
                    _uiInventory.inventory.OnConsumableItemUsed(Slot, (IInventoryItem)item);
                    break;
                case IConsumableHealingItem item:
                    _uiInventory.inventory.OnConsumableItemUsed(Slot, (IInventoryItem)item);
                    break;
                case IPotionFlatItem item:
                    PotionDrinker.DrinkPotion(_uiInventory.inventory, item);
                    break;
                case IPotionMultiplyItem item:
                    PotionDrinker.DrinkPotion(_uiInventory.inventory, item);
                    break;
                case IPotionUltimateItem item:
                    PotionDrinker.DrinkPotion(_uiInventory.inventory, item);
                    break;
                case ISpellItem item:
                    Game.GetCharacterByInventory(_uiInventory.inventory).SpellManager.LearnSpell(item);
                    break;
            }
        }
        
    }
}