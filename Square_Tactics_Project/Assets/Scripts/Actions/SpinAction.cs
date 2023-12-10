using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAction : BaseAction
{
    [Title("// Spin")]
    [SerializeField, ReadOnly] float _totalSpinAmount = 0;

    private void Update()
    {
        if (!_isActive) return;

        var _spinAmount = 360 * Time.deltaTime;
        transform.eulerAngles += new Vector3(0, _spinAmount, 0);

        _totalSpinAmount += _spinAmount;

        if (_totalSpinAmount >= 360)
        {
            ActionComplete();
        }
    }

    public override void TakeAction(GridPosition _gridPosition, Action _onComplete)
    {
        _totalSpinAmount = 0;
        ActionStart(_onComplete);
    }

    public override string GetActionName()
    {
        return "Spin";
    }

    public override List<GridPosition> GetValidActionGridPositions()
    {
        var _myGridPosition = _unit.GetGridPosition();
        return new List<GridPosition>() { _myGridPosition };
    }

    public override int GetActionPointsCost()
    {
        return 2;
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
