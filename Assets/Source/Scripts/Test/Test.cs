using System.Collections.Generic;
using Extensions;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;
using VContainer.Unity;

namespace Test
{
    public class Test : SerializedMonoBehaviour
    {
#if UNITY_EDITOR
        [ValueDropdown("@InventoryServiceConfig.ItemsIds")]
#endif
        [SerializeField, TabGroup("Parameters")] private string _itemId;

        [SerializeField, TabGroup("Parameters")] private List<IInstaller> _list;
        
        [Button]
        public void Test1()
        {
            EditorGet.BuiltScenesNames.ForEach(Debug.Log);
        }
    }
}