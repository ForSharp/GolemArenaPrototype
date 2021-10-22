using DragAndDrop;
using Inventory.Abstracts;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIInventoryItem : UIItem
    {
        [SerializeField] private Image imageIcon;
        [SerializeField] private Text textAmount;

        public IInventoryItem Item { get; private set; }
        
        public void Refresh(IInventorySlot slot)
        {
            if (slot.IsEmpty)
            {
                Cleanup();
                return;
            }

            Item = slot.Item;
            imageIcon.sprite = Item.Info.SpriteIcon;
            imageIcon.gameObject.SetActive(true);

            // var textAmountEnabled = slot.Amount > 1;
            // textAmount.gameObject.SetActive(textAmountEnabled);
            //
            // if (textAmountEnabled)
            // {
            //     textAmount.text = $"x{slot.Amount.ToString()}";
            // }
            textAmount.gameObject.SetActive(true);
            textAmount.text = $"x{slot.Amount.ToString()}";
        }

        private void Cleanup()
        {
            imageIcon.gameObject.SetActive(false);
            textAmount.gameObject.SetActive(false);
        }

    }
}
