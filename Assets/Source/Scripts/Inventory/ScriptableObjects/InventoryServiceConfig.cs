using System.Collections.Generic;
using System.Linq;
using Extensions;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Inventory
{
    [CreateAssetMenu(fileName = "Inventory Service Config", menuName = "ScriptableObjects/Inventory/Inventory service config")]
    public class InventoryServiceConfig : SerializedScriptableObject
    {
        [field: SerializeField] public string DataStorageKey { get; private set; }
        
        [field: SerializeField] public Dictionary<InventoryIdentifier, IInventory> Inventories { get; private set; } = new();

        [field: SerializeField] public HashSet<ItemConfig> ItemsConfigs { get; private set; } = new();

#if UNITY_EDITOR
        [Button]
        public void AddAllItemsConfigs()
        {
            ItemsConfigs = ExtensionMethods.GetAllScriptableObjects<ItemConfig>().ToHashSet();
        }
        
        public static IEnumerable<string> ItemsIds
        {
            get
            {
                var instance = ExtensionMethods.GetAllScriptableObjects<InventoryServiceConfig>().First();

                return instance.ItemsConfigs.Select(config => config.Id);
            }
        }
#endif
    }
}