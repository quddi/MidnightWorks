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

        [SerializeField, TabGroup("Components")] private Image _itemIcon;
        [SerializeField, TabGroup("Components")] private TMP_Text _countText;
        
        private IInventoryService _inventoryService;

        private bool _subscribed;
        private bool _constructed;

        [Inject]
        private void Construct(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;

            _constructed = true;
            
            Subscribe();
        }

        private void UpdateIcon()
        {
            _itemIcon.sprite = _inventoryService.GetItemConfig(_itemId).Icon;
        }

        private void UpdateCount()
        {
            _countText.text = _inventoryService.Get
        }
        
        private void Subscribe()
        {
            if (_subscribed || !_constructed) return;

            _subscribed = true;
        }

        private void Unsubscribe()
        {
            if (!_subscribed) return;

            _subscribed = false;
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