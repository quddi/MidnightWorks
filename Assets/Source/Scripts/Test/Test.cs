using Extensions;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

namespace Test
{
    public class Test : MonoBehaviour
    {
        [Button]
        public void Test1()
        {
            EditorGet.BuiltScenesNames.ForEach(Debug.Log);
        }
    }
}