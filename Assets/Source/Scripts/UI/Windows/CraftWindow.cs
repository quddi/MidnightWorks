using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace UI
{
    public class CraftWindow : Window
    {
        [SerializeField, TabGroup("Components")] private Button _closeButton;
        
        private IUiService _uiService;

        [Inject]
        private void Construct(IUiService uiService)
        {
            _uiService = uiService;
        }

        private void OnCloseButtonClickedHandler()
        {
            _uiService.TryHideWindow<CraftWindow>();
        }

        private void OnEnable()
        {
            _closeButton.onClick.AddListener(OnCloseButtonClickedHandler);
        }

        private void OnDisable()
        {
            _closeButton.onClick.RemoveListener(OnCloseButtonClickedHandler);
        }
    }
}