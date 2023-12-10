using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    [SerializeField] TrailRenderer _trailRenderer = null;
    [SerializeField] float _moveSpeed = 200f;
    [SerializeField] ParticleSystem _hitVfx = null;
    [SerializeField, ReadOnly] Unit _targetUnit = null;
    [SerializeField, ReadOnly] Vector3 _targetPosition = default;
    [SerializeField, ReadOnly] bool _hasReachedTarget = false;
    [SerializeField, ReadOnly] int _damage = 0;

    private void Update()
    {
        if (_hasReachedTarget) return;

        Vector3 _myPosition = transform.position;

        float _step = _moveSpeed * Time.deltaTime;
        _myPosition = Vector3.MoveTowards(_myPosition, _targetPosition, _step);
        transform.position = _myPosition;

        if (_myPosition == _targetPosition)
        {
            _hasReachedTarget = true;
            _targetUnit.GetComponent<HealthSystem>().TakeDamage(_damage);
            Instantiate(_hitVfx, _targetPosition, Quaternion.identity);
        }
    }

    private void LateUpdate()
    {
        if (_hasReachedTarget && _trailRenderer.positionCount <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Setup(ShootAction.OnShootEventArgs _e)
    {
        _hasReachedTarget = false;

        _targetUnit = _e.targetUnit;
        _targetPosition = _targetUnit.transform.position;
        _targetPosition.y += 1.7f;
        _damage = _e.damage;
        //SetDamage(_e.damage);
        //SetTargetPosition(_targetPosition);
    }

    //public void SetTargetPosition(Vector3 _targetPosition)
    //{
    //    this._targetPosition = _targetPosition;
    //}

    //public void SetDamage(int _amount)
    //{
    //    _damage = _amount;
    //}
}
