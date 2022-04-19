using __Scripts.CharacterEntity.State;
using __Scripts.GameLoop;
using UnityEngine;
using UnityEngine.UI;

namespace __Scripts.Environment
{
    public class InventoryShowcase : MonoBehaviour
    {
        [SerializeField] private InventoryShowcaseItem[] cells;
        [SerializeField] private Text cash;

        private Store _store;
        public ChampionState Owner { get; private set; }

        private void Awake()
        {
            DisableAllCells();
            _store = GetComponentInParent<Store>();
            EventContainer.ItemSold += SetCashValue;
        }

        private void OnEnable()
        {
            SetOwner(Player.PlayerCharacter);
        }

        public void SetOwner(ChampionState owner)
        {
            if (Owner)
            {
                Owner.InventoryHelper.InventoryOrganization.inventory.InventoryStateChanged -= FillCells;
            }
            
            Owner = owner;
            
            SetCashValue();
            
            FillCells(this);

            Owner.InventoryHelper.InventoryOrganization.inventory.InventoryStateChanged += FillCells;
        }

        private void SetCashValue()
        {
            _store.purses.TryGetValue(Owner, out var money);
            cash.text = money.ToString();
        }

        private void FillCells(object sender)
        {
            DisableAllCells();
            
            for (int i = 0; i < cells.Length; i++)
            {
                var item = Owner.InventoryHelper.NonEquippingSlots[i].GetSlotItem();
                if (item != null)
                {
                    cells[i].gameObject.SetActive(true);
                    cells[i].Initialize(item);
                }
                
            }
        }

        private void DisableAllCells()
        {
            foreach (var cell in cells)
            {
                cell.gameObject.SetActive(false);
            }
        }

        private void OnDestroy()
        {
            if (Owner)
            {
                Owner.InventoryHelper.InventoryOrganization.inventory.InventoryStateChanged -= FillCells;
            }
            
            EventContainer.ItemSold -= SetCashValue;
            
        }

        private InventoryShowcaseItem _currentItem;
        public void SetInventoryShowcaseItem(InventoryShowcaseItem item)
        {
            _currentItem = item;
        }

        public void SellItem()
        {
            _currentItem.SellItem();
        }
    }
}
