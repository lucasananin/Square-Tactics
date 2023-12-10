using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseWorld : Singleton<MouseWorld>
{
    [SerializeField] LayerMask _mouseLayerMask = default;

    private void Update()
    {
        transform.position = GetHitPoint();
    }

    public Vector3 GetHitPoint()
    {
        Ray _ray = Camera.main.ScreenPointToRay(InputManager.Instance.GetMousePosition());
        Physics.Raycast(_ray, out RaycastHit _hitInfo, float.MaxValue, _mouseLayerMask);
        return _hitInfo.point;
    }
}
