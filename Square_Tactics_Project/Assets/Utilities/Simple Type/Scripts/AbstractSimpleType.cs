using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Events;

namespace Utilities.SimpleTypes
{
    public abstract class AbstractSimpleType<T> : ScriptableObject
    {
        [Title("// Current")]
        [SerializeField] protected T _currentValue = default;

        [Title("// Min")]
        [SerializeField] protected bool _clampMin = true;
        [SerializeField] protected T _minValue = default;

        [Title("// Max")]
        [SerializeField] protected bool _clampMax = true;
        [SerializeField] protected T _maxValue = default;

        public Delegates.Action OnValueChange = null;

        public T CurrentValue { get => GetCurrentValue(); set => SetCurrentValue(value); }
        public T MinValue { get => _minValue; set => _minValue = value; }
        public T MaxValue { get => _maxValue; set => _maxValue = value; }

        private void OnEnable()
        {
            SetToMin();
        }

        private void OnValidate()
        {
            SetCurrentValue(_currentValue);
        }

        protected void SetCurrentValue(T _value)
        {
            _currentValue = _value;
            ClampCurrentValue();
            InvokeEvent();
        }

        protected T GetCurrentValue()
        {
            return _currentValue;
        }

        public void InvokeEvent()
        {
            OnValueChange?.Invoke();
        }

        public void SetToMin()
        {
            SetCurrentValue(_minValue);
        }

        public void SetToMax()
        {
            SetCurrentValue(_maxValue);
        }

        protected abstract void ClampCurrentValue();
    }
}
