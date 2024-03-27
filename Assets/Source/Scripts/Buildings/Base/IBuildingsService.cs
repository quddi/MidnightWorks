using System;

namespace Buildings
{
    public interface IBuildingsService
    {
        public event Action<string> OnBuilt; 
        
        bool TryBuild(string buildingId);

        bool IsBuilt(string buildingId);
    }
}