using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public class SafeAreaSetter : MonoBehaviour
    {
        [Title("// General")]
        [SerializeField] Canvas _canvas = null;

        private RectTransform _safeAreaPanel = null;
        private Rect _currentSafeArea = new Rect();
        private ScreenOrientation _currentOrientation = ScreenOrientation.AutoRotation;

        private void Start()
        {
            if (_safeAreaPanel == null)
                _safeAreaPanel = GetComponent<RectTransform>();

            _currentOrientation = Screen.orientation;
            _currentSafeArea = Screen.safeArea;

            ApplySafeArea();
        }

#if UNITY_EDITOR
        private void Update()
        {
            if (_currentOrientation != Screen.orientation || _currentSafeArea != Screen.safeArea)
            {
                ApplySafeArea();
            }
        }
#endif

        public void ApplySafeArea()
        {
            if (_safeAreaPanel == null)
                _safeAreaPanel = GetComponent<RectTransform>();

            Rect _safeArea = Screen.safeArea;

            Vector2 _anchorMin = _safeArea.position;
            Vector2 _anchorMax = _safeArea.position + _safeArea.size;

            _anchorMin.x /= _canvas.pixelRect.width;
            _anchorMin.y /= _canvas.pixelRect.height;
            _anchorMax.x /= _canvas.pixelRect.width;
            _anchorMax.y /= _canvas.pixelRect.height;

            _safeAreaPanel.anchorMin = _anchorMin;
            _safeAreaPanel.anchorMax = _anchorMax;

            _currentOrientation = Screen.orientation;
            _currentSafeArea = Screen.safeArea;
        }
    }
}
