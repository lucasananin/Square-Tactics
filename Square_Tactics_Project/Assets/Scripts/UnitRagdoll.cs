using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitRagdoll : MonoBehaviour
{
    [SerializeField] Transform _rootBone = null;

    public void SetOriginalRootBone(Transform _originalRootBone)
    {
        MatchAllChildTransforms(_originalRootBone, _rootBone);

        Vector3 _randomDir = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
        ApplyExplosionToRagdoll(_rootBone, 300, transform.position + _randomDir, 10);
    }

    private void MatchAllChildTransforms(Transform _root, Transform _clone)
    {
        foreach (Transform _child in _root)
        {
            Transform _cloneChild = _clone.Find(_child.name);

            if (_cloneChild != null)
            {
                _cloneChild.position = _child.position;
                _cloneChild.rotation = _child.rotation;
                MatchAllChildTransforms(_child, _cloneChild);
            }
        }
    }

    private void ApplyExplosionToRagdoll(Transform _root, float _explosionForce, Vector3 _explosionPosition, float _explosionRange)
    {
        foreach (Transform _child in _root)
        {
            if (_child.TryGetComponent(out Rigidbody _childRb))
            {
                float _randomForce = Random.Range(_explosionForce, _explosionForce * 2f);
                _childRb.AddExplosionForce(_randomForce, _explosionPosition, _explosionRange);
                ApplyExplosionToRagdoll(_child, _explosionForce, _explosionPosition, _explosionRange);
            }
        }
    }
}
