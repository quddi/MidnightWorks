using System.Collections.Generic;

namespace NPC
{
    public interface INpcService
    {
        NpcConfig GetNpcConfig(string npcId);
    }
}