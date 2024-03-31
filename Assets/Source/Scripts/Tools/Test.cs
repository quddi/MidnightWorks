using Inventory;
using Sirenix.OdinInspector;
using VContainer;

namespace Tools
{
    public class Test : SerializedMonoBehaviour
    {
        private IInventoryService _inventoryService;

        [Inject]
        private void Construct(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }
        
        [Button]
        private void Add(InventoryIdentifier inventoryIdentifier, ItemParameters itemParameters)
        {
            _inventoryService.TryAddItem(inventoryIdentifier, itemParameters);
        } 
        
        [Button]
        private void Remove(InventoryIdentifier inventoryIdentifier, ItemParameters itemParameters)
        {
            _inventoryService.TryRemoveItem(inventoryIdentifier, itemParameters);
        } 
    }
}