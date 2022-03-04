using System;
using System.Collections.Generic;
using System.Linq;
using __Scripts.Inventory.Abstracts;

namespace __Scripts.Inventory
{
    public sealed class InventoryWithSlots : IInventory
    {
        private readonly List<IInventorySlot> _slots;
        public int Capacity { get; set; }
        public bool IsFull => _slots.All(slot => slot.IsFull);

        public event Action<object, IInventoryItem, int> InventoryItemAdded;

        public event Action<object, string, int> InventoryItemRemoved;

        public event Action<object> InventoryStateChanged;

        private void OnInventoryItemAdded(object sender, IInventoryItem item, int amount)
        {
            InventoryItemAdded?.Invoke(sender, item, amount);
        }

        private void OnInventoryItemRemoved(object sender, string itemId, int amount)
        {
            InventoryItemRemoved?.Invoke(sender, itemId, amount);
        }
        
        public void OnInventoryStateChanged(object sender)
        {
            InventoryStateChanged?.Invoke(sender);
        }

        public InventoryWithSlots(int capacity)
        {
            Capacity = capacity;
            _slots = new List<IInventorySlot>(capacity);

            for (var i = 0; i < capacity; i++)
            {
                _slots.Add(new InventorySlot());
            }
        }
    
        public IInventoryItem GetItem(string itemId)
        {
            return _slots.Find(slot => slot.ItemId == itemId).Item;
        }

        public IInventoryItem[] GetAllItems()
        {
            var allItems = new List<IInventoryItem>();
            foreach (var slot in _slots)
            {
                if (!slot.IsEmpty)
                {
                    allItems.Add(slot.Item);
                }
            }

            return allItems.ToArray();
        }

        public IInventoryItem[] GetAllItems(string itemId)
        {
            var allItemsOfType = new List<IInventoryItem>();
            var slotOfType = _slots.FindAll(slot => !slot.IsEmpty && slot.ItemId == itemId);
            foreach (var slot in slotOfType)
            {
                allItemsOfType.Add(slot.Item);
            }

            return allItemsOfType.ToArray();
        }

        public IInventoryItem[] GetEquippedItems()
        {
            var requiredSlots = _slots.FindAll(slot => !slot.IsEmpty && slot.Item.State.IsEquipped);
            var equippedItems = new List<IInventoryItem>();
            foreach (var slot in requiredSlots)
            {
                equippedItems.Add(slot.Item);
            }
        
            return equippedItems.ToArray();
        }

        public int GetItemAmount(string itemId)
        {
            var amount = 0;
            var allItemSlots = _slots.FindAll(slot => !slot.IsEmpty && slot.ItemId == itemId);
            foreach (var slot in allItemSlots)
            {
                amount += slot.Amount;
            }
        
            return amount;
        }

        public bool TryToAdd(object sender, IInventoryItem item)
        {
            var slotWithSameItemButNotEmpty =
                _slots.Find(slot => !slot.IsEmpty && slot.ItemId == item.Id && !slot.IsFull);
            if (slotWithSameItemButNotEmpty != null)
            {
                return TryToAddToSlot(sender, slotWithSameItemButNotEmpty, item);
            }

            var emptySlot = _slots.Find(slot => slot.IsEmpty && !slot.IsEquippingSlot);
            if (emptySlot != null)
            {
                return TryToAddToSlot(sender, emptySlot, item);
            }

            return false;
        }

        public event Action<IInventorySlot, IInventoryItem> ConsumableItemUsed;
        
        public void OnConsumableItemUsed(IInventorySlot slot, IInventoryItem item)
        {
            ConsumableItemUsed?.Invoke(slot, item);
        }
        
        public event Action<IInventorySlot, IInventoryItem> InventoryItemEquipped;
        
        public event Action<IInventorySlot, IInventoryItem> InventoryItemUnEquipped;

        private bool TryToAddToSlot(object sender, IInventorySlot slot, IInventoryItem item)
        {
            var fits = slot.Amount + item.State.Amount <= item.Info.MaxItemsInInventorySlot;
            var amountToAdd = fits ? item.State.Amount : item.Info.MaxItemsInInventorySlot - slot.Amount;
            var amountLeft = item.State.Amount - amountToAdd;
        
            if (slot.IsEmpty)
            {
                var clonedItem = item.Clone();
                clonedItem.State.Amount = amountToAdd;
                slot.SetInventoryItem(clonedItem);
                if (slot.IsEquippingSlot)
                {
                    InventoryItemEquipped?.Invoke(slot, item);
                }
            }
            else
            {
                slot.Item.State.Amount += amountToAdd;
            }
            
            OnInventoryItemAdded(sender, item, amountToAdd);
            OnInventoryStateChanged(sender);

            if (amountLeft <= 0)
            {
                return true;
            }

            item.State.Amount = amountLeft;
            return TryToAdd(sender, item);
        }

        public bool TryToRemove(object sender, string itemId, int amount = 1)
        {
            var slotsWithItem = GetAllNonEquippingSlots(itemId);
            if (slotsWithItem.Length == 0)
                return false;

            var amountToRemove = amount;
            var count = slotsWithItem.Length;
            for (var i = count - 1; i >= 0; i--)
            {
                var slot = slotsWithItem[i];
                if (slot.Amount >= amountToRemove)
                {
                    slot.Item.State.Amount -= amountToRemove;
                    if (slot.Amount <= 0)
                    {
                        var item = slot.Item;
                        slot.Clear();
                        if (slot.IsEquippingSlot)
                        {
                            InventoryItemUnEquipped?.Invoke(slot, item);
                        }
                    }
                    OnInventoryItemRemoved(sender, itemId, amount);
                    OnInventoryStateChanged(sender);
                    break;
                }

                var amountRemoved = slot.Amount;
                amountToRemove -= amountRemoved;
                OnInventoryItemRemoved(sender, itemId, amountRemoved);
                OnInventoryStateChanged(sender);
            }

            return true;
        }

        public void TransitFromSlotToSlot(object sender, IInventorySlot fromSlot, IInventorySlot toSlot)
        {
            if (CantTransit())
                return;

            var slotCapacity = fromSlot.Capacity;
            var fits = fromSlot.Amount + toSlot.Amount <= slotCapacity;
            var amountToAdd = fits ? fromSlot.Amount : slotCapacity - toSlot.Amount;
            var amountLeft = fromSlot.Amount - amountToAdd;

            if (toSlot.IsEmpty)
            {
                var item = fromSlot.Item;
                toSlot.SetInventoryItem(item);
                if (toSlot.IsEquippingSlot)
                {
                    InventoryItemEquipped?.Invoke(toSlot, item);
                }
                
                fromSlot.Clear();
                if (fromSlot.IsEquippingSlot)
                {
                    InventoryItemUnEquipped?.Invoke(fromSlot, item);
                }
                toSlot.Item.State.Amount += amountToAdd;
                OnInventoryStateChanged(sender);
                return;
            }
            
            toSlot.Item.State.Amount += amountToAdd;
            
            if (fits)
            {
                var item = fromSlot.Item;
                fromSlot.Clear();
                if (fromSlot.IsEquippingSlot)
                {
                    InventoryItemUnEquipped?.Invoke(fromSlot, item);
                }
            }
            else
            {
                fromSlot.Item.State.Amount = amountLeft;
            }
            
            OnInventoryStateChanged(sender);
            
            bool CantTransit()
            {
                return fromSlot.IsEmpty || toSlot.IsFull || !toSlot.IsEmpty && fromSlot.ItemId != toSlot.ItemId;
            }
        }

        public IInventorySlot GetSlotByItem(IInventoryItem item)
        {
            foreach (var slot in _slots)
            {
                if (slot.Item == item)
                {
                    return slot;
                }
            }

            throw new Exception();
        }

        public IInventorySlot[] GetAllEquippingSlotsWithItems()
        {
            return _slots.FindAll(slot => !slot.IsEmpty && slot.IsEquippingSlot).ToArray();
        }

        public IInventorySlot[] GetAllEquippingSlots()
        {
            return _slots.Where(slot => slot.IsEquippingSlot).ToArray();
        }
        
        public IInventorySlot[] GetAllEmptyEquippingSlots()
        {
            return _slots.Where(slot => slot.IsEquippingSlot && slot.IsEmpty).ToArray();
        }
        
        public IInventorySlot[] GetAllNonEquippingSlots(string itemId)
        {
            return _slots.FindAll(slot => !slot.IsEmpty && slot.ItemId == itemId).Where(slot => !slot.IsEquippingSlot).ToArray();
        }

        public IInventorySlot[] GetAllNonEquippingSlots()
        {
            return _slots.Where(slot => !slot.IsEquippingSlot).ToArray();
        }
        
        public IInventorySlot[] GetAllSlots(string itemId)
        {
            return _slots.FindAll(slot => !slot.IsEmpty && slot.ItemId == itemId).ToArray();
        }

        public IInventorySlot[] GetAllSlots()
        {
            return _slots.ToArray();
        }
    
        public bool HasItem(string itemId, out IInventoryItem item)
        {
            item = GetItem(itemId);
            return item != null;
        }

    }
}