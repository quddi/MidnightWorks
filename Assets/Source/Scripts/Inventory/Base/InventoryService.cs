using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using DataStorage;
using Extensions;
using Newtonsoft.Json;
using VContainer;
using VContainer.Unity;

namespace Inventory
{
    public class InventoryService : IInventoryService, IAsyncStartable
    {
        private InventoryServiceConfig _inventoryServiceConfig;
        
        private IDataStorageService _dataStorageService;

        private Dictionary<InventoryIdentifier, IInventory> _inventories;

        [Inject]
        private void Construct(InventoryServiceConfig inventoryServiceConfig, IDataStorageService dataStorageService)
        {
            _dataStorageService = dataStorageService;
            _inventoryServiceConfig = inventoryServiceConfig;
        }

        private void Save()
        {
            _dataStorageService.SaveLazily(_inventoryServiceConfig.DataStorageKey,
                JsonConvert.SerializeObject(_inventories, Constants.JsonSerializerSettings));
        }
        
        public bool TryAddItem(InventoryIdentifier inventoryIdentifier, string itemId, long itemsCount)
        {
            if (!_inventories.ContainsKey(inventoryIdentifier))
                return false;

            var inventory = _inventories[inventoryIdentifier];

            inventory.AddItem(itemId, itemsCount);
            
            Save();
            
            return true;
        }

        public bool TryRemoveItem(InventoryIdentifier inventoryIdentifier, string itemId, long itemsCount)
        {
            if (!_inventories.ContainsKey(inventoryIdentifier))
                return false;
            
            var inventory = _inventories[inventoryIdentifier];

            var succeed = inventory.TryRemoveItem(itemId, itemsCount);
            
            if (succeed)
                Save();
            
            return succeed;
        }

        public bool ContainItem(InventoryIdentifier inventoryIdentifier, string itemId, long itemsCount)
        {
            if (!_inventories.ContainsKey(inventoryIdentifier))
                return false;
            
            var inventory = _inventories[inventoryIdentifier];
            
            return inventory.ContainItem(itemId, itemsCount);
        }

        public bool ContainItem(InventoryIdentifier inventoryIdentifier, string itemId)
        {
            if (!_inventories.ContainsKey(inventoryIdentifier))
                return false;
            
            var inventory = _inventories[inventoryIdentifier];
            
            return inventory.ContainItem(itemId);
        }

        public async UniTask StartAsync(CancellationToken cancellation)
        {
            if (!_dataStorageService.Contains(_inventoryServiceConfig.DataStorageKey))
            {
                _inventories = new(_inventoryServiceConfig.Inventories);
                
                return;
            }

            var data = await _dataStorageService.Load(_inventoryServiceConfig.DataStorageKey);
            
            _inventories = JsonConvert.DeserializeObject<Dictionary<InventoryIdentifier, IInventory>>(data, 
                Constants.JsonSerializerSettings);
        }
    }
}