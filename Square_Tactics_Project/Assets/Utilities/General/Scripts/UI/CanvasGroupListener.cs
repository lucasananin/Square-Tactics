using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Events;

namespace Utilities
{
    public class CanvasGroupListener : MonoBehaviour
    {
        [Title("// General")]
        [SerializeField] CanvasGroup _canvasGroup = null;
        [SerializeField] CanvasGroupData _enabledData = null;
        [SerializeField] CanvasGroupData _disabledData = null;

        [Title("// Events - Subscribe")]
        [SerializeField] ActionSO[] _enableEvents = null;
        [SerializeField] ActionSO[] _disableEvents = null;

        private void OnEnable()
        {
            int _count = _enableEvents.Length;

            for (int i = 0; i < _count; i++)
            {
                _enableEvents[i].Subscribe(EnableCanvasGroup);
            }

            _count = _disableEvents.Length;

            for (int i = 0; i < _count; i++)
            {
                _disableEvents[i].Subscribe(DisableCanvasGroup);
            }
        }

        private void OnDisable()
        {
            int _count = _enableEvents.Length;

            for (int i = 0; i < _count; i++)
            {
                _enableEvents[i].Unsubscribe(EnableCanvasGroup);
            }

            _count = _disableEvents.Length;

            for (int i = 0; i < _count; i++)
            {
                _disableEvents[i].Unsubscribe(DisableCanvasGroup);
            }
        }

        [Button]
        private void EnableCanvasGroup()
        {
            _canvasGroup.alpha = _enabledData.alpha;
            _canvasGroup.interactable = _enabledData.interactable;
            _canvasGroup.blocksRaycasts = _enabledData.blocksRaycasts;
        }

        [Button]
        private void DisableCanvasGroup()
        {
            _canvasGroup.alpha = _disabledData.alpha;
            _canvasGroup.interactable = _disabledData.interactable;
            _canvasGroup.blocksRaycasts = _disabledData.blocksRaycasts;
        }

        [System.Serializable]
        private class CanvasGroupData
        {
            public int alpha = 1;
            public bool interactable = true;
            public bool blocksRaycasts = true;
        }
    }
}
