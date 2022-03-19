using System;
using System.Collections.Generic;
using __Scripts.CharacterEntity.State;
using __Scripts.Controller;
using __Scripts.GameLoop;
using __Scripts.Inventory;
using __Scripts.Inventory.Abstracts;
using CharacterEntity.State;
using GameLoop;
using Inventory;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace __Scripts.Environment
{
    public class Store : MonoBehaviour
    {
        [SerializeField] private StoreItem storeItem;
        [SerializeField] private GameObject showcaseContainer;
        [SerializeField] private Button storeButton;
        [SerializeField] private Text storeButtonText;
        [SerializeField] private GameObject switchButtons;
        [SerializeField] private GameObject inventoryShowcase;
        
        [SerializeField] public GameObject showcaseArtefacts;
        [SerializeField] public GameObject showcaseConsumables;
        [SerializeField] public GameObject showcasePotions;
        [SerializeField] public GameObject showcaseSpells;

        [SerializeField] private Image buttonArtefacts;
        [SerializeField] private Image buttonConsumables;
        [SerializeField] private Image buttonPotions;
        [SerializeField] private Image buttonSpells;
        
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
                purses.Add(champion, Settings.Instance.StartMoney);
            }
        }

        private void HideStore()
        {
            storeButtonText.text = "Open";
            
            showcaseContainer.SetActive(false);
            switchButtons.SetActive(false);
            inventoryShowcase.SetActive(false);
        }

        private void ShowStore()
        {
            storeButtonText.text = "Close";
            
            showcaseContainer.SetActive(true);
            switchButtons.SetActive(true);
            inventoryShowcase.SetActive(true);
            
            SetShowcase("Artefact");
        }

        public void SetShowcase(string itemType)
        {
            switch (itemType)
            {
                case "Artefact":
                    showcaseArtefacts.SetActive(true);
                    buttonArtefacts.color = new Color(buttonArtefacts.color.r, buttonArtefacts.color.g,
                        buttonArtefacts.color.b, 1);
                    buttonConsumables.color = new Color(buttonConsumables.color.r, buttonConsumables.color.g,
                        buttonConsumables.color.b, 0.5f);
                    buttonPotions.color = new Color(buttonPotions.color.r, buttonPotions.color.g,
                        buttonPotions.color.b, 0.5f);
                    buttonSpells.color = new Color(buttonSpells.color.r, buttonSpells.color.g,
                        buttonSpells.color.b, 0.5f);
                    
                    showcaseConsumables.SetActive(false);
                    showcasePotions.SetActive(false);
                    showcaseSpells.SetActive(false);
                    break;
                case "Consumable":
                    showcaseConsumables.SetActive(true);
                    buttonArtefacts.color = new Color(buttonArtefacts.color.r, buttonArtefacts.color.g,
                        buttonArtefacts.color.b, 0.5f);
                    buttonConsumables.color = new Color(buttonConsumables.color.r, buttonConsumables.color.g,
                        buttonConsumables.color.b, 1);
                    buttonPotions.color = new Color(buttonPotions.color.r, buttonPotions.color.g,
                        buttonPotions.color.b, 0.5f);
                    buttonSpells.color = new Color(buttonSpells.color.r, buttonSpells.color.g,
                        buttonSpells.color.b, 0.5f);
                    
                    showcaseArtefacts.SetActive(false);
                    showcasePotions.SetActive(false);
                    showcaseSpells.SetActive(false);
                    break;
                case "Potion":
                    showcasePotions.SetActive(true);
                    buttonArtefacts.color = new Color(buttonArtefacts.color.r, buttonArtefacts.color.g,
                        buttonArtefacts.color.b, 0.5f);
                    buttonConsumables.color = new Color(buttonConsumables.color.r, buttonConsumables.color.g,
                        buttonConsumables.color.b, 0.5f);
                    buttonPotions.color = new Color(buttonPotions.color.r, buttonPotions.color.g,
                        buttonPotions.color.b, 1);
                    buttonSpells.color = new Color(buttonSpells.color.r, buttonSpells.color.g,
                        buttonSpells.color.b, 0.5f);
                    
                    showcaseArtefacts.SetActive(false);
                    showcaseConsumables.SetActive(false);
                    showcaseSpells.SetActive(false);
                    break;
                case "Spell":
                    showcaseSpells.SetActive(true);
                    buttonArtefacts.color = new Color(buttonArtefacts.color.r, buttonArtefacts.color.g,
                        buttonArtefacts.color.b, 0.5f);
                    buttonConsumables.color = new Color(buttonConsumables.color.r, buttonConsumables.color.g,
                        buttonConsumables.color.b, 0.5f);
                    buttonPotions.color = new Color(buttonPotions.color.r, buttonPotions.color.g,
                        buttonPotions.color.b, 0.5f);
                    buttonSpells.color = new Color(buttonSpells.color.r, buttonSpells.color.g,
                        buttonSpells.color.b, 1);
                    
                    showcaseArtefacts.SetActive(false);
                    showcaseConsumables.SetActive(false);
                    showcasePotions.SetActive(false);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        
        public void OpenShowcase()
        {
            if (!showcaseContainer.activeSelf)
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
                switch (t.Info.ItemType)
                {
                    case ItemType.Artefact:
                        var artItem = Instantiate(storeItem, showcaseArtefacts.transform);
                        artItem.Initialize(t);
                        t.State.Amount = 1;
                        break;
                    case ItemType.Consumable:
                        var conItem = Instantiate(storeItem, showcaseConsumables.transform);
                        conItem.Initialize(t);
                        t.State.Amount = 1;
                        break;
                    case ItemType.Potion:
                        var potItem = Instantiate(storeItem, showcasePotions.transform);
                        potItem.Initialize(t);
                        t.State.Amount = 1;
                        break;
                    case ItemType.Spell:
                        var spellItem = Instantiate(storeItem, showcaseSpells.transform);
                        spellItem.Initialize(t);
                        t.State.Amount = 1;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
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