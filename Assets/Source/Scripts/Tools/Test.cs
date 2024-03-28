using Sirenix.OdinInspector;
using UI;
using VContainer;

namespace Tools
{
    public class Test : SerializedMonoBehaviour
    {
        private IUiService _uiService;

        [Inject]
        private void Construct(IUiService uiService)
        {
            _uiService = uiService;
        }

        [Button]
        private bool ShowGameplayWindow(int layer)
        {
            return _uiService.TryShowWindow<GameplayWindow>(layer) == null;
        }
        
        [Button]
        private bool HideGameplayWindow()
        {
            return _uiService.TryHideWindow<GameplayWindow>();
        }
    }
}