using System.Collections.Generic;
using System.Linq;
using Extensions;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NPC
{
    [CreateAssetMenu(fileName = "NPC Service Config", menuName = "ScriptableObjects/NPC/NPC service config")]
    public class NpcServiceConfig : SerializedScriptableObject
    {
        [field: SerializeField] public List<NpcConfig> NpcConfigs { get; private set; } = new();
        
#if UNITY_EDITOR
        [Button]
        public void AddAllItemsConfigs()
        {
            NpcConfigs = ExtensionMethods.GetAllScriptableObjects<NpcConfig>().ToList();
            
            UnityEditor.EditorUtility.SetDirty(this);
        }
        
        public static IEnumerable<string> NpcIds
        {
            get
            {
                var instance = ExtensionMethods.GetAllScriptableObjects<NpcServiceConfig>().First();

                return instance.NpcConfigs.Select(config => config.Id);
            }
        }
#endif
    }
}