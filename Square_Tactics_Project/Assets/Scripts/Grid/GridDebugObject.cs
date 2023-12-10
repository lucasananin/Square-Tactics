using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GridDebugObject : MonoBehaviour
{
    [SerializeField] TextMeshPro _text = null;

    private object _gridObject = null;

    private void LateUpdate()
    {
        UpdateTexts();
    }

    public virtual void SetGridObject(object _gObject)
    {
        _gridObject = _gObject;
    }

    protected virtual void UpdateTexts()
    {
        //_text.SetText(_gridObject.GetInfoString());
        //_text.text = _gridObject.GetInfoString();
        _text.text = _gridObject.ToString();
    }
}
