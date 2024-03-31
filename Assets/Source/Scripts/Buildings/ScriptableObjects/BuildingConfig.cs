using System.Collections.Generic;
using Inventory;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Buildings
{
    [CreateAssetMenu(fileName = "Building Config", menuName = "ScriptableObjects/Buildings/Building config")]
    public class BuildingConfig : SerializedScriptableObject
    {
        [field: SerializeField] public string Id { get; private set; }

        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public List<ItemParameters> BuildPrice { get; private set; } = new();
        
        [field: SerializeField] public List<ItemParameters> ExecutionPrice { get; private set; } = new();
        
        [field: SerializeField] public List<ItemParameters> ExecutionReward { get; private set; } = new();

        public bool IsBoughtImmediately => BuildPrice.Count == 0;
    }
}