using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Audio;

public class MeteorAction : BaseAction
{
    [SerializeField] float _timeToAttack = 1f;
    [SerializeField] float _timeToEnd = 1f;
    [SerializeField] int _damage = 1;
    [SerializeField] LayerMask _hitLayers = default;
    [SerializeField] ParticleSystem _particleVfx = null;
    [SerializeField, ReadOnly] Vector3 _targetPosition = default;

    [Title("// Audio")]
    [SerializeField] AudioDataSO _thunderAudio = null;

    public override string GetActionName()
    {
        return _displayName;
    }

    public override EnemyAiAction GetEnemyAiAction(GridPosition _gridPosition)
    {
        return new EnemyAiAction()
        {
            gridPosition = _gridPosition,
            actionValue = 10,
        };
    }

    public override List<GridPosition> GetValidActionGridPositions()
    {
        GridPosition _myGridPosition = _unit.GetGridPosition();
        return GetValidActionGridPositions(_myGridPosition);
    }

    public List<GridPosition> GetValidActionGridPositions(GridPosition _myGridPosition)
    {
        List<GridPosition> _validGridPositions = new List<GridPosition>();

        for (int x = -_maxGridHorizontalDistance; x <= _maxGridHorizontalDistance; x++)
        {
            for (int z = -_maxGridHorizontalDistance; z <= _maxGridHorizontalDistance; z++)
            {
                for (int f = -_maxGridHorizontalDistance; f <= _maxGridHorizontalDistance; f++)
                {
                    GridPosition _offset = new GridPosition(x, z, f);
                    GridPosition _validGridPosition = _myGridPosition + _offset;

                    int _testDistance = Mathf.Abs(x) + Mathf.Abs(z);

                    if (_testDistance > _maxGridHorizontalDistance)
                    {
                        continue;
                    }

                    if (!LevelGrid.Instance.IsValidGridPosition(_validGridPosition))
                    {
                        continue;
                    }

                    _validGridPositions.Add(_validGridPosition);
                }
            }
        }

        return _validGridPositions;
    }

    public override void TakeAction(GridPosition _gridPosition, Action _onComplete)
    {
        _targetPosition = LevelGrid.Instance.GetWorldPosition(_gridPosition);
        ActionStart(_onComplete);
        StartCoroutine(TakeAction_routine());
    }

    public IEnumerator TakeAction_routine()
    {
        GetComponent<UnitAnimator>().TriggerSpecialAttack();

        yield return new WaitForSeconds(_timeToAttack);

        _particleVfx.transform.position = _targetPosition;
        _particleVfx.Play();
        _thunderAudio?.PlayAsSfx();

        Vector3 _halfExtents = Vector3.one * LevelGrid.Instance.GetCellSize() * _maxGridHorizontalDistance * 0.5f;
        Collider[] _collidersHit = Physics.OverlapBox(_targetPosition, _halfExtents, Quaternion.identity, _hitLayers);
        int _count = _collidersHit.Length;

        for (int i = 0; i < _count; i++)
        {
            if (_collidersHit[i].TryGetComponent(out HealthSystem _healthSystem))
            {
                if (_unit.IsMyHealthSystem(_healthSystem)) continue;

                _healthSystem.TakeDamage(_damage * GetDamageBuffMultiplier());
            }
        }

        if (_count > 0)
        {
            //onAnyHit?.Invoke(this, EventArgs.Empty);
            ScreenShaker.Instance.GrenadeProjectile_onAnyGrenadeExploded();
        }

        yield return new WaitForSeconds(_timeToEnd);

        ActionComplete();
    }
}
