using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Events
{
    public abstract class AbstractFuncSO2<T> : ScriptableObject
    {
        [Title("// General")]
        [SerializeField] bool _enableDebugMessage = false;

        private event Delegates.Function<T> _onFunction;

        private void OnEnable()
        {
            UnsubscribeAll();
        }

        public T InvokeEvent()
        {
            PrintInvokeMessage();
            return _onFunction.Invoke();
        }

        public void Subscribe(Delegates.Function<T> _method)
        {
            PrintSubscribeMessage(_method.Method.Name);
            _onFunction = _method;
        }

        public void UnsubscribeAll()
        {
            if (_onFunction == null) return;
            PrintUnsubscribeAllMessage(_onFunction.GetInvocationList());
            _onFunction = null;
        }

        protected void PrintInvokeMessage()
        {
            if (_enableDebugMessage)
                Debug.Log($"\"{name}\" {_onFunction.Invoke()} Invoked()!", this);
        }

        protected void PrintSubscribeMessage(string _methodName)
        {
            if (_enableDebugMessage)
                Debug.Log($"\"{name}\" Subscribe({_methodName})!", this);
        }

        protected void PrintUnsubscribeMessage(string _methodName)
        {
            if (_enableDebugMessage)
                Debug.Log($"\"{name}\" Unsubscribe({_methodName})!", this);
        }

        protected void PrintUnsubscribeAllMessage(System.Delegate[] _invocationList)
        {
            if (_enableDebugMessage)
                for (int i = 0; i < _invocationList.Length; i++)
                    PrintUnsubscribeMessage(_invocationList[i].Method.Name);
        }
    }
}
