﻿using System;
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

        public long GetItemsCount(string itemId)
        {
            return _items.Count(inventoryItemId => inventoryItemId == itemId);
        }

        public void AddItem(ItemParameters itemParameters)
        {
            for (int i = 0; i < itemParameters.Count; i++)
            {
                _items.Add(itemParameters.Id);
            }
        }

        public bool TryRemoveItem(ItemParameters itemParameters)
        {
            if (!ContainItem(itemParameters))
                return false;

            for (int i = 0; i < itemParameters.Count; i++)
            {
                _items.Remove(itemParameters.Id);
            }

            return true;
        }

        public bool ContainItem(ItemParameters itemParameters)
        {
            var containedItemsCount = _items.Count(inventoryItemId => inventoryItemId == itemParameters.Id);

            return containedItemsCount >= itemParameters.Count;
        }

        public IInventory GetCopy()
        {
            return new UnStackableInventory
            {
                _items = new(Items)
            };
        }

        public bool ContainItem(string itemId)
        {
            return ContainItem(new ItemParameters { Id = itemId, Count = 1});
        }
    }
}