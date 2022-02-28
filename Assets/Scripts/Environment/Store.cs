using System;
using System.Collections.Generic;
using System.Linq;
using GameLoop;
using Inventory;
using Inventory.Abstracts;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Environment
{
    public class Store : MonoBehaviour
    {
        [SerializeField] private StoreItem storeItem;
        [SerializeField] private GameObject showcase;
        [SerializeField] private Button storeButton;
        [SerializeField] private Text storeButtonText;
        
        private List<IInventoryItem> _items = new List<IInventoryItem>();
        private ItemContainer _container;

        private void Start()
        {
            _container = ItemContainer.Instance;
            
            _items = _container.GetAllItems();
            
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
            foreach (var t in _items)
            {
                Instantiate(storeItem, showcase.transform);
                storeItem.Initialize(t);
                t.State.Amount = 1;
            }
        }
        
        public void SendItemToPlayer(string itemId)
        {
            ItemDispenser.DispenseItemToCharacter(itemId, Player.PlayerCharacter);
        }
        
    }
}