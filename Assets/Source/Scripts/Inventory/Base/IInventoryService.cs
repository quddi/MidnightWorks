using System;

namespace Inventory
{
    public interface IInventoryService
    {
        public event Action<InventoryIdentifier, string, long> OnItemAdded; 
        public event Action<InventoryIdentifier, string, long> OnItemRemoved; 
        
        public bool TryAddItem(InventoryIdentifier inventoryIdentifier, string itemId, long itemsCount);
        
        public bool TryRemoveItem(InventoryIdentifier inventoryIdentifier, string itemId, long itemsCount);

        public bool ContainItem(InventoryIdentifier inventoryIdentifier, string itemId, long itemsCount);
        
        public bool ContainItem(InventoryIdentifier inventoryIdentifier, string itemId);
    }
}