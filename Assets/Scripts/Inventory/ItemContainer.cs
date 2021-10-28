using System;
using System.Collections.Generic;
using Inventory.Abstracts;
using Inventory.Info;
using Inventory.Items;
using UnityEngine;

namespace Inventory
{
    public class ItemContainer : MonoBehaviour
    {
        [Header("InventoryItemInfo")]
        [SerializeField] private InventoryItemInfo appleItemInfo;
        [SerializeField] private InventoryItemInfo beerItemInfo;
        [SerializeField] private InventoryItemInfo pepperItemInfo;
        [SerializeField] private InventoryItemInfo warHelmetItemInfo;
        [Header("ArtefactInfo")]
        [SerializeField] private ArtefactInfo warHelmetArtefactInfo;
        [Header("ConsumableInfo")]
        [SerializeField] private ArtefactInfo _consumableInfo;
        [Header("PotionInfo")]
        [SerializeField] private ArtefactInfo _potionInfo;
        [Header("SpellsInfo")]
        [SerializeField] private ArtefactInfo _spellsInfo;
        
        public List<IInventoryItem> AllItems { get; private set; } = new List<IInventoryItem>();
        public static ItemContainer Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            FillItemsList();
        }

        private void FillItemsList()
        {
            AllItems.Add(new Apple(appleItemInfo));
            AllItems.Add(new Beer(beerItemInfo));
            AllItems.Add(new Pepper(pepperItemInfo));
            AllItems.Add(new WarHelmet(warHelmetItemInfo, warHelmetArtefactInfo));
        }
    }
}