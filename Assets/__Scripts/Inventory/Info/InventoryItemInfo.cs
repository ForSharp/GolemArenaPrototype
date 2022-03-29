using __Scripts.Inventory.Abstracts;
using UnityEngine;
using UnityEngine.Serialization;

namespace __Scripts.Inventory.Info
{
    [CreateAssetMenu(fileName = "InventoryItemInfo", menuName = "Gameplay/Items/Create New ItemInfo")]
    public class InventoryItemInfo : ScriptableObject, IInventoryItemInfo
    {
        [SerializeField] private string id;
        [SerializeField] private string title;
        [SerializeField] private string description; 
        [SerializeField] private int maxItemsInInventorySlot;
        [SerializeField] private Sprite spriteIcon;
        [FormerlySerializedAs("itemRarity")] [SerializeField] private ItemType itemType;
        [SerializeField] private int price;
        
        public string Id => id;
        public string Title => title;
        public string Description => description;
        public int MaxItemsInInventorySlot => maxItemsInInventorySlot;
        public Sprite SpriteIcon => spriteIcon;
        public ItemType ItemType => itemType;
        public int Price => price;
    }
}