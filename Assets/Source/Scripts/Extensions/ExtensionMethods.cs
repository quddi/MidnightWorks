using System.Collections.Generic;
using UnityEngine;

namespace Extensions
{
    public static class ExtensionMethods
    {
#if UNITY_EDITOR
        public static IEnumerable<T> GetAllScriptableObjects<T>() where T : ScriptableObject
        {
            string[] guids = UnityEditor.AssetDatabase.FindAssets("t:" + typeof(T).Name);
            
            foreach (var guid in guids)
            {
                string path = UnityEditor.AssetDatabase.GUIDToAssetPath(guid);
                
                yield return UnityEditor.AssetDatabase.LoadAssetAtPath<T>(path);
            }
        }
#endif
    }
}
