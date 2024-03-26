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
            _installersContainer.InjectableServicesInstallers.ForEach(objectResolver.Inject);
        }
        
        protected override void Configure(IContainerBuilder builder)
        {
            _installersContainer.InjectableServicesInstallers.ForEach(service => service.Install(builder));
            _installersContainer.DefaultServicesInstallers.ForEach(service => service.Install(builder));
        }
    }
}