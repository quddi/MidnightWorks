using System;

namespace Inventory
{
    public interface IInventoryService
    {
        public event Action<InventoryIdentifier, ItemParameters> OnItemAdded; 
        public event Action<InventoryIdentifier, ItemParameters> OnItemRemoved; 
        
        public bool TryAddItem(InventoryIdentifier inventoryIdentifier, ItemParameters itemParameters);
        
        public bool TryRemoveItem(InventoryIdentifier inventoryIdentifier, ItemParameters itemParameters);

        public bool CanRemove(InventoryIdentifier inventoryIdentifier, ItemParameters itemParameters);

        public bool ContainItem(InventoryIdentifier inventoryIdentifier, ItemParameters itemParameters);
        
        public bool ContainItem(InventoryIdentifier inventoryIdentifier, string itemId);
    }
}