using Inventory.Abstracts;
using UnityEngine;

namespace Inventory
{
    [CreateAssetMenu(fileName = "InventoryItemInfo", menuName = "Gameplay/Items/Create New ItemInfo")]
    public class InventoryItemInfo : ScriptableObject, IInventoryItemInfo
    {
        [SerializeField] private string id;
        [SerializeField] private string title;
        [SerializeField] private string description; 
        [SerializeField] private int maxItemsInInventorySlot;
        [SerializeField] private Sprite spriteIcon;

        public string Id => id;
        public string Title => title;
        public string Description => description;
        public int MaxItemsInInventorySlot => maxItemsInInventorySlot;
        public Sprite SpriteIcon => spriteIcon;
    }
}