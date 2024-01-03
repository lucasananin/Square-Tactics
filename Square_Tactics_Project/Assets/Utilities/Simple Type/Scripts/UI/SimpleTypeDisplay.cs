using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Utilities.SimpleTypes
{
    public abstract class SimpleTypeDisplay<T, U> : MonoBehaviour where T : AbstractSimpleType<U>
    {
        [Title("// General")]
        [SerializeField] protected T _simpleTypeSO = default;

        [Title("// UI - Texts")]
        [SerializeField] protected TextMeshProUGUI _text = null;

        private IEnumerator Start()
        {
            yield return null;
            UpdateVisuals();
        }

        private void OnEnable()
        {
            _simpleTypeSO.OnValueChange += UpdateVisuals;
        }

        private void OnDisable()
        {
            _simpleTypeSO.OnValueChange -= UpdateVisuals;
        }

        public virtual void UpdateVisuals()
        {
            _text.SetText($"{_simpleTypeSO.CurrentValue}");
        }
    }
}
