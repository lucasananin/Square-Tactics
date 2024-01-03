using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Events
{
    public abstract class AbstractActionSO : AbstractAction
    {
        private event Delegates.Action _onAction;

        private void OnEnable()
        {
            UnsubscribeAll();
        }

        [Button]
        public void InvokeEvent()
        {
            TryLogInvokeMessage();
            _onAction?.Invoke();
        }

        public void Subscribe(Delegates.Action _method)
        {
            TryLogSubscribeMessage(_method.Method.Name);
            _onAction += _method;
        }

        public void Unsubscribe(Delegates.Action _method)
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
    }
}
