using System;

namespace Buildings
{
    public interface IBuildingsService
    {
        public event Action OnBuildingPurchased;

        BuildingConfig GetBuildingConfig(string buildingId);
        
        bool TryPurchase(string buildingId);

        bool IsBuilt(string buildingId);
    }
}