using VContainer;
using VContainer.Unity;

namespace DataStorage
{
    public class DataStorageInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<DataStorageService>().AsSelf();
        }
    }
}