namespace __Scripts.Inventory.Abstracts
{
    public interface IInventoryItemState
    {
        int Amount { get; set; }
        bool IsEquipped { get; set; }
    }
}