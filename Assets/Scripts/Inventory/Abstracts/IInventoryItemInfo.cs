using UnityEngine;

namespace Inventory.Abstracts
{
    public interface IInventoryItemInfo
    {
        string Id { get; }
        string Title { get; }
        string Description { get; }
        int MaxItemsInInventorySlot { get; }
        Sprite SpriteIcon { get; }
        ItemRarity ItemRarity { get; }
        int Price { get; }
    }
}