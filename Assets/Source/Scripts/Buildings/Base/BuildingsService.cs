using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Transactions;
using Cysharp.Threading.Tasks;
using DataStorage;
using Extensions;
using Inventory;
using Newtonsoft.Json;
using VContainer;
using VContainer.Unity;

namespace Buildings
{
    public class BuildingsService : IBuildingsService, IAsyncStartable
    {
        private IDataStorageService _dataStorageService;
        private IInventoryService _inventoryService;

        private BuildingsServiceConfig _buildingsServiceConfig;
        private HashSet<string> _purchasedBuildingsIds = new();
        private Dictionary<string, BuildingConfig> _buildingConfigs = new();

        public event Action<string> OnBuildingPurchased;

        [Inject]
        private void Construct(BuildingsServiceConfig buildingsServiceConfig, IDataStorageService dataStorageService,
            IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
            _dataStorageService = dataStorageService;
            _buildingsServiceConfig = buildingsServiceConfig;

            _buildingConfigs = _buildingsServiceConfig.BuildingsConfigs.ToDictionary(config => config.Id, config => config);
        }

        private void Save()
        {
            _dataStorageService.SaveImmediately(_buildingsServiceConfig.DataStorageKey,
                JsonConvert.SerializeObject(_purchasedBuildingsIds, Constants.JsonSerializerSettings));
        }

        public BuildingConfig GetBuildingConfig(string buildingId)
        {
            return _buildingConfigs.GetValueOrDefault(buildingId);
        }

        public bool TryPurchase(string buildingId)
        {
            if (IsBuilt(buildingId))
                return false;

            var price = GetBuildingConfig(buildingId).BuildPrice;
            var canPurchase = _inventoryService.ContainItems(
                _buildingsServiceConfig.PurchasingInventoryIdentifier, price);

            if (!canPurchase)
                return false;
            
            foreach (var itemParameters in price)
            {
                if (!_inventoryService.TryRemoveItem(_buildingsServiceConfig.PurchasingInventoryIdentifier, itemParameters))
                    throw new TransactionException($"Could not remove items: [{itemParameters}]");
            }

            _purchasedBuildingsIds.Add(buildingId);
            
            Save();
            
            OnBuildingPurchased?.Invoke(buildingId);

            return true;
        }

        public bool IsBuilt(string buildingId)
        {
            return _purchasedBuildingsIds.Contains(buildingId);
        }

        public void ClaimBuildingReward(string buildingId)
        {
            var config = GetBuildingConfig(buildingId);

            _inventoryService.TryAddItem(_buildingsServiceConfig.PurchasingInventoryIdentifier, config.ExecutionReward);
        }

        public async UniTask StartAsync(CancellationToken _)
        {
            if (!_dataStorageService.Contains(_buildingsServiceConfig.DataStorageKey))
            {
                _purchasedBuildingsIds = _buildingConfigs.Values
                    .Where(buildingConfig => buildingConfig.IsBoughtImmediately)
                    .Select(buildingConfig => buildingConfig.Id)
                    .ToHashSet();
                
                Save();

                return;
            }
            
            var data = await _dataStorageService.Load(_buildingsServiceConfig.DataStorageKey);
            
            _purchasedBuildingsIds = JsonConvert.DeserializeObject<HashSet<string>>(data, Constants.JsonSerializerSettings);
        }
    }
}