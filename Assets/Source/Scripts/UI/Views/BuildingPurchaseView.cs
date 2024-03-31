using System.Collections.Generic;
using System.Text;
using Buildings;
using Cysharp.Threading.Tasks;
using Inventory;
using Sirenix.OdinInspector;
using Sirenix.Utilities.Editor;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using VContainer;

namespace UI
{
    public class BuildingPurchaseView : MonoBehaviour
    {
        [SerializeField, TabGroup("Components")] private Button _puchaseButton;
        [SerializeField, TabGroup("Components")] private TMP_Text _purchaseButtonText;
        [SerializeField, TabGroup("Components")] private TMP_Text _titleText;

        [SerializeField, TabGroup("Parameters")] private InventoryIdentifier _inventoryIdentifier;
        [SerializeField, TabGroup("Parameters")] private Color _availableColor;
        [SerializeField, TabGroup("Parameters")] private Color _unAvailableColor;
        
        private IBuildingsService _buildingsService;
        private IInventoryService _inventoryService;

        private BuildingConfig _buildingConfig;
        private bool _subscribed;
        private bool _constructed;
        private string _buildingId;

        [Inject]
        private void Construct(IBuildingsService buildingsService, IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
            _buildingsService = buildingsService;

            _constructed = true;
            
            Subscribe();
        }

        public async UniTaskVoid SetBuildingId(string buildingId)
        {
            await UniTask.WaitUntil(() => _constructed);
            
            _buildingId = buildingId;
            _buildingConfig = _buildingsService.GetBuildingConfig(buildingId);
            
            UpdateButtonState();
            UpdateTitleState();
        }

        private void UpdateTitleState()
        {
            if (_buildingConfig == null) 
                return;

            _titleText.text = _buildingConfig.Name;
        }

        private void UpdateButtonState()
        {
            if (_buildingConfig == null) 
                return;
            
            var buildPrice = _buildingConfig.BuildPrice;
            var canPurchase = _inventoryService.ContainItems(_inventoryIdentifier, buildPrice);

            _purchaseButtonText.text = PriceToString(buildPrice);
            _purchaseButtonText.color = canPurchase ? _availableColor : _unAvailableColor;
        }

        private string PriceToString(List<ItemParameters> price)
        {
            var stringBuilder = new StringBuilder();

            foreach (var itemParameters in price)
            {
                stringBuilder
                    .Append(GetItemSpriteInText(itemParameters.Id))
                    .Append(itemParameters.Count)
                    .Append(' ');
            }

            return stringBuilder.ToString();

            string GetItemSpriteInText(string itemId)
            {
                var itemConfig = _inventoryService.GetItemConfig(itemId);
                return itemConfig.IconInText;
            }
        }

        private void OnSomeInventorItemsChangedHandler(InventoryIdentifier inventoryIdentifier, ItemParameters parameters)
        {
            if (_inventoryIdentifier.Id != inventoryIdentifier.Id) 
                return;
            
            UpdateButtonState();
        }

        private void Subscribe()
        {
            if (_subscribed || !_constructed) return;

            _subscribed = true;
            
            _inventoryService.OnItemAdded += OnSomeInventorItemsChangedHandler;
            _inventoryService.OnItemRemoved += OnSomeInventorItemsChangedHandler;
        }

        private void Unsubscribe()
        {
            if (!_subscribed) return;

            _subscribed = false;
            
            _inventoryService.OnItemAdded -= OnSomeInventorItemsChangedHandler;
            _inventoryService.OnItemRemoved -= OnSomeInventorItemsChangedHandler;
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