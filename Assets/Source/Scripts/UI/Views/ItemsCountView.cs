using Inventory;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace UI
{
    public class ItemsCountView : MonoBehaviour
    {
#if UNITY_EDITOR
        [ValueDropdown("@InventoryServiceConfig.ItemsIds")]
#endif
        [SerializeField, TabGroup("Parameters")] private string _itemId;
        [SerializeField, TabGroup("Parameters")] private InventoryIdentifier _inventoryIdentifier;
        
        [SerializeField, TabGroup("Components")] private Image _itemIcon;
        [SerializeField, TabGroup("Components")] private TMP_Text _countText;
        [SerializeField, TabGroup("Components")] private ItemAdditionView _itemAdditionView;
        
        private IInventoryService _inventoryService;
        
        private bool _subscribed;
        private bool _constructed;

        [Inject]
        private void Construct(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;

            _constructed = true;

            Initialize();
        }

        private void Initialize()
        {
            UpdateIcon();
            UpdateCount();
            
            Subscribe();
        }

        private void UpdateIcon()
        {
            _itemIcon.sprite = _inventoryService.GetItemConfig(_itemId).Icon;
        }

        private void UpdateCount()
        {
            _countText.text = _inventoryService.GetItemsCount(_inventoryIdentifier, _itemId).ToString();
        }

        private void OnSomeInventoryItemsAddedHandler(InventoryIdentifier inventoryIdentifier, ItemParameters itemParameters)
        {
            if (_inventoryIdentifier.Id != inventoryIdentifier.Id) 
                return;

            if (itemParameters.Id == _itemId)
            {
                UpdateCount();
                _itemAdditionView.ExecuteAnimation(itemParameters.Count).Forget();
            }
        }
        
        private void OnSomeInventoryItemsRemovedHandler(InventoryIdentifier inventoryIdentifier, ItemParameters itemParameters)
        {
            if (_inventoryIdentifier.Id != inventoryIdentifier.Id) 
                return;

            if (itemParameters.Id == _itemId)
                UpdateCount();
        }

        private void Subscribe()
        {
            if (_subscribed || !_constructed) return;

            _subscribed = true;
            
            _inventoryService.OnItemAdded += OnSomeInventoryItemsAddedHandler;
            _inventoryService.OnItemRemoved += OnSomeInventoryItemsRemovedHandler;
        }

        private void Unsubscribe()
        {
            if (!_subscribed) return;

            _subscribed = false;
            
            _inventoryService.OnItemAdded -= OnSomeInventoryItemsAddedHandler;
            _inventoryService.OnItemRemoved -= OnSomeInventoryItemsRemovedHandler;
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