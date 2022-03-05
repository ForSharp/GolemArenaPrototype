using System;
using System.Collections.Generic;
using __Scripts.GameLoop;
using __Scripts.Inventory;
using __Scripts.Inventory.Abstracts;
using CharacterEntity.State;
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

        public readonly Dictionary<ChampionState, int> purses = new Dictionary<ChampionState, int>();

        private void Start()
        {
            _container = ItemContainer.Instance;
            
            _items = _container.GetAllItems();
            
            FillShowcase();
            HideAll();

            EventContainer.PlayerCharacterCreated += ShowButton;
            Game.AllChampionsAreReady += CreatePurses;
        }

        private void OnDestroy()
        {
            EventContainer.PlayerCharacterCreated -= ShowButton;
            Game.AllChampionsAreReady -= CreatePurses;
        }
        
        private void CreatePurses()
        {
            foreach (var champion in Game.AllChampionsInSession)
            {
                purses.Add(champion, 500);
            }
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
            purses.TryGetValue(Player.PlayerCharacter, out var cash);
            var price = ItemContainer.Instance.GetItemById(itemId).Info.Price;
            if (cash >= price)
            {
                purses.Remove(Player.PlayerCharacter);
                purses.Add(Player.PlayerCharacter, cash - price);
                ItemDispenser.DispenseItemToCharacter(itemId, Player.PlayerCharacter);
                EventContainer.OnItemSold();
            }
            else
            {
                //не хватает денег
            }
        }
        
    }
}