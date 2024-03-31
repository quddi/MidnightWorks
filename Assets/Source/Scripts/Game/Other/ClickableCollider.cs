using System;
using UnityEngine;

namespace Game
{
    public class ClickableCollider : MonoBehaviour
    {
        public event Action OnClicked;

        private void OnMouseUp()
        {
            OnClicked?.Invoke();
        }
    }
}