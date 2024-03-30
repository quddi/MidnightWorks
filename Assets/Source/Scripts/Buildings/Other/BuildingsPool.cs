using System;
using System.Collections.Generic;
using System.Linq;
using Extensions;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Buildings
{
    public class BuildingsPool : MonoBehaviour
    {
        [SerializeField, TabGroup("Components")] private List<Building> _buildings = new();

        public Building GetRandomBuilding()
        {
            return _buildings.Any() ? _buildings.SnatchRandom() : null;
        }

        public void ReleaseBuilding(Building building)
        {
            if (_buildings.Contains(building))
                throw new ArgumentException($"Trying to release [{building}] building, but it is already in the pool!");
            
            _buildings.Add(building);
        }
    }
}