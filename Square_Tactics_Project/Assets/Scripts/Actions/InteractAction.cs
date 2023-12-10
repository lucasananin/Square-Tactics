using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractAction : BaseAction
{
    public override string GetActionName()
    {
        return "Interact";
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
        List<GridPosition> _validGridPositions = new List<GridPosition>();
        GridPosition _myGridPosition = _unit.GetGridPosition();

        for (int x = -_maxGridHorizontalDistance; x <= _maxGridHorizontalDistance; x++)
        {
            for (int z = -_maxGridHorizontalDistance; z <= _maxGridHorizontalDistance; z++)
            {
                GridPosition _offset = new GridPosition(x, z, 0);
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

                if (_validGridPosition == _myGridPosition)
                {
                    continue;
                }

                _validGridPositions.Add(_validGridPosition);
            }
        }

        return _validGridPositions;
    }

    public override void TakeAction(GridPosition _gridPosition, Action _onComplete)
    {
        ActionStart(_onComplete);

        Collider[] _colliders = Pathfinding.Instance.GetObstaclesOnGridPosition(_gridPosition);

        if (_colliders.Length > 0)
        {
            if (_colliders[0].TryGetComponent(out AbstractInteractable _interactable))
            {
                _interactable.Interact(ActionComplete);
                return;
            }
        }

        ActionComplete();
        Debug.LogWarning($"// There's nothing here to interact!");
    }
}
