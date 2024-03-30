using System.Collections.Generic;
using System.Linq;
using VContainer;
using VContainer.Unity;

namespace NPC
{
    public class NpcService : INpcService, IStartable
    {
        private NpcServiceConfig _npcServiceConfig;

        private Dictionary<string, NpcConfig> _npcConfigs;

        [Inject]
        private void Construct(NpcServiceConfig npcServiceConfig)
        {
            _npcServiceConfig = npcServiceConfig;
        }

        public void Start()
        {
            _npcConfigs = _npcServiceConfig.NpcConfigs.ToDictionary(config => config.Id, config => config);
        }

        public NpcConfig GetNpcConfig(string npcId)
        {
            return _npcConfigs.GetValueOrDefault(npcId);
        }
    }
}