using Sirenix.OdinInspector;
using UnityEngine;

namespace NPC
{
    [CreateAssetMenu(fileName = "NPC Config", menuName = "ScriptableObjects/NPC/NPC config")]
    public class NpcConfig : SerializedScriptableObject
    {
        [field: SerializeField] public string Id { get; private set; }
        
        [field: SerializeField] public Npc Prefab { get; private set; }
    }
}