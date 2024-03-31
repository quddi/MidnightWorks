using System;
using System.Collections.Generic;
using System.Linq;
using Extensions;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
using VContainer;

namespace Buildings
{
    public class BuildingsPool : MonoBehaviour
    {
        [SerializeField, TabGroup("Components")] private List<Building> _freeBuildings = new();

        private HashSet<Building> _notBuiltBuildings;
        
        private IBuildingsService _buildingsService;

        public bool ContainsFreeBuildings => _freeBuildings.Any();

        [Inject]
        private void Construct(IBuildingsService buildingsService)
        {
            _buildingsService = buildingsService;

            _notBuiltBuildings = _freeBuildings.Where(building => !buildingsService.IsBuilt(building.Id)).ToHashSet();
            
            foreach (var notBuiltBuilding in _notBuiltBuildings)
            {
                _freeBuildings.Remove(notBuiltBuilding);
            }
            
            _buildingsService.OnBuildingPurchased += OnBuildingPurchasedHandler;
        }

        public Building GetRandomBuilding()
        {
            if (!ContainsFreeBuildings)
                throw new InvalidOperationException("Trying to take a free building when there is no one!");

            return _freeBuildings.SnatchRandom();
        }

        public void ReleaseBuilding(Building building)
        {
            if (_freeBuildings.Contains(building))
                throw new ArgumentException($"Trying to release [{building}] building, but it is already in the pool!");
            
            _freeBuildings.Add(building);
        }

        private void OnBuildingPurchasedHandler(string buildingId)
        {
            var building = _notBuiltBuildings.First(building => building.Id == buildingId);

            _notBuiltBuildings.Remove(building);
            
            _freeBuildings.Add(building);
        }

        private void OnDestroy()
        {
            _buildingsService.OnBuildingPurchased -= OnBuildingPurchasedHandler;
        }
    }
}