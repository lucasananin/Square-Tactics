using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Events
{
    public abstract class AbstractActionSO1<T> : AbstractAction
    {
        private event Delegates.Action<T> _onAction;

        private void OnEnable()
        {
            UnsubscribeAll();
        }

        [Button]
        public void InvokeEvent(T _value)
        {
            TryLogInvokeMessage(_value);
            _onAction?.Invoke(_value);
        }

        public void Subscribe(Delegates.Action<T> _method)
        {
            TryLogSubscribeMessage(_method.Method.Name);
            _onAction += _method;
        }

        public void Unsubscribe(Delegates.Action<T> _method)
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

        protected void TryLogInvokeMessage(T _value)
        {
            if (_enableDebugMessage)
                Debug.Log($"\"{name}\" Invoked({_value})!", this);
        }
    }
}
