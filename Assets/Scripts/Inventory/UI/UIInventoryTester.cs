using System.Collections.Generic;
using Inventory.Abstracts;
using Inventory.Info;
using Inventory.Items;
using Random = UnityEngine.Random;

namespace Inventory.UI
{
    public class UIInventoryTester 
    {
        
        private InventoryItemInfo _pepperInfo;
        private InventoryItemInfo _beerInfo;
        private UIInventorySlot[] _uiSlots;
        
        public InventoryWithSlots Inventory { get; }

        public UIInventoryTester(InventoryItemInfo pepperInfo, InventoryItemInfo beerInfo, UIInventorySlot[] uiSlots)
        {
            
            _pepperInfo = pepperInfo;
            _beerInfo = beerInfo;
            _uiSlots = uiSlots;

            Inventory = new InventoryWithSlots(15);
            Inventory.InventoryStateChanged += OnInventoryStateChanged;
        }

        public void FillSlots()
        {
            var allSlots = Inventory.GetAllSlots();
            var availableSlots = new List<IInventorySlot>(allSlots);

            var filledSlots = 4;
            for (int i = 0; i < filledSlots; i++)
            {
                var filledSlot = AddRandomItemsIntoRandomSlots(new Pepper(_pepperInfo), availableSlots);
                availableSlots.Remove(filledSlot);
                
                filledSlot = AddRandomItemsIntoRandomSlots(new Beer(_beerInfo), availableSlots);
                availableSlots.Remove(filledSlot);
            }
            
            SetupInventoryUI(Inventory);
        }

        private IInventorySlot AddRandomItemsIntoRandomSlots(IInventoryItem item, List<IInventorySlot> slots)
        {
            var rSlotIndex = Random.Range(0, slots.Count);
            var rSlot = slots[rSlotIndex];
            var rCount = Random.Range(1, 4);
            item.State.Amount = rCount;
            Inventory.TryToAddToSlot(this, rSlot, item);
            return rSlot;
        }

        private void SetupInventoryUI(InventoryWithSlots inventory)
        {
            var allSlots = inventory.GetAllSlots();
            var allSlotsCount = allSlots.Length;
            for (int i = 0; i < allSlotsCount; i++)
            {
                var slot = allSlots[i];
                var uiSlot = _uiSlots[i];
                uiSlot.SetSlot(slot);
                uiSlot.Refresh();
            }
        }
        
        private void OnInventoryStateChanged(object sender)
        {
            foreach (var slot in _uiSlots)
            {
                slot.Refresh();
            }
        }
    }
}
