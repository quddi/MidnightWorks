using System;
using Cysharp.Threading.Tasks;
using Game;
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
        [SerializeField, TabGroup("Components")] private ClickableCollider _clickableCollider;
        [SerializeField, TabGroup("Components")] private GameObject _buildingModel;
        
        private IBuildingsService _buildingsService;
        private IUiService _uiService;

        private bool _subscribed;
        private bool _constructed;

        [Inject]
        private void Construct(IBuildingsService buildingsService, IUiService uiService)
        {
            _uiService = uiService;
            _buildingsService = buildingsService;

            _constructed = true;
            
            Subscribe();
        }

        private void Start()
        {
            _buildingPurchaseView.SetBuildingId(Id).Forget();
            
            UpdateBuildingState();
        }

        private void UpdateBuildingState()
        {
            var isBuilt = _buildingsService.IsBuilt(Id);
            
            _clickableCollider.gameObject.SetActive(!isBuilt);
            _buildingModel.SetActive(isBuilt);
        }

        private void OnSomeBuildingPurchasedHandler(string _)
        {
            UpdateBuildingState();
        }

        private void OnColliderClickedHandler()
        {
            if (!_uiService.IsWindowActive<CraftWindow>())
                _buildingPurchaseView.gameObject.SetActive(true);
        }

        private void Subscribe()
        {
            if (_subscribed || !_constructed) return;

            _subscribed = true;
            
            _clickableCollider.OnClicked += OnColliderClickedHandler;
            _buildingsService.OnBuildingPurchased += OnSomeBuildingPurchasedHandler;
        }

        private void Unsubscribe()
        {
            if (!_subscribed) return;

            _subscribed = false;
            
            _clickableCollider.OnClicked -= OnColliderClickedHandler;
            _buildingsService.OnBuildingPurchased -= OnSomeBuildingPurchasedHandler;
        }

        private void OnEnable()
        {
            Subscribe();
        }

        private void OnDisable()
        {
            Unsubscribe();
        }
    }
}