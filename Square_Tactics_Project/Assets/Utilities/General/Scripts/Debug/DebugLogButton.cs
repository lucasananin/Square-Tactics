using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Utilities
{
    public class DebugLogButton : MonoBehaviour
    {
        private Button _button = null;

        private void OnValidate()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(PrintName);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveAllListeners();
        }

        private void PrintName()
        {
            Debug.Log($"{_button.name} Clicked!");
        }
    }
}
