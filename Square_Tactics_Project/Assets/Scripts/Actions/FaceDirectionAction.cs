using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceDirectionAction : BaseAction
{
    [SerializeField, ReadOnly] Vector3 _targetPosition = default;

    private void Update()
    {
        if (!_isActive) return;

        Vector3 _moveDirection = (_targetPosition - transform.position).normalized;
        _moveDirection.y = 0;
        Quaternion _targetRotation = Quaternion.LookRotation(_moveDirection, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, 15f * Time.deltaTime);

        if (transform.rotation == _targetRotation)
        {
            ActionComplete();
        }
    }

    public override string GetActionName()
    {
        return $"Face Direction";
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
        var _validGridPositions = new List<GridPosition>();
        var _myGridPosition = _unit.GetGridPosition();

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

                    if (_validGridPosition == _myGridPosition)
                    {
                        continue;
                    }

                    if (!LevelGrid.Instance.IsValidGridPosition(_validGridPosition))
                    {
                        continue;
                    }

                    if (!Pathfinding.Instance.IsWalkableGridPosition(_validGridPosition))
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
    }
}
