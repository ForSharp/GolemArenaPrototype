using UnityEngine;

namespace __Scripts.Inventory.Abstracts
{
    public interface IInventoryItemInfo
    {
        string Id { get; }
        string Title { get; }
        string Description { get; }
        int MaxItemsInInventorySlot { get; }
        Sprite SpriteIcon { get; }
        ItemType ItemType { get; }
        int Price { get; }
    }
}