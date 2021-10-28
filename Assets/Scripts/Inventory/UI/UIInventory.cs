using System;
using Inventory;
using Inventory.Info;
using UnityEngine;

namespace UI
{
    public class UIInventory : MonoBehaviour
    {
        [SerializeField] private InventoryItemInfo appleInfo;
        [SerializeField] private InventoryItemInfo pepperInfo;
        [SerializeField] private InventoryItemInfo beerInfo;
        
        //public InventoryWithSlots Inventory { get; private set; }
        public InventoryWithSlots Inventory => _tester.Inventory;
        private UIInventoryTester _tester;
        private void Start()
        {
            var uiSlots = GetComponentsInChildren<UIInventorySlot>();
            _tester = new UIInventoryTester(appleInfo, pepperInfo, beerInfo, uiSlots);
            _tester.FillSlots();
        }

        // private void Awake()
        // {
        //     Inventory = new InventoryWithSlots(15);
        // }
    }
}
