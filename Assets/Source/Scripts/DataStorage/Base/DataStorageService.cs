using System;
using System.Collections.Generic;
using System.IO;
using Cysharp.Threading.Tasks;
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

        public async UniTaskVoid SaveLazily(string data, string key)
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

            await SaveImmediately(data, key);

            _lazySavesQueue.Remove(key);
        }

        public async UniTask SaveImmediately(string data, string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentException("Received key is null or empty");

            if (!_lazySavesQueue.TryAdd(key, data))
            {
                _lazySavesQueue[key] = data;
                return;
            }

            await UniTask.Delay(TimeSpan.FromSeconds(1));

            var filePath = Path.Combine(Application.persistentDataPath, key + FileExtension);

            await File.WriteAllTextAsync(filePath, _lazySavesQueue[key]);

            _lazySavesQueue.Remove(key);
        }

        public UniTask<string> Load(string key)
        {
            var filePath = Path.Combine(Application.persistentDataPath, key + FileExtension);

            if (!File.Exists(filePath))
                throw new ArgumentException($"Provided key [{key}] does not exist in database");


            var result = File.ReadAllText(filePath);

            return UniTask.FromResult(result);
        }
    }
}