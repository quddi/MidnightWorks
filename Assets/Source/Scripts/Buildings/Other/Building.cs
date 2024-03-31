using System;
using Sirenix.OdinInspector;
using UI;
using UnityEngine;
using VContainer;

namespace Buildings
{
    public class Building : MonoBehaviour
    {
#if UNITY_EDITOR
        [field: ValueDropdown("@BuildingsServiceConfig.BuildingsIds")]
#endif
        [field: SerializeField, TabGroup("Parameters")] public string Id { get; private set; }
        
        [field: SerializeField, TabGroup("Components")] public Transform StayPoint { get; private set; }

        [SerializeField, TabGroup("Components")] private BuildingPurchaseView _buildingPurchaseView;
        
        private void Start()
        {
            _buildingPurchaseView.SetBuildingId(Id);
        }
    }
}