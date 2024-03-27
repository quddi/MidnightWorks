using Sirenix.OdinInspector;
using UnityEngine;

namespace Inventory
{
    public struct ItemParameters
    {
#if UNITY_EDITOR
        [field: ValueDropdown("@InventoryServiceConfig.ItemsIds")]
#endif
        [field: SerializeField] public string Id { get; set; }
        
        [field: SerializeField] public long Count { get; set; }
    }
}