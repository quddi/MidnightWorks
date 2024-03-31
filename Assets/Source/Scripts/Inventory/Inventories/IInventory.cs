using System.Collections.Generic;
using System.Linq;

namespace Inventory
{
    public interface IInventory
    {
        public long GetItemsCount(string itemId);
        
        public void AddItem(ItemParameters itemParameters);
        
        public bool TryRemoveItem(ItemParameters itemParameters);

        public bool ContainItem(string itemId);
        
        public bool ContainItem(ItemParameters itemParameters);

        public bool ContainItems(List<ItemParameters> itemParameters)
        {
            return itemParameters.All(ContainItem);
        }
    }
}