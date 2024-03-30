using Sirenix.OdinInspector;
using UnityEngine;

namespace Buildings
{
    public class Building : MonoBehaviour
    {
#if UNITY_EDITOR
        [field: ValueDropdown("@BuildingsServiceConfig.BuildingsIds")]
#endif
        [field: SerializeField, TabGroup("Parameters")] public string Id { get; private set; }
        
        [field: SerializeField, TabGroup("Components")] public Transform StayPoint { get; private set; }
    }
}