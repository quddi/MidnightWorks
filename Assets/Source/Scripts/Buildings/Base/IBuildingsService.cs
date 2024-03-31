using System;

namespace Buildings
{
    public interface IBuildingsService
    {
        public event Action<string> OnBuilt;

        BuildingConfig GetBuildingConfig(string buildingId);
        
        bool TryBuild(string buildingId);

        bool IsBuilt(string buildingId);
    }
}