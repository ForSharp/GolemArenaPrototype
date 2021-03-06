using System;
using __Scripts.GameLoop;
using __Scripts.Inventory.Abstracts;
using __Scripts.Inventory.Abstracts.Spells;
using __Scripts.Inventory.DragAndDrop;
using __Scripts.UI;
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
        private ItemInfoPanel _itemInfoPanel;
        
        public bool IsEquippingSlot => isEquippingSlot;
        public IInventoryItem GetSlotItem()
        {
            return uiInventoryItem.Item;
        }
        
        private void Awake()
        {
            _uiInventory = GetComponentInParent<InventoryOrganization>();
            _itemInfoPanel = FindObjectOfType<ItemInfoPanel>();
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

            if (isEquippingSlot)
            {
                UseItem();
            }
            else
            {
                HandleClick();
            }
        }

        private void HandleClick()
        {
            _itemInfoPanel.SetUIInventorySlot(this);
            
            switch (uiInventoryItem.Item)
            {
                case IConsumableBuffItem item:
                    _itemInfoPanel.ShowInfo(uiInventoryItem.Item.Info, ItemInfoPanelType.UseConsumable, transform.position);
                    break;
                case IConsumableHealingItem item:
                    _itemInfoPanel.ShowInfo(uiInventoryItem.Item.Info, ItemInfoPanelType.UseConsumable, transform.position);
                    break;
                case IPotionFlatItem item:
                    _itemInfoPanel.ShowInfo(uiInventoryItem.Item.Info, ItemInfoPanelType.UseConsumable, transform.position);
                    break;
                case IPotionMultiplyItem item:
                    _itemInfoPanel.ShowInfo(uiInventoryItem.Item.Info, ItemInfoPanelType.UseConsumable, transform.position);
                    break;
                case IPotionUltimateItem item:
                    _itemInfoPanel.ShowInfo(uiInventoryItem.Item.Info, ItemInfoPanelType.UseConsumable, transform.position);
                    break;
                case ISpellItem item:
                    _itemInfoPanel.ShowInfo(uiInventoryItem.Item.Info, ItemInfoPanelType.LearnSpell, transform.position);
                    break;
                case IArtefactItem item:
                    _itemInfoPanel.ShowInfo(uiInventoryItem.Item.Info, ItemInfoPanelType.Empty, transform.position);
                    break;
            }
        }
        
        public void UseItem()
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