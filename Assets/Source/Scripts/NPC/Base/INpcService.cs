namespace NPC
{
    public interface INpcService
    {
        Npc NpcPrefab { get; }
        
        NpcConfig GetNpcConfig(string npcId);
    }
}