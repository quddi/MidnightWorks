namespace Inventory
{
    public interface IInventory
    {
        public bool TryAddItem(string itemId, long itemsCount);
        
        public bool TryRemoveItem(string itemId, long itemsCount);

        public bool ContainItem(string itemId, long itemCount);
        
        public bool ContainItem(string itemId);
    }
}