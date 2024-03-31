using System.Collections.Generic;
using System.Transactions;
using Inventory;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace UI
{
    public class CraftView : MonoBehaviour
    {
#if UNITY_EDITOR
        [ValueDropdown("@InventoryServiceConfig.ItemsIds")]
#endif
        [SerializeField, TabGroup("Parameters")] private string _itemId;
        [SerializeField, TabGroup("Parameters")] private InventoryIdentifier _inventoryIdentifier;
        [SerializeField, TabGroup("Parameters")] private Color _availableColor;
        [SerializeField, TabGroup("Parameters")] private Color _unAvailableColor;
        
        [SerializeField, TabGroup("Components")] private Image _craftItemIcon;
        [SerializeField, TabGroup("Components")] private Image _firstCraftItemIcon;
        [SerializeField, TabGroup("Components")] private Image _secondCraftItemIcon;
        [SerializeField, TabGroup("Components")] private TMP_Text _craftItemText;
        [SerializeField, TabGroup("Components")] private TMP_Text _firstCraftItemText;
        [SerializeField, TabGroup("Components")] private TMP_Text _secondCraftItemText;
        [SerializeField, TabGroup("Components")] private Button _craftButton;
        
        private IInventoryService _inventoryService;

        private ItemConfig _itemConfig;
        private ItemConfig _firstCraftItemConfig;
        private ItemConfig _secondCraftItemConfig;
        private ItemParameters _craftedParameters;
        private ItemParameters _firstCraftParameters;
        private ItemParameters _secondCraftParameters;
        private bool _subscribed;
        private bool _constructed;

        [Inject]
        private void Construct(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;

            _constructed = true;
            
            SetUpConfigs();
            SetUpParameters();

            UpdateCraftIconsState();
            UpdateCraftTextsCount();
            
            Subscribe();
        }

        private void SetUpConfigs()
        {
            _itemConfig = _inventoryService.GetItemConfig(_itemId);
            _firstCraftItemConfig = _inventoryService.GetItemConfig(_itemConfig.FirstCraftItemId);
            _secondCraftItemConfig = _inventoryService.GetItemConfig(_itemConfig.SecondCraftItemId);
        }

        private void SetUpParameters()
        {
            _firstCraftParameters = new() { Id = _itemConfig.FirstCraftItemId, Count = _inventoryService.CraftItemCount };
            _secondCraftParameters = new() { Id = _itemConfig.SecondCraftItemId, Count = _inventoryService.CraftItemCount };
            _craftedParameters = new() { Id = _itemConfig.Id, Count = _inventoryService.CraftItemCount };
        }

        private void UpdateCraftIconsState()
        {
            _craftItemIcon.sprite = _itemConfig.Icon;
            _firstCraftItemIcon.sprite = _firstCraftItemConfig.Icon;
            _secondCraftItemIcon.sprite = _secondCraftItemConfig.Icon;
        }

        private void UpdateCraftTextsCount()
        {
            _craftItemText.text = $"x{_inventoryService.CraftItemCount}";
            _firstCraftItemText.text = $"x{_inventoryService.CraftItemCount}";
            _secondCraftItemText.text = $"x{_inventoryService.CraftItemCount}";
        }

        private void UpdateCraftTextsColor()
        {
            _firstCraftItemText.color = _inventoryService.ContainItem(_inventoryIdentifier, _firstCraftParameters)
                ? _availableColor : _unAvailableColor;
            
            _secondCraftItemText.color = _inventoryService.ContainItem(_inventoryIdentifier, _secondCraftParameters)
                ? _availableColor : _unAvailableColor;
        }

        private void OnSomeInventoryItemsChangedHandler(InventoryIdentifier inventoryIdentifier, ItemParameters _)
        {
            if (inventoryIdentifier.Id != _inventoryIdentifier.Id)
                return;
            
            UpdateCraftTextsColor();
        }

        private void OnCraftButtonClickedHandler()
        {
            if (!_inventoryService.ContainItem(_inventoryIdentifier, _firstCraftParameters))
                return;
            
            if (!_inventoryService.ContainItem(_inventoryIdentifier, _secondCraftParameters))
                return;

            var firstRemoveResult = _inventoryService.TryRemoveItem(_inventoryIdentifier, _firstCraftParameters);
            var secondRemoveResult = _inventoryService.TryRemoveItem(_inventoryIdentifier, _firstCraftParameters);

            if (firstRemoveResult == false || secondRemoveResult == false)
                throw new TransactionException($"Could not remove the [{_firstCraftParameters}] or [{_secondCraftParameters}]");
            
            if (!_inventoryService.TryAddItem(_inventoryIdentifier, _craftedParameters))
                throw new TransactionException($"Could not add crafted item [{_craftedParameters}]");
        }

        private void Subscribe()
        {
            if (_subscribed || !_constructed) return;

            _subscribed = true;
            
            _inventoryService.OnItemAdded += OnSomeInventoryItemsChangedHandler;
            _inventoryService.OnItemRemoved += OnSomeInventoryItemsChangedHandler;
            _craftButton.onClick.AddListener(OnCraftButtonClickedHandler);
        }

        private void Unsubscribe()
        {
            if (!_subscribed) return;

            _subscribed = false;
            
            _inventoryService.OnItemAdded -= OnSomeInventoryItemsChangedHandler;
            _inventoryService.OnItemRemoved -= OnSomeInventoryItemsChangedHandler;
            _craftButton.onClick.RemoveListener(OnCraftButtonClickedHandler);
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