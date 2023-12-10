using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    [SerializeField] bool _invert = false;
    [SerializeField, ReadOnly] Transform _cameraTransform = null;

    private void Start()
    {
        _cameraTransform = Camera.main.transform;
    }

    private void LateUpdate()
    {
        //if (_invert)
        //{
        //    var _dir = (_cameraTransform.position - transform.position).normalized;
        //    transform.LookAt(transform.position + _dir * -1);
        //}
        //else
        //{
        //    transform.LookAt(_cameraTransform);
        //}

        transform.forward = _invert ? _cameraTransform.forward * -1 : _cameraTransform.forward;
    }
}
