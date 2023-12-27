using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    [SerializeField] TrailRenderer _trailRenderer = null;
    [SerializeField] ParticleSystem _particleVfx = null;
    [SerializeField] float _moveSpeed = 200f;
    [SerializeField] float _yOffset = 1.7f;
    [SerializeField] ParticleSystem _hitVfx = null;
    [SerializeField, ReadOnly] Vector3 _targetPosition = default;
    [SerializeField, ReadOnly] bool _hasReachedTarget = false;
    [SerializeField, ReadOnly] int _damage = 0;

    private ShootAction.OnShootEventArgs _shootArgs = null;

    public static event Action onAnyBulletHit = null;

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
            _shootArgs.targetUnit.GetComponent<HealthSystem>().TakeDamage(_damage * GetAttackDirectionMultiplier());
            Instantiate(_hitVfx, _targetPosition, Quaternion.identity);
            onAnyBulletHit?.Invoke();

            if (_particleVfx != null)
                _particleVfx.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
    }

    private void LateUpdate()
    {
        if (_trailRenderer != null && _hasReachedTarget && _trailRenderer.positionCount <= 0)
        {
            Destroy(gameObject);
        }

        if (_particleVfx != null && !_particleVfx.isPlaying)
        {
            Destroy(gameObject);
        }
    }

    public void Setup(ShootAction.OnShootEventArgs _args)
    {
        _hasReachedTarget = false;

        //_targetUnit = _e.targetUnit;
        _shootArgs = _args;
        _targetPosition = _args.targetUnit.transform.position;
        _targetPosition.y += _yOffset;
        _damage = _args.damage;
        //SetDamage(_e.damage);
        //SetTargetPosition(_targetPosition);
    }

    private int GetAttackDirectionMultiplier()
    {
        if (_shootArgs.shootingUnit.IsEnemy())
        {
            return 1;
        }

        float _dot = Vector3.Dot(_shootArgs.shootingUnit.transform.forward, _shootArgs.targetUnit.transform.forward);
        _dot = Mathf.RoundToInt(_dot);

        switch (_dot)
        {
            case 1:
                return 3;
            case 0:
                return 2;
            default:
                return 1;
        }
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
