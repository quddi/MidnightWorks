using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Inventory
{
    public class InventoryServiceInstaller : IInstaller
    {
        [SerializeField] private InventoryServiceConfig _inventoryServiceConfig;
        
        public void Install(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<InventoryService>()
                .WithParameter("inventoryServiceConfig", _inventoryServiceConfig);
        }
    }
}