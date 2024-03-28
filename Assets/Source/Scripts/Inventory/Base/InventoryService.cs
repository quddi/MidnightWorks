using System;
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

        public event Action<InventoryIdentifier, ItemParameters> OnItemAdded;

        public event Action<InventoryIdentifier, ItemParameters> OnItemRemoved;

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

        public bool TryAddItem(InventoryIdentifier inventoryIdentifier, ItemParameters itemParameters)
        {
            if (!_inventories.ContainsKey(inventoryIdentifier))
                return false;

            var inventory = _inventories[inventoryIdentifier];

            inventory.AddItem(itemParameters);
            
            OnItemAdded?.Invoke(inventoryIdentifier, itemParameters);
            
            Save();
            
            return true;
        }

        public bool TryRemoveItem(InventoryIdentifier inventoryIdentifier, ItemParameters itemParameters)
        {
            if (!_inventories.ContainsKey(inventoryIdentifier))
                return false;
            
            var inventory = _inventories[inventoryIdentifier];

            var succeed = inventory.TryRemoveItem(itemParameters);

            if (succeed)
            {
                Save();
                OnItemRemoved?.Invoke(inventoryIdentifier, itemParameters);
            }
            
            return succeed;
        }

        public bool CanRemove(InventoryIdentifier inventoryIdentifier, ItemParameters itemParameters)
        {
            throw new NotImplementedException();
        }

        public bool ContainItem(InventoryIdentifier inventoryIdentifier, ItemParameters itemParameters)
        {
            if (!_inventories.ContainsKey(inventoryIdentifier))
                return false;
            
            var inventory = _inventories[inventoryIdentifier];

            return inventory.ContainItem(itemParameters);
        }

        public bool ContainItem(InventoryIdentifier inventoryIdentifier, string itemId)
        {
            if (!_inventories.ContainsKey(inventoryIdentifier))
                return false;
            
            var inventory = _inventories[inventoryIdentifier];
            
            return inventory.ContainItem(itemId);
        }

        public async UniTask StartAsync(CancellationToken _)
        {
            if (!_dataStorageService.Contains(_inventoryServiceConfig.DataStorageKey))
            {
                _inventories = new(_inventoryServiceConfig.Inventories);
                
                Save();
                
                return;
            }

            var data = await _dataStorageService.Load(_inventoryServiceConfig.DataStorageKey);
            
            _inventories = JsonConvert.DeserializeObject<Dictionary<InventoryIdentifier, IInventory>>(data, 
                Constants.JsonSerializerSettings);
        }
    }
}