using __Scripts.Inventory.Abstracts;
using Environment;
using Inventory.Abstracts;
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

        public void Initialize(IInventoryItem item)
        {
            icon.sprite = item.Info.SpriteIcon;
            title.text = item.Info.Title;
            price.text = item.Info.Price.ToString();
            itemId.text = item.Info.Id;
            quantity.text = item.State.Amount.ToString();
        }
        
        public void OnButtonClicked(Text id)
        {
            //_store.SendItemToPlayer(id.text);
            
            //продать магазину
        }
    }
}