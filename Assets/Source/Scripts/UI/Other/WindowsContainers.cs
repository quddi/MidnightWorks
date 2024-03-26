using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace UI
{
    public class WindowsContainers : MonoBehaviour
    {
        private readonly List<WindowsContainer> _windowsContainers = new();

        public WindowsContainer this[int layer] => _windowsContainers[layer];

        [Inject]
        private void Construct(UiService uiService)
        {
            uiService.RegisterWindowsContainers(this);
        }
        
        private void Awake()
        {
            foreach (Transform child in transform)
            {
                var windowContainer = child.GetComponent<WindowsContainer>();

                if (windowContainer == null)
                    throw new InvalidOperationException($"Found a child without windows container component: [{child}]!");
                
                _windowsContainers.Add(windowContainer);
            }
        }
    }
}