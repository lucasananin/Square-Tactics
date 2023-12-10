using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeAction : BaseAction
{
    [Title("// Grenade")]
    [SerializeField] GrenadeProjectile _grenadePrefab = null;

    private void Update()
    {
        if (!_isActive) return;

        //ActionComplete();
    }

    public override string GetActionName()
    {
        return "Grenade";
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
        var _myGridPosition = _unit.GetGridPosition();
        return GetValidActionGridPositions(_myGridPosition);
    }

    public List<GridPosition> GetValidActionGridPositions(GridPosition _myGridPosition)
    {
        var _validGridPositions = new List<GridPosition>();

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
        GrenadeProjectile _grenadeInstance = Instantiate(_grenadePrefab, _unit.transform.position, Quaternion.identity);
        _grenadeInstance.Setup(_gridPosition, ActionComplete);
        ActionStart(_onComplete);
    }
}
