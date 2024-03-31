﻿using System.Collections.Generic;
using System.Linq;
using Extensions;
using Inventory;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Buildings
{
    [CreateAssetMenu(fileName = "Buildings Service Config", menuName = "ScriptableObjects/Buildings/Buildings service config")]
    public class BuildingsServiceConfig : SerializedScriptableObject
    {
        [field: SerializeField] public string DataStorageKey { get; private set; }

        [field: SerializeField] public InventoryIdentifier PurchasingInventoryIdentifier { get; private set; }
        
        [field: SerializeField] public List<BuildingConfig> BuildingsConfigs { get; private set; } = new();
        
#if UNITY_EDITOR
        [Button]
        public void AddAllItemsConfigs()
        {
            BuildingsConfigs = ExtensionMethods.GetAllScriptableObjects<BuildingConfig>().ToList();
            UnityEditor.EditorUtility.SetDirty(this);
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