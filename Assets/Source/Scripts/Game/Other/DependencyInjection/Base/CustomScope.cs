using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game
{
    public abstract class CustomScope : LifetimeScope
    {
        [SerializeField] private InstallersContainer _installersContainer;

        [Inject]
        private void Construct(IObjectResolver objectResolver)
        {
            if (_installersContainer == null) return;
            
            _installersContainer.InjectableServicesInstallers.ForEach(objectResolver.Inject);
        }
        
        protected override void Configure(IContainerBuilder builder)
        {
            if (_installersContainer == null) return;
            
            _installersContainer.InjectableServicesInstallers.ForEach(service => service.Install(builder));
            _installersContainer.DefaultServicesInstallers.ForEach(service => service.Install(builder));
        }
    }
}