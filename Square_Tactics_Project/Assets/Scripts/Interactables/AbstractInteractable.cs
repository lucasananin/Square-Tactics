using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractInteractable : MonoBehaviour
{
    [Title("// General")]
    [SerializeField] protected float _interactionTime = 1f;

    protected WaitForSeconds _interactionWaitTime = null;

    private void Awake()
    {
        _interactionWaitTime = new WaitForSeconds(_interactionTime);
    }

    protected virtual IEnumerator InteractRoutine(Action _onComplete)
    {
        yield return _interactionWaitTime;

        _onComplete?.Invoke();
    }

    public abstract void Interact(Action _onComplete);
}
