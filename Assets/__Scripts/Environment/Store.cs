using System;
using System.Collections.Generic;
using __Scripts.Inventory.Abstracts;
using GameLoop;
using Inventory;
using UnityEngine;
using UnityEngine.UI;

namespace __Scripts.Environment
{
    public class Store : MonoBehaviour
    {
        [SerializeField] private StoreItem storeItem;
        [SerializeField] private GameObject showcase;
        [SerializeField] private Button storeButton;
        [SerializeField] private Text storeButtonText;
        [SerializeField] private GameObject switchButtons;
        [SerializeField] private GameObject inventoryShowcase;
        
        private List<IInventoryItem> _items = new List<IInventoryItem>();
        private ItemContainer _container;

        private void Start()
        {
            _container = ItemContainer.Instance;
            
            _items = _container.GetAllItems();
            
            FillShowcase();
            HideAll();

            EventContainer.PlayerCharacterCreated += ShowButton;
        }

        private void OnDestroy()
        {
            EventContainer.PlayerCharacterCreated -= ShowButton;
        }

        private void HideStore()
        {
            storeButtonText.text = "Open";
            
            showcase.SetActive(false);
            switchButtons.SetActive(false);
            inventoryShowcase.SetActive(false);
        }

        private void ShowStore()
        {
            storeButtonText.text = "Close";
            
            showcase.SetActive(true);
            switchButtons.SetActive(true);
            inventoryShowcase.SetActive(true);
        }
        
        public void OpenShowcase()
        {
            if (!showcase.activeSelf)
            {
                ShowStore();
            }
            else
            {
                HideStore();
            }
            
        }

        private void HideAll()
        {
            HideStore();
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