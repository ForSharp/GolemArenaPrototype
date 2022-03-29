using __Scripts.GameLoop;
using __Scripts.Inventory.Abstracts;
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
        
        public void OnButtonClicked(Text id)
        {
            if(_inventoryShowcase.Owner.InventoryHelper.InventoryOrganization.inventory.TryToRemove(this, id.text))
            {
                _store.purses.TryGetValue(_inventoryShowcase.Owner, out var money);
                money += _price;
                _store.purses.Remove(_inventoryShowcase.Owner);
                _store.purses.Add(_inventoryShowcase.Owner, money);
                EventContainer.OnItemSold();
            }
            
        }
    }
}