using Camera;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

namespace Implementations.Gameplay
{
    public class CameraLooker : MonoBehaviour
    {
        [SerializeField, TabGroup("Components")] private Transform _transform;
        [SerializeField, TabGroup("Components")] private Canvas _canvas;

        [ShowInInspector, ReadOnly] private Transform _cameraTransform;
        
        private ICameraService _cameraService;

        [Inject]
        private void Construct(ICameraService cameraService)
        {
            _cameraService = cameraService;
            
            UpdateCamera();

            _cameraService.OnCameraChanged += OnCameraChangedHandler;
        }

        private void UpdateCamera()
        {
            if (_cameraService.Camera == null) 
                return;
            
            _cameraTransform = _cameraService.Camera.transform;

            if (_canvas != null)
                _canvas.worldCamera = _cameraService.Camera;
        }
        
        private void OnCameraChangedHandler()
        {
            UpdateCamera();
        }
        
        protected void LateUpdate()
        {
            if (_cameraTransform != null) 
                _transform.LookAt(_transform.position + _cameraTransform.forward);
        }

        private void OnDestroy()
        {
            _cameraService.OnCameraChanged -= OnCameraChangedHandler;
        }
    }
}