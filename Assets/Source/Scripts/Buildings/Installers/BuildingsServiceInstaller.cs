using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Buildings
{
    public class BuildingsServiceInstaller : IInstaller
    {
        [SerializeField] private BuildingsServiceConfig _buildingsServiceConfig;
        
        public void Install(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<BuildingsService>()
                .WithParameter("buildingsServiceConfig", _buildingsServiceConfig);
        }
    }
}