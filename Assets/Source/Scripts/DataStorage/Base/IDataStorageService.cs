using Cysharp.Threading.Tasks;

namespace DataStorage
{
    public interface IDataStorageService
    {
        public bool Contains(string key);
        
        public UniTaskVoid SaveLazily(string data, string key);
        
        public UniTask SaveImmediately(string data, string key);

        public UniTask<string> Load(string key);
    }
}