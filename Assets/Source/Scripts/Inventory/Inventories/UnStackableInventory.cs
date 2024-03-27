using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Inventory
{
    [Serializable]
    public class UnStackableInventory : IInventory
    {
        [SerializeField, ReadOnly, JsonProperty] 
        private List<string> _items = new();

        [JsonIgnore]
        public IReadOnlyList<string> Items => _items;
        
        public void AddItem()
        {
            for (int i = 0; i < itemsCount; i++)
            {
                _items.Add(itemId);
            }
        }

        public bool TryRemoveItem(string itemId, long itemsCount)
        {
            if (!ContainItem(itemId, itemsCount))
                return false;

            for (int i = 0; i < itemsCount; i++)
            {
                _items.Remove(itemId);
            }

            return true;
        }

        public bool ContainItem(string itemId, long itemsCount)
        {
            var containedItemsCount = _items.Count(inventoryItemId => inventoryItemId == itemId);

            return containedItemsCount >= itemsCount;
        }

        public bool ContainItem(string itemId)
        {
            return ContainItem(itemId, 1);
        }
    }
}