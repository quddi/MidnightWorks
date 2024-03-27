namespace Inventory
{
    public interface IInventory
    {
        public void AddItem(ItemParameters itemParameters);
        
        public bool TryRemoveItem(ItemParameters itemParameters);

        public bool ContainItem(ItemParameters itemParameters);
        
        public bool ContainItem(string itemId);
    }
}