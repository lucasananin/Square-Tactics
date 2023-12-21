using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class NearestTest : MonoBehaviour
{
    private void Update()
    {
        //transform.position = MouseWorld.Instance.GetHitPoint();

        var _nearestUnit = TransformMethods.GetNearest(transform.position, UnitManager.Instance.GetFriendlyUnitList().ToArray());
        float _distanceToNearest = Vector3.Distance(transform.position, _nearestUnit.position);
        _distanceToNearest = Mathf.RoundToInt(_distanceToNearest * 10);
        int _distanceMultiplier = Mathf.RoundToInt((/*_distanceToNearest **/ 10f) - _distanceToNearest);
        _distanceMultiplier += 1000;
        //int _distanceMultiplier = Mathf.RoundToInt((_distanceToNearest + 10f) - _distanceToNearest);
        //_distanceMultiplier = Mathf.Clamp(_distanceMultiplier, 1, int.MaxValue);
        Debug.Log($"// _nearestUnit: {_nearestUnit}, _distanceMultiplier {_distanceMultiplier}");
    }
}
