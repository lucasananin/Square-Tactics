using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveArrowAction : BaseAction
{
    [Title("// Shoot")]
    [SerializeField] float _rotSpeed = 10f;
    [SerializeField] int _damage = 4;
    [SerializeField] float _timeToEnd = 1f;
    [SerializeField] LayerMask _obstaclesLayerMask = default;
    [SerializeField, ReadOnly] Vector3 _targetPosition = default;

    public int Damage { get => _damage; private set => _damage = value; }
    public Vector3 TargetPosition { get => _targetPosition; private set => _targetPosition = value; }

    private void Update()
    {
        if (!_isActive) return;

        Vector3 _moveDir = (_targetPosition - transform.position).normalized;
        Quaternion _newRot = Quaternion.LookRotation(_moveDir, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, _newRot, _rotSpeed * Time.deltaTime);
    }

    public override string GetActionName()
    {
        return _displayName;
    }

    public override EnemyAiAction GetEnemyAiAction(GridPosition _gridPosition)
    {
        return new EnemyAiAction()
        {
            gridPosition = _gridPosition,
            actionValue = 0,
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

                    //if (!LevelGrid.Instance.HasAnyUnitOnThisGridPosition(_validGridPosition))
                    //{
                    //    continue;
                    //}

                    //Unit _targetUnit = LevelGrid.Instance.GetUnitOnThisGridPosition(_validGridPosition);

                    //if (_unit.IsEnemy() == _targetUnit.IsEnemy())
                    //{
                    //    continue;
                    //}

                    _targetPosition = LevelGrid.Instance.GetWorldPosition(_validGridPosition);

                    Vector3 _unitWorldPosition = LevelGrid.Instance.GetWorldPosition(_myGridPosition);
                    Vector3 _raycastOrigin = _unitWorldPosition + Vector3.up * 1.7f;
                    Vector3 _shootDir = (_targetPosition - _unitWorldPosition).normalized;
                    float _distanceBetweenTargets = (_targetPosition - _unitWorldPosition).magnitude;

                    if (Physics.Raycast(_raycastOrigin, _shootDir, _distanceBetweenTargets, _obstaclesLayerMask))
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

    private IEnumerator TakeAction_routine()
    {
        GetComponent<UnitAnimator>().TriggerExplosiveArrow(this);

        yield return new WaitForSeconds(_timeToEnd);

        ActionComplete();
    }
}
