using System.Collections.Generic;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Inventory
{
    [SerializeField]
    public class StackableInventory : IInventory
    {
        [SerializeField, ReadOnly, JsonProperty] 
        private Dictionary<string, long> _items = new();

        [JsonIgnore]
        private IReadOnlyDictionary<string, long> Items => _items;

        public long GetItemsCount(string itemId)
        {
            return Items.GetValueOrDefault(itemId, 0);
        }

        public void AddItem(ItemParameters itemParameters)
        {
            if (!_items.TryAdd(itemParameters.Id, itemParameters.Count)) 
                _items[itemParameters.Id] += itemParameters.Count;
        }

        public bool TryRemoveItem(ItemParameters itemParameters)
        {
            if (!ContainItem(itemParameters))
                return false;

            _items[itemParameters.Id] -= itemParameters.Count;

            if (_items[itemParameters.Id] == 0)
                _items.Remove(itemParameters.Id);

            return true;
        }

        public bool ContainItem(ItemParameters itemParameters)
        {
            return _items.ContainsKey(itemParameters.Id) && _items[itemParameters.Id] >= itemParameters.Count;
        }

        public bool ContainItem(string itemId)
        {
            return ContainItem(new ItemParameters { Id = itemId, Count = 0 });
        }
    }
}