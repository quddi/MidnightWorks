using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Extensions
{
    public static class ExtensionMethods
    {
        public static Vector3 GetRandomPoint(this Bounds bounds) 
        {
            var minX = bounds.size.x * 0.5f;
            var minY = bounds.size.y * 0.5f;
            var minZ = bounds.size.z * 0.5f;

            return new Vector3(Random.Range(minX, -minX), Random.Range(minY, -minY), Random.Range(minZ, -minZ));
        }
        
        public static Vector3 GetRandomPointInBounds(this BoxCollider boundsCollider)
        {
            var position = boundsCollider.transform.position;

            return position + boundsCollider.bounds.GetRandomPoint();
        }

        public static float RandomFromInterval(this (float Min, float Max) interval)
        {
            return Random.Range(interval.Min, interval.Max);
        }
        
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
