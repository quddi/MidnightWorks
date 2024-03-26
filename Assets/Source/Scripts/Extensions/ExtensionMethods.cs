using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Extensions
{
    public static class ExtensionMethods
    {
        public static T SnatchFirst<T>(this HashSet<T> hashSet)
        {
            var element = hashSet.First();

            hashSet.Remove(element);

            return element;
        }

        public static T2 Snatch<T1, T2>(this Dictionary<T1, T2> dictionary, T1 key)
        {
            var value = dictionary[key];

            dictionary.Remove(key);
            
            return value;
        }
        
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
