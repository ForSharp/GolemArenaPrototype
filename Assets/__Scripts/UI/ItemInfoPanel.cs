using System;
using __Scripts.Inventory.Abstracts;
using __Scripts.Inventory.UI;
using UnityEngine;
using UnityEngine.UI;

namespace __Scripts.UI
{
    public class ItemInfoPanel : MonoBehaviour
    {
        [SerializeField] private Image itemImage;
        [SerializeField] private Text itemTitle;
        [SerializeField] private Text itemDescription;
        [SerializeField] private GameObject infoPanel;

        [SerializeField] private GameObject shopBuyPanel;
        [SerializeField] private Text shopBuyTextPrice;
        [SerializeField] private GameObject shopSellPanel;
        [SerializeField] private Text shopSellTextPrice;
        [SerializeField] private GameObject learnSpellPanel;
        [SerializeField] private GameObject activateSpellPanel;
        [SerializeField] private GameObject useConsumablePanel;

        private RectTransform _rectTransform;

        private void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        public void ShowInfo(IInventoryItemInfo info, ItemInfoPanelType panelType, Vector2 pos)
        {
            infoPanel.SetActive(true);
            itemImage.sprite = info.SpriteIcon;
            itemTitle.text = info.Title;
            itemDescription.text = info.Description;
            
            HideAllExtraPanels();
            ShowExtraPanel(panelType, info);

            _rectTransform.transform.position = new Vector3(pos.x, pos.y);
        }

        public void Close()
        {
            infoPanel.SetActive(false);
        }
        
        private void ShowExtraPanel(ItemInfoPanelType panelType, IInventoryItemInfo info)
        {
            switch (panelType)
            {
               case ItemInfoPanelType.ShopBuy:
                   shopBuyPanel.SetActive(true);
                   shopBuyTextPrice.text = info.Price.ToString();
                   break;
               case ItemInfoPanelType.ShopSell:
                   shopSellPanel.SetActive(true);
                   var price = info.Price / 3;
                   shopSellTextPrice.text = price.ToString();
                   break;
               case ItemInfoPanelType.LearnSpell:
                   learnSpellPanel.SetActive(true);
                   break;
               case ItemInfoPanelType.ActivateSpell:
                   activateSpellPanel.SetActive(true);
                   break;
               case ItemInfoPanelType.UseConsumable:
                   useConsumablePanel.SetActive(true);
                   break;
               case ItemInfoPanelType.Empty:
                   HideAllExtraPanels();
                   break;
            }
        }

        private UIInventorySlot _currentSlot;
        
        public void SetUIInventorySlot(UIInventorySlot slot)
        {
            _currentSlot = slot;
        }

        public void UseItem()
        {
            _currentSlot.UseItem();
            Close();
        }
        
        private void HideAllExtraPanels()
        {
            shopBuyPanel.SetActive(false);
            shopSellPanel.SetActive(false);
            learnSpellPanel.SetActive(false);
            activateSpellPanel.SetActive(false);
            useConsumablePanel.SetActive(false);
        }
    }

    public enum ItemInfoPanelType
    {
        ShopBuy,
        ShopSell,
        LearnSpell,
        ActivateSpell,
        UseConsumable,
        Empty
    }
}
