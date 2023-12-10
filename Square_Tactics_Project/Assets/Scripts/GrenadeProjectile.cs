using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeProjectile : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 15f;
    [SerializeField] int _cellDamageArea = 3;
    [SerializeField] float _maxCurveHeight = 3f;
    [SerializeField] AnimationCurve _arcYAnimationCurve = null;
    [SerializeField] ParticleSystem _explosionVfx = null;
    [SerializeField, ReadOnly] Vector3 _targetPosition = default;
    [SerializeField, ReadOnly] bool _hasReachedTarget = false;
    [SerializeField, ReadOnly] float _totalDistance = 0f;
    [SerializeField, ReadOnly] Vector3 _positionXZ = default;

    public static event Action onAnyGrenadeExploded = null;
    private event Action _onGrenadeComplete = null;

    private void Update()
    {
        if (_hasReachedTarget) return;

        float _step = _moveSpeed * Time.deltaTime;
        _positionXZ = Vector3.MoveTowards(_positionXZ, _targetPosition, _step);

        float _distance = Vector3.Distance(_positionXZ, _targetPosition);
        float _distanceNormalized = _distance / _totalDistance;
        //float _distanceNormalized = 1 - _distance / _totalDistance;
        float _maxHeight = _totalDistance / _maxCurveHeight;
        float _positionY = _arcYAnimationCurve.Evaluate(_distanceNormalized) * _maxHeight;
        _positionXZ.y = _positionY;

        transform.position = _positionXZ;

        if (_positionXZ == _targetPosition)
        {
            _hasReachedTarget = true;

            Collider[] _colliders = Physics.OverlapBox(_targetPosition, Vector3.one * (LevelGrid.Instance.GetCellSize() * _cellDamageArea * 0.4f));
            int _count = _colliders.Length;

            for (int i = 0; i < _count; i++)
            {
                if (_colliders[i].TryGetComponent(out HealthSystem _healthComponent))
                {
                    _healthComponent.TakeDamage(1);
                }
            }

            Instantiate(_explosionVfx, _targetPosition + Vector3.up, Quaternion.identity);
            onAnyGrenadeExploded?.Invoke();
            StartCoroutine(CompleteGrenade_routine());
        }
    }

    public void Setup(GridPosition _targetGridPosition, Action _onGrenadeComplete)
    {
        _hasReachedTarget = false;
        _targetPosition = LevelGrid.Instance.GetWorldPosition(_targetGridPosition);

        _positionXZ = transform.position;
        _positionXZ.y = 0;
        _totalDistance = (_targetPosition - _positionXZ).magnitude;

        this._onGrenadeComplete = _onGrenadeComplete;
    }

    private IEnumerator CompleteGrenade_routine()
    {
        yield return new WaitForSeconds(1f);
        _onGrenadeComplete?.Invoke();
        Destroy(gameObject);
    }
}
