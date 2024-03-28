namespace NPC
{
    public interface INpcPool
    {
        int ActiveNpcCount { get; }

        Npc DeployNpc(NpcConfig npcConfig);
        
        void ReleaseNpc(Npc npc);
    }
}