using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurnAction : BaseAction
{
    public override string GetActionName()
    {
        return $"End Turn";
    }

    public override List<GridPosition> GetValidActionGridPositions()
    {
        var _myGridPosition = _unit.GetGridPosition();
        return new List<GridPosition>() { _myGridPosition };
    }

    public override void TakeAction(GridPosition _gridPosition, Action _onComplete)
    {
        ActionStart(_onComplete);
        StartCoroutine(TakeAction_routine());
    }

    private IEnumerator TakeAction_routine()
    {
        yield return new WaitForSeconds(1);

        TurnSystem.Instance.NextTurn();
        ActionComplete();
    }

    public override int GetActionPointsCost()
    {
        return 0;
    }

    public override EnemyAiAction GetEnemyAiAction(GridPosition _gridPosition)
    {
        return new EnemyAiAction()
        {
            gridPosition = _gridPosition,
            actionValue = 0,
        };
    }
}
