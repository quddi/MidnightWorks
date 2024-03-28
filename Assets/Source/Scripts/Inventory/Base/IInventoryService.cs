﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Inventory
{
    public interface IInventoryService
    {
        public event Action<InventoryIdentifier, ItemParameters> OnItemAdded; 
        public event Action<InventoryIdentifier, ItemParameters> OnItemRemoved; 
        
        public bool TryAddItem(InventoryIdentifier inventoryIdentifier, ItemParameters itemParameters);
        
        public bool TryRemoveItem(InventoryIdentifier inventoryIdentifier, ItemParameters itemParameters);

        public bool ContainItem(InventoryIdentifier inventoryIdentifier, string itemId);
        
        public bool ContainItem(InventoryIdentifier inventoryIdentifier, ItemParameters itemParameters);

        public bool ContainItems(InventoryIdentifier inventoryIdentifier, List<ItemParameters> itemParameters)
        {
            return itemParameters.All(param => ContainItem(inventoryIdentifier, param));
        }
    }
}