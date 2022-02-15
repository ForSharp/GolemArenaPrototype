using System;
using System.Collections.Generic;
using GameLoop;
using Inventory;
using Inventory.Abstracts;
using UnityEngine;
using UnityEngine.UI;

namespace Environment
{
    public class Store : MonoBehaviour
    {
        [SerializeField] private ItemContainer itemContainer;
        [SerializeField] private StoreItem storeItem;
        [SerializeField] private GameObject showcase;
        [SerializeField] private Button storeButton;
        [SerializeField] private Text storeButtonText;
        private List<IInventoryItem> _items = new List<IInventoryItem>();

        private void Start()
        {
            //_items = itemContainer.GetAllItems();
            _items = ItemContainer.Instance.GetAllItems();
            FillShowcase();
            CloseShowcase();
        }

        public void CloseShowcase()
        {
            storeButtonText.text = "Open";
            showcase.SetActive(false);
        }

        public void OpenShowcase()
        {
            if (!showcase.activeSelf)
            {
                storeButtonText.text = "Close";
                showcase.SetActive(true);
            }
            else
            {
                storeButtonText.text = "Open";
                showcase.SetActive(false);
            }
            
        }

        private void HideAll()
        {
            CloseShowcase();
            storeButton.gameObject.SetActive(false);
        }

        private void ShowButton()
        {
            storeButton.gameObject.SetActive(true);
        }

        private void FillShowcase()
        {
            //showcase.SetActive(true);
            foreach (var item in _items)
            {
                Instantiate(storeItem, showcase.transform);
                storeItem.Initialize(item);
            }
        }
        public void SendItemToPlayer(StoreItem item)
        {
            var newItem = item.Item;
            newItem.State.Amount = 1;
            Player.PlayerCharacter.InventoryHelper.AddItem(newItem);
        }
        
    }
}