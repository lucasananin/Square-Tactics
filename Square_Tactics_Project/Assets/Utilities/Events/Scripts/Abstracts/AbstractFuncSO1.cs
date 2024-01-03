using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Events
{
    public abstract class AbstractFuncSO1<T, U> : ScriptableObject
    {
        [Title("// General")]
        [SerializeField] bool _enableDebugMessage = false;

        private event Delegates.Function<T, U> _onFunction;

        private void OnEnable()
        {
            UnsubscribeAll();
        }

        public T InvokeEvent(U _value)
        {
            PrintInvokeMessage(_value);
            return _onFunction.Invoke(_value);
        }

        public void Subscribe(Delegates.Function<T, U> _method)
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

        protected void PrintInvokeMessage(U _value)
        {
            if (_enableDebugMessage)
                Debug.Log($"\"{name}\" {_onFunction.Invoke(_value)} Invoked({_value})!", this);
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
