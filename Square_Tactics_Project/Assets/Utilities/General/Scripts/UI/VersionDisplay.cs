using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Utilities
{
    public class VersionDisplay : MonoBehaviour
    {
        [Title("// UI - Texts")]
        [SerializeField] TextMeshProUGUI _text = null;

        private void Start()
        {
            _text.SetText($"Version: {Application.version}");
        }
    }
}
