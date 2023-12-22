using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public static class TransformMethods
    {
        public static Transform GetNearest(Vector3 _position, Component[] _components/*, int _hits*/)
        {
            Transform _nearestTarget = null;
            float _nearestDistance = Mathf.Infinity;
            int _count = _components.Length;

            for (int i = 0; i < _count; i++)
            {
                Vector3 _directionToTarget = _components[i].transform.position - _position;
                float _distanceToTarget = _directionToTarget.sqrMagnitude;

                if (_distanceToTarget < _nearestDistance)
                {
                    _nearestDistance = _distanceToTarget;
                    _nearestTarget = _components[i].transform;
                }
            }

            return _nearestTarget;
        }
    }
}
