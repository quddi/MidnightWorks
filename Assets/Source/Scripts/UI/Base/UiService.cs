using System;
using System.Collections.Generic;
using Extensions;
using VContainer;
using Object = UnityEngine.Object;

namespace UI
{
    public class UiService : IUiService
    {
        private UiServiceConfig _uiServiceConfig;
        
        private WindowsContainers _windowsContainers;
        private Dictionary<Type, (Window window, int layer)> _activeWindows = new();
        private Dictionary<Type, Window> _inactiveWindows = new();

        [Inject]
        private void Construct(UiServiceConfig uiServiceConfig)
        {
            _uiServiceConfig = uiServiceConfig;
        }
        
        public T TryShowWindow<T>(int layer) where T : Window
        {
            var key = typeof(T);

            if (_activeWindows.TryGetValue(key, out (Window window, int layer) value))
            {
                if (value.layer != layer)
                    value.window.transform.parent = _windowsContainers[value.layer].Transform;
                
                return (T)value.window;
            }

            var window = _inactiveWindows.ContainsKey(key)
                ? _inactiveWindows.Snatch(key)
                : CreateWindow(key, _windowsContainers[layer]);
            
            window.gameObject.SetActive(true);
            
            _activeWindows[key] = (window, layer);

            return (T)window;
        }

        public bool TryHideWindow<T>() where T : Window
        {
            var key = typeof(T);

            if (!_activeWindows.ContainsKey(key))
                return false;

            var window = _activeWindows.Snatch(key).window;
            
            window.gameObject.SetActive(false);
            
            _inactiveWindows[key] = window;

            return true;
        }

        public void RegisterWindowsContainers(WindowsContainers windowsContainers)
        {
            _windowsContainers = windowsContainers;
        }
        
        private Window CreateWindow(Type type, WindowsContainer windowsContainer)
        {
            if (_uiServiceConfig.WindowsPrefabs.ContainsKey(type))
                throw new ArgumentException($"Prefab for window of type [{type}] not found!");

            var prefab = _uiServiceConfig.WindowsPrefabs[type];

            var window = Object.Instantiate(prefab, windowsContainer.Transform);

            return window;
        }
    }
}