using System;
using System.Collections.Generic;
using System.IO;
using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace DataStorage
{
    public class DataStorageService : IDataStorageService
    {
        private const string FileExtension = ".save";
        
        private readonly TimeSpan _lazySavingDelay = TimeSpan.FromSeconds(1);
        
        private readonly Dictionary<string, string> _lazySavesQueue = new();

        public bool Contains(string key)
        {
            return File.Exists(Path.Combine(Application.persistentDataPath, key + FileExtension));
        }

        public async UniTaskVoid SaveLazily(string key, string data)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentException("Received key is null or empty");

            if (!_lazySavesQueue.TryAdd(key, data))
            {
                _lazySavesQueue[key] = data;
                return;
            }

            await UniTask.Delay(_lazySavingDelay);

            if (!_lazySavesQueue.ContainsKey(key))
                return;

            await SaveImmediately(key, data);
        }

        public UniTask SaveImmediately(string key, string data)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentException("Received key is null or empty");

            if (_lazySavesQueue.ContainsKey(key)) 
                _lazySavesQueue.Remove(key);

            var filePath = Path.Combine(Application.persistentDataPath, key + FileExtension);

            return File.WriteAllTextAsync(filePath, data).AsUniTask();
        }

        public UniTask<string> Load(string key)
        {
            var filePath = Path.Combine(Application.persistentDataPath, key + FileExtension);

            if (!File.Exists(filePath))
                throw new ArgumentException($"Provided key [{key}] does not exist in database");


            var result = File.ReadAllText(filePath);

            return UniTask.FromResult(result);
        }
        
#if UNITY_EDITOR
        [MenuItem("Data Storage/Open Storage %&o")]
        public static void OpenStorage()
        {
            string path = Application.persistentDataPath;
            
            path = path.Replace(@"/", @"\");
            
            System.Diagnostics.Process.Start("explorer.exe", "/select," + path);
        }

        [MenuItem("Data Storage/Clear Storage %&d")]
        public static void ClearStorage()
        {
            foreach (var file in Directory.GetFiles(Application.persistentDataPath, $"*{FileExtension}",
                         SearchOption.AllDirectories)) File.Delete(file);
            
            Debug.Log($"Cleared storage: {Application.persistentDataPath}");
        }
#endif
    }
}