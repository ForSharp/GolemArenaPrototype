using __Scripts.GameLoop;
using __Scripts.Inventory;
using __Scripts.Inventory.Abstracts;
using __Scripts.UI;
using UnityEngine;
using UnityEngine.UI;

namespace __Scripts.Environment
{
    public class InventoryShowcaseItem : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private Text price;
        [SerializeField] private Text title;
        [SerializeField] private Text itemId;
        [SerializeField] private Text quantity;

        private InventoryShowcase _inventoryShowcase;
        private Store _store;
        private int _quantity;
        private int _price;
        
        
        private void Start()
        {
            _inventoryShowcase = GetComponentInParent<InventoryShowcase>();
            _store = GetComponentInParent<Store>();
        }

        public void Initialize(IInventoryItem item)
        {
            icon.sprite = item.Info.SpriteIcon;
            title.text = item.Info.Title;
            _price = item.Info.Price / 3;
            price.text = _price.ToString();
            itemId.text = item.Info.Id;
            quantity.text = item.State.Amount.ToString();
            _quantity = item.State.Amount;
            
            PreventExcEnabling(_quantity);
        }

        private void PreventExcEnabling(int itemQuantity)
        {
            if (itemQuantity <= 0)
            {
                gameObject.SetActive(false);
            }
        }
        
        private string _currentItemId;
        
        public void OnButtonClicked(Text id)
        {
            _currentItemId = id.text;
            
            _inventoryShowcase.SetInventoryShowcaseItem(this);
        }

        private Vector2 _pos;
        public void SetPosition(Transform tr)
        {
            _pos.x = tr.position.x;
            _pos.y = tr.position.y;
            
            ShowInfo();
        }

        private void ShowInfo()
        {
            var itemInfo = ItemContainer.Instance.GetItemById(_currentItemId).Info;
            _store.ItemInfoPanel.ShowInfo(itemInfo, ItemInfoPanelType.ShopSell, _pos);
        }

        public void SellItem()
        {
            if(_inventoryShowcase.Owner.InventoryHelper.InventoryOrganization.inventory.TryToRemove(this, _currentItemId))
            {
                _store.purses.TryGetValue(_inventoryShowcase.Owner, out var money);
                money += _price;
                _store.purses.Remove(_inventoryShowcase.Owner);
                _store.purses.Add(_inventoryShowcase.Owner, money);
                EventContainer.OnItemSold();
            }
            
            _store.ItemInfoPanel.Close();
        }
    }
}