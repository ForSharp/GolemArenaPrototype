namespace __Scripts.Inventory.Abstracts
{
    public interface IInventory
    {
        int Capacity { get; set; }
        bool IsFull { get; }
        
        IInventoryItem GetItem(string itemId);
        IInventoryItem[] GetAllItems();
        IInventoryItem[] GetAllItems(string itemId);
        IInventoryItem[] GetEquippedItems();
        int GetItemAmount(string itemId);
        bool TryToAdd(object sender, IInventoryItem item);
        bool TryToRemove(object sender, string itemId, int amount = 1);
        bool HasItem(string itemId, out IInventoryItem item);
    }
}