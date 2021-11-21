using Inventory.Abstracts;
using Inventory.DragAndDrop;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory.UI
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

            if (Item.State.Amount > 1)
            {
                textAmount.gameObject.SetActive(true);
                textAmount.text = $"x{slot.Amount.ToString()}";
            }
            else
            {
                textAmount.text = $"x{slot.Amount.ToString()}";
                textAmount.gameObject.SetActive(false);
            }
        }

        private void Cleanup()
        {
            imageIcon.gameObject.SetActive(false);
            textAmount.gameObject.SetActive(false);
        }

    }
}
