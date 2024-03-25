using Extensions;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

namespace Test
{
    public class Test : MonoBehaviour
    {
#if UNITY_EDITOR
        [ValueDropdown("@InventoryServiceConfig.ItemsIds")]
#endif
        [SerializeField, TabGroup("Parameters")] private string _itemId;
        
        [Button]
        public void Test1()
        {
            EditorGet.BuiltScenesNames.ForEach(Debug.Log);
        }
    }
}