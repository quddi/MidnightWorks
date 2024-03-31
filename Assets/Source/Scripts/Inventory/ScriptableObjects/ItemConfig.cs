using Sirenix.OdinInspector;
using UnityEngine;

namespace Inventory
{
    [CreateAssetMenu(fileName = "Item Config", menuName = "ScriptableObjects/Inventory/Item config")]
    public class ItemConfig : SerializedScriptableObject
    {
        [field: SerializeField] public string Id { get; }
        
        [field: SerializeField] public string IconInText { get; }
        [field: SerializeField] public Sprite Icon { get; }
        
        public override int GetHashCode()
        {
            return Id == null ? default : Id.GetHashCode();
        }

        public override bool Equals(object other)
        {
            return other is ItemConfig itemConfig && itemConfig.Id == Id;
        }
    }
}