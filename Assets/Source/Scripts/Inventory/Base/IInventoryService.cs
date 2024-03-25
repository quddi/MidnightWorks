namespace Inventory
{
    public interface IInventoryService
    {
        public bool TryAddItem(InventoryIdentifier inventoryIdentifier, string itemId, long itemsCount);
        
        public bool TryRemoveItem(InventoryIdentifier inventoryIdentifier, string itemId, long itemsCount);

        public bool ContainItem(InventoryIdentifier inventoryIdentifier, string itemId, long itemsCount);
        
        public bool ContainItem(InventoryIdentifier inventoryIdentifier, string itemId);
    }
}