using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utilities
{
    public static class RandomMethods
    {
        public static T GetRandomInList<T>(IList<T> _objects)
        {
            if (_objects.Count == 1)
            {
                return _objects[0];
            }

            int _randomIndex = Random.Range(0, _objects.Count);
            //Debug.Log($"// Index: {_randomIndex}");

            return _objects[_randomIndex];
        }

        public static T GetRandomInList<T>(IReadOnlyList<float> _probabilities, IReadOnlyList<T> _objects)
        {
            if (_probabilities == null || _probabilities.Count <= 0 || _objects.Count == 1)
            {
                return _objects[0];
            }

            _probabilities= _probabilities.OrderByDescending(x => x).ToList();
            float _totalProbability = 0f;
            int _count = _probabilities.Count;

            for (int i = 0; i < _count; i++)
            {
                float _item = _probabilities[i];
                _totalProbability += _item;
            }

            float _randomValue = Random.value * _totalProbability;
            _count = _probabilities.Count;
            //Debug.Log($"Total: {_totalProbability}");
            //Debug.Log($"Random Point: {_randomValue}");

            for (int i = 0; i < _count; i++)
            {
                if (_randomValue < _probabilities[i])
                {
                    return _objects[i];
                }
                else
                {
                    _randomValue -= _probabilities[i];
                }
            }

            return _objects[_probabilities.Count - 1];
        }

        public static T GetRandomInCurve<T>(AnimationCurve _probabilityCurve, List<T> _objects)
        {
            if (_probabilityCurve == null || _probabilityCurve.length <= 1 || _objects.Count == 1)
            {
                return _objects[0];
            }

            float _randomValue = Random.value;
            float _curveValue = _probabilityCurve.Evaluate(_randomValue);
            int _valueInteger = Mathf.FloorToInt(_curveValue);

            float _firstKeyValue = _probabilityCurve.keys[0].value;
            float _lastKeyValue = _probabilityCurve.keys[_probabilityCurve.length - 1].value;
            float _finalIndex = Mathf.Clamp(_valueInteger, _firstKeyValue, _lastKeyValue - 1);

            //Debug.Log($"//");
            //Debug.Log($"Random Value - {_randomValue}");
            //Debug.Log($"Curve Value - {_curveValue}");
            //Debug.Log($"Value Integer - {_valueInteger}");
            //Debug.Log($"First Key - {_firstKeyValue}");
            //Debug.Log($"Last Key - {_lastKeyValue}");
            //Debug.Log($"Final Index - {_finalIndex}");

            return _objects[Mathf.RoundToInt(_finalIndex)];
        }
    }
}
