using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitAction : BaseAction
{
    public override string GetActionName()
    {
        return $"Wait";
    }

    public override EnemyAiAction GetEnemyAiAction(GridPosition _gridPosition)
    {
        return new EnemyAiAction()
        {
            gridPosition = _gridPosition,
            actionValue = int.MinValue,
        };
    }

    public override List<GridPosition> GetValidActionGridPositions()
    {
        GridPosition _myGridPosition = _unit.GetGridPosition();
        return new List<GridPosition>() { _myGridPosition };
    }

    public override void TakeAction(GridPosition _gridPosition, Action _onComplete)
    {
        ActionStart(_onComplete);
        _unit.SpendAllActionPoints();
        ActionComplete();
    }
}
