﻿using __Scripts.Inventory.Abstracts;
using Environment;
using Inventory.Abstracts;
using UnityEngine;
using UnityEngine.UI;

namespace __Scripts.Environment
{
    public class StoreItem : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private Text price;
        [SerializeField] private Text title;
        [SerializeField] private Text itemId;
        [SerializeField] private Text quantity;
        private Store _store;

        private void Start()
        {
            _store = GetComponentInParent<Store>();
        }

        public void Initialize(IInventoryItem item)
        {
            icon.sprite = item.Info.SpriteIcon;
            title.text = item.Info.Title;
            price.text = item.Info.Price.ToString();
            itemId.text = item.Info.Id;
        }

        public void OnButtonClicked(Text id)
        {
            _store.SendItemToPlayer(id.text);
        }
    }
}