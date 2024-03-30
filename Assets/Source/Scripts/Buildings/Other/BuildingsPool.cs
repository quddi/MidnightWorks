using System;
using System.Collections.Generic;
using System.Linq;
using Extensions;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Buildings
{
    public class BuildingsPool : MonoBehaviour
    {
        [SerializeField, TabGroup("Components")] private List<Building> _freeBuildings = new();

        public bool ContainsFreeBuildings => _freeBuildings.Any();

        public Building GetRandomBuilding()
        {
            if (!ContainsFreeBuildings)
                throw new InvalidOperationException("Trying to take a free building when there is no one!");

            return _freeBuildings.SnatchRandom();
        }

        public void ReleaseBuilding(Building building)
        {
            if (_freeBuildings.Contains(building))
                throw new ArgumentException($"Trying to release [{building}] building, but it is already in the pool!");
            
            _freeBuildings.Add(building);
        }
    }
}