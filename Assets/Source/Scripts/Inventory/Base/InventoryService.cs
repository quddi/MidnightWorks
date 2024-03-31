﻿using System;
using System.Collections.Generic;
using System.Linq;
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

        private Dictionary<string, ItemConfig> _itemConfigs;
        private Dictionary<string, IInventory> _inventories;

        public int CraftItemCount => _inventoryServiceConfig.CraftItemCount;

        public event Action<InventoryIdentifier, ItemParameters> OnItemAdded;

        public event Action<InventoryIdentifier, ItemParameters> OnItemRemoved;

        [Inject]
        private void Construct(InventoryServiceConfig inventoryServiceConfig, IDataStorageService dataStorageService)
        {
            _dataStorageService = dataStorageService;
            _inventoryServiceConfig = inventoryServiceConfig;

            _itemConfigs = _inventoryServiceConfig.ItemsConfigs.ToDictionary(config => config.Id, config => config);
        }

        private void Save()
        {
            _dataStorageService.SaveLazily(_inventoryServiceConfig.DataStorageKey,
                JsonConvert.SerializeObject(_inventories, Constants.JsonSerializerSettings));
        }

        public ItemConfig GetItemConfig(string itemId)
        {
            return _itemConfigs.GetValueOrDefault(itemId);
        }

        public long GetItemsCount(InventoryIdentifier inventoryIdentifier, string itemId)
        {
            if (!_inventories.ContainsKey(inventoryIdentifier.Id))
                return 0;

            var inventory = _inventories[inventoryIdentifier.Id];

            return inventory.GetItemsCount(itemId);
        }

        public bool TryAddItem(InventoryIdentifier inventoryIdentifier, ItemParameters itemParameters)
        {
            if (!_inventories.ContainsKey(inventoryIdentifier.Id))
                return false;

            var inventory = _inventories[inventoryIdentifier.Id];

            inventory.AddItem(itemParameters);
            
            OnItemAdded?.Invoke(inventoryIdentifier, itemParameters);
            
            Save();
            
            return true;
        }

        public bool TryRemoveItem(InventoryIdentifier inventoryIdentifier, ItemParameters itemParameters)
        {
            if (!_inventories.ContainsKey(inventoryIdentifier.Id))
                return false;
            
            var inventory = _inventories[inventoryIdentifier.Id];

            var succeed = inventory.TryRemoveItem(itemParameters);

            if (succeed)
            {
                Save();
                OnItemRemoved?.Invoke(inventoryIdentifier, itemParameters);
            }
            
            return succeed;
        }

        public bool ContainItem(InventoryIdentifier inventoryIdentifier, ItemParameters itemParameters)
        {
            if (!_inventories.ContainsKey(inventoryIdentifier.Id))
                return false;
            
            var inventory = _inventories[inventoryIdentifier.Id];

            return inventory.ContainItem(itemParameters);
        }

        public bool ContainItem(InventoryIdentifier inventoryIdentifier, string itemId)
        {
            if (!_inventories.ContainsKey(inventoryIdentifier.Id))
                return false;
            
            var inventory = _inventories[inventoryIdentifier.Id];
            
            return inventory.ContainItem(itemId);
        }

        public async UniTask StartAsync(CancellationToken _)
        {
            if (!_dataStorageService.Contains(_inventoryServiceConfig.DataStorageKey))
            {
                _inventories = _inventoryServiceConfig.Inventories
                    .ToDictionary(pair => pair.Key.Id, pair => pair.Value.GetCopy());
                
                Save();
                
                return;
            }

            var data = await _dataStorageService.Load(_inventoryServiceConfig.DataStorageKey);
            
            _inventories = JsonConvert.DeserializeObject<Dictionary<string, IInventory>>(data, 
                Constants.JsonSerializerSettings);
        }
    }
}