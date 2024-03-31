using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace UI
{
    public class GameplayWindow : Window
    {
        [SerializeField, TabGroup("Components")] private Button _craftButton;
        
        private IUiService _uiService;

        [Inject]
        private void Construct(IUiService uiService)
        {
            _uiService = uiService;
        }

        private void OnCraftButtonClickedHandler()
        {
            _uiService.TryShowWindow<CraftWindow>(6);
        }

        private void OnEnable()
        {
            _craftButton.onClick.AddListener(OnCraftButtonClickedHandler);
        }

        private void OnDisable()
        {
            _craftButton.onClick.RemoveListener(OnCraftButtonClickedHandler);
        }
    }
}