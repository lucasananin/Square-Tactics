using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utilities;

public class Tester : MonoBehaviour
{
    [SerializeField] List<string> _names = null;
    [SerializeField] List<float> _probs = null;

    [Button]
    private void GetRandomName()
    {
        var _name = RandomMethods.GetRandomInList(_probs, _names);
        Debug.Log($"// {_name}");
    }

    [Button]
    private void SortProbs()
    {
        _probs = _probs.OrderByDescending(x => x).ToList();
    }

    [Button]
    private void GetNearest()
    {
        var _colliders = Physics.OverlapSphere(transform.position, 5f);
        var _nearestTarget = TransformMethods.GetNearest(transform.position, _colliders);
        Debug.Log($"// _nearestTarget: {_nearestTarget.name}", _nearestTarget);
    }
}
