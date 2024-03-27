using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using DataStorage;
using Extensions;
using Newtonsoft.Json;
using VContainer;
using VContainer.Unity;

namespace Buildings
{
    public class BuildingsService : IBuildingsService, IAsyncStartable
    {
        private BuildingsServiceConfig _buildingsServiceConfig;
        private IDataStorageService _dataStorageService;

        private HashSet<string> _boughtBuildingsIds = new();
        private Dictionary<string, BuildingConfig> _buildingConfigs = new();

        public event Action<string> OnBuilt;

        [Inject]
        private void Construct(BuildingsServiceConfig buildingsServiceConfig, IDataStorageService dataStorageService)
        {
            _dataStorageService = dataStorageService;
            _buildingsServiceConfig = buildingsServiceConfig;
        }

        private void Save()
        {
            _dataStorageService.SaveLazily(_buildingsServiceConfig.DataStorageKey,
                JsonConvert.SerializeObject(_boughtBuildingsIds, Constants.JsonSerializerSettings));
        }
        
        public bool TryBuild(string buildingId)
        {
            if (IsBuilt(buildingId))
                return false;
            
            //TODO: Paying

            return true;
        }

        public bool IsBuilt(string buildingId)
        {
            return _boughtBuildingsIds.Contains(buildingId);
        }

        public async UniTask StartAsync(CancellationToken _)
        {
            if (!_dataStorageService.Contains(_buildingsServiceConfig.DataStorageKey))
            {
                _boughtBuildingsIds = _buildingConfigs.Values
                    .Where(buildingConfig => buildingConfig.IsBoughtImmediately)
                    .Select(buildingConfig => buildingConfig.Id)
                    .ToHashSet();
                
                Save();

                return;
            }
            
            var data = await _dataStorageService.Load(_buildingsServiceConfig.DataStorageKey);
            
            _boughtBuildingsIds = JsonConvert.DeserializeObject<HashSet<string>>(data, Constants.JsonSerializerSettings);
        }
    }
}