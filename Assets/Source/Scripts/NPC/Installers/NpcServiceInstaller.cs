using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace NPC
{
    public class NpcServiceInstaller : IInstaller
    {
        [SerializeField] private NpcServiceConfig _npcServiceConfig;
        
        public void Install(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<NpcService>().WithParameter("npcServiceConfig", _npcServiceConfig);
        }
    }
}