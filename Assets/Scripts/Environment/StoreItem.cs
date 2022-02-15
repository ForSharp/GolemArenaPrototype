using System;
using Inventory.Abstracts;
using UnityEngine;
using UnityEngine.UI;

namespace Environment
{
    public class StoreItem : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private Text price;
        [SerializeField] private Text title;
        private Store _store;
        public IInventoryItem Item { get; private set; }

        private void Start()
        {
            _store = GetComponentInParent<Store>();
        }

        public void Initialize(IInventoryItem item)
        {
            Item = item;
            icon.sprite = item.Info.SpriteIcon;
            title.text = item.Info.Title;
            price.text = item.Info.Price.ToString();
        }

        public void OnButtonClicked()
        {
            _store.SendItemToPlayer(this);
        }
    }
}