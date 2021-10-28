using Inventory.Abstracts;
using UnityEngine;

namespace Inventory.Info
{
    [CreateAssetMenu(fileName = "InventoryItemInfo", menuName = "Gameplay/Items/Create New ItemInfo")]
    public class InventoryItemInfo : ScriptableObject, IInventoryItemInfo
    {
        [SerializeField] private string id;
        [SerializeField] private string title;
        [SerializeField] private string description; 
        [SerializeField] private int maxItemsInInventorySlot;
        [SerializeField] private Sprite spriteIcon;
        [SerializeField] private ItemRarity itemRarity;
        [SerializeField] private int price;
        
        public string Id => id;
        public string Title => title;
        public string Description => description;
        public int MaxItemsInInventorySlot => maxItemsInInventorySlot;
        public Sprite SpriteIcon => spriteIcon;
        public ItemRarity ItemRarity => itemRarity;
        public int Price => price;
    }
}