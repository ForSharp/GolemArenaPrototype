using Inventory.Info;
using UI;
using UnityEngine;

namespace Inventory.UI
{
    public class UIInventory : MonoBehaviour
    {
        [SerializeField] private InventoryItemInfo appleInfo;
        [SerializeField] private InventoryItemInfo pepperInfo;
        [SerializeField] private InventoryItemInfo beerInfo;
        
        public InventoryWithSlots Inventory => _tester.Inventory;
        private UIInventoryTester _tester;
        private void Start()
        {
            var uiSlots = GetComponentsInChildren<UIInventorySlot>();
            _tester = new UIInventoryTester(pepperInfo, beerInfo, uiSlots);
            _tester.FillSlots();
        }
    }
}
