using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Audio;

public class ExplosiveArrowProjectile : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 200f;
    [SerializeField] LayerMask _hitLayers = default;
    [SerializeField] TrailRenderer _trailRenderer = null;
    [SerializeField] ParticleSystem _hitVfx = null;
    [SerializeField, ReadOnly] Vector3 _targetPosition = default;
    [SerializeField, ReadOnly] bool _hasReachedTarget = false;
    [SerializeField, ReadOnly] ExplosiveArrowAction _explosiveArrowAction = null;

    [Title("// Audio")]
    [SerializeField] AudioDataSO _explosionAudio = null;

    //public static event Action onAnyBulletHit = null;

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

            Vector3 _halfExtents = Vector3.one * LevelGrid.Instance.GetCellSize() * _explosiveArrowAction.MaxGridHorizontalDistance * 0.5f;
            Collider[] _collidersHit = Physics.OverlapBox(transform.position, _halfExtents, Quaternion.identity, _hitLayers);
            int _count = _collidersHit.Length;

            for (int i = 0; i < _count; i++)
            {
                if (_collidersHit[i].TryGetComponent(out HealthSystem _healthSystem))
                {
                    if (_explosiveArrowAction.GetUnit().IsMyHealthSystem(_healthSystem)) continue;

                    _healthSystem.TakeDamage(_explosiveArrowAction.Damage * _explosiveArrowAction.GetDamageBuffMultiplier());
                }
            }

            if (_count > 0)
            {
                //onAnyBulletHit?.Invoke();
                ScreenShaker.Instance.GrenadeProjectile_onAnyGrenadeExploded();
            }

            _explosionAudio?.PlayAsSfx();
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

    public void Setup(ExplosiveArrowAction _actionValue)
    {
        _hasReachedTarget = false;
        _explosiveArrowAction = _actionValue;
        _targetPosition = _actionValue.TargetPosition;
    }
}
