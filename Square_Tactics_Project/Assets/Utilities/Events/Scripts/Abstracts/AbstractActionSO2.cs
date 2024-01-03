using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Events
{
    public abstract class AbstractActionSO2<T, U> : AbstractAction
    {
        private event Delegates.Action<T, U> _onAction;

        private void OnEnable()
        {
            UnsubscribeAll();
        }

        [Button]
        public void InvokeEvent(T _value1, U _value2)
        {
            TryLogInvokeMessage(_value1, _value2);
            _onAction?.Invoke(_value1, _value2);
        }

        public void Subscribe(Delegates.Action<T, U> _method)
        {
            TryLogSubscribeMessage(_method.Method.Name);
            _onAction += _method;
        }

        public void Unsubscribe(Delegates.Action<T, U> _method)
        {
            TryLogUnsubscribeMessage(_method.Method.Name);
            _onAction -= _method;
        }

        public void UnsubscribeAll()
        {
            if (_onAction == null) return;
            TryLogUnsubscribeAllMessages(_onAction.GetInvocationList());
            _onAction = null;
        }

        protected void TryLogInvokeMessage(T _value1, U _value2)
        {
            if (_enableDebugMessage)
                Debug.Log($"\"{name}\" Invoked({_value1}, {_value2})!", this);
        }
    }
}
