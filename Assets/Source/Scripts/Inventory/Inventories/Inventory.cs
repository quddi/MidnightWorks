using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Inventory
{
    public class Inventory : IInventory
    {
        [SerializeField, ReadOnly] private Dictionary<string, long> _items = new();
        
        public void AddItem(string itemId, long itemsCount)
        {
            if (!_items.TryAdd(itemId, itemsCount)) 
                _items[itemId] += itemsCount;
        }

        public bool TryRemoveItem(string itemId, long itemsCount)
        {
            if (!ContainItem(itemId, itemsCount))
                return false;

            _items[itemId] -= itemsCount;

            if (_items[itemId] == 0)
                _items.Remove(itemId);

            return true;
        }

        public bool ContainItem(string itemId, long itemCount)
        {
            return _items.ContainsKey(itemId) && _items[itemId] >= itemCount;
        }

        public bool ContainItem(string itemId)
        {
            return ContainItem(itemId, 0);
        }
    }
}