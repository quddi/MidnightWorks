#if UNITY_EDITOR
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Tools
{
    public class BuildingsTool : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _buildings = new();
        [SerializeField] private List<GameObject> _buildingsModels = new();
        [SerializeField] private List<GameObject> _buildingsObstacles = new();

        [Button, HorizontalGroup("Buildings")]
        private void EnableAllBuildings() => _buildings.ForEach(obj => obj.SetActive(true));
        
        [Button, HorizontalGroup("Buildings")]
        private void DisableAllBuildings() => _buildings.ForEach(obj => obj.SetActive(false));
        
        [Button, HorizontalGroup("Buildings models")]
        private void EnableAllBuildingsModels() => _buildingsModels.ForEach(obj => obj.SetActive(true));
        
        [Button, HorizontalGroup("Buildings models")]
        private void DisableAllBuildingsModels() => _buildingsModels.ForEach(obj => obj.SetActive(false));
        
        [Button, HorizontalGroup("Buildings obstacles")]
        private void EnableAllBuildingsObstacles() => _buildingsObstacles.ForEach(obj => obj.SetActive(true));
        
        [Button, HorizontalGroup("Buildings obstacles")]
        private void DisableAllBuildingsObstacles() => _buildingsObstacles.ForEach(obj => obj.SetActive(false));
    }
}
#endif
