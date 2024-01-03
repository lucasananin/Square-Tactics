using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Events;

namespace Utilities.SimpleTypes
{
    [CreateAssetMenu(fileName = "Bool_", menuName = "SO/Simple Types/Bool")]
    public class BoolSO : ScriptableObject
    {
        [Title("// Current Value")]
        [SerializeField] protected bool _value = default;

        public Delegates.Action OnValueChange = null;

        public bool Value { get => GetCurrentValue(); set => SetCurrentValue(value); }

        private void OnValidate()
        {
            SetCurrentValue(_value);
        }

        protected void SetCurrentValue(bool _value)
        {
            this._value = _value;
            InvokeEvent();
        }

        protected bool GetCurrentValue()
        {
            return _value;
        }

        public void InvokeEvent()
        {
            OnValueChange?.Invoke();
        }
    }
}