using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace UI
{
    public class ItemAdditionView : MonoBehaviour
    {
        [SerializeField, TabGroup("Components")] private TMP_Text _countText;
        [SerializeField, TabGroup("Components")] private CanvasGroup _canvasGroup;

        [SuffixLabel("s")]
        [SerializeField, TabGroup("Parameters")] private float _animationDuration;
        [SerializeField, TabGroup("Parameters")] private AnimationCurve _alphaCurve;

        private long _currentAdditionValue;
        private bool _isExecuting;
        private float _currentTime;

        private void Start()
        {
            _canvasGroup.alpha = 0;
        }

        public async UniTaskVoid ExecuteAnimation(long additionItemsCount)
        {
            if (_isExecuting)
            {
                _currentAdditionValue += additionItemsCount;
                _countText.text = $"+{_currentAdditionValue}";
                return;
            }
            
            _isExecuting = true;
            _currentTime = 0;
            _currentAdditionValue = additionItemsCount;
            _countText.text = $"+{_currentAdditionValue}";

            while (_currentTime < _animationDuration)
            {
                _canvasGroup.alpha = _alphaCurve.Evaluate(_currentTime / _animationDuration);

                await UniTask.DelayFrame(1);

                _currentTime += Time.deltaTime;
            }

            _isExecuting = false;
        }
    }
}