using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractAction : ScriptableObject
{
    [Title("// Debug")]
    [SerializeField] protected bool _enableDebugMessage = false;

    protected virtual void TryLogInvokeMessage()
    {
        if (_enableDebugMessage)
            Debug.Log($"\"{name}\" Invoked()!", this);
    }

    protected virtual void TryLogSubscribeMessage(string _methodName)
    {
        if (_enableDebugMessage)
            Debug.Log($"\"{name}\" Subscribed the method: \"{_methodName}\"!", this);
    }

    protected virtual void TryLogUnsubscribeMessage(string _methodName)
    {
        if (_enableDebugMessage)
            Debug.Log($"\"{name}\" Unsubscribed the method: \"{_methodName}\"!", this);
    }

    protected virtual void TryLogUnsubscribeAllMessages(System.Delegate[] _invocationList)
    {
        if (_enableDebugMessage)
            for (int i = 0; i < _invocationList.Length; i++)
                TryLogUnsubscribeMessage(_invocationList[i].Method.Name);
    }
}
