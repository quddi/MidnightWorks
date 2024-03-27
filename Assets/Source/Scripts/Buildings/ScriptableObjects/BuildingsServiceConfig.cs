using System.Collections.Generic;
using System.Linq;
using Extensions;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Buildings
{
    [CreateAssetMenu(fileName = "Buildings Service Config", menuName = "ScriptableObjects/Buildings/Buildings service config")]
    public class BuildingsServiceConfig : SerializedScriptableObject
    {
        [field: SerializeField] public string DataStorageKey { get; private set; }

        [field: SerializeField] public HashSet<BuildingConfig> BuildingsConfigs { get; private set; } = new();
        
#if UNITY_EDITOR
        [Button]
        public void AddAllItemsConfigs()
        {
            BuildingsConfigs = ExtensionMethods.GetAllScriptableObjects<BuildingConfig>().ToHashSet();
        }
        
        public static IEnumerable<string> BuildingsIds
        {
            get
            {
                var instance = ExtensionMethods.GetAllScriptableObjects<BuildingsServiceConfig>().First();

                return instance.BuildingsConfigs.Select(config => config.Id);
            }
        }
#endif
    }
}