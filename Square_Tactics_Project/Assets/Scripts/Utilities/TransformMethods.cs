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

        public static List<Transform> GetTwoNearest(Vector3 _position, Component[] _components/*, int _hits*/)
        {
            var _nearestList = new List<Transform>();
            Transform _nearestTarget_1 = null;
            Transform _nearestTarget_2 = null;
            float _nearestDistance = Mathf.Infinity;
            int _count = _components.Length;

            for (int i = 0; i < _count; i++)
            {
                Vector3 _directionToTarget = _components[i].transform.position - _position;
                float _distanceToTarget = _directionToTarget.sqrMagnitude;

                if (_distanceToTarget < _nearestDistance)
                {
                    _nearestDistance = _distanceToTarget;
                    _nearestTarget_2 = _nearestTarget_1;
                    _nearestTarget_1 = _components[i].transform;

                    _nearestList.Clear();
                    _nearestList.Add(_nearestTarget_1);
                    _nearestList.Add(_nearestTarget_2);
                }
            }

            return _nearestList;
        }
    }
}
