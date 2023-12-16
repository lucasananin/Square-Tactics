using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class BaseAction : MonoBehaviour
{
    [SerializeField, ReadOnly] protected Unit _unit = null;
    [SerializeField, ReadOnly] protected bool _isActive = false;
    [SerializeField] protected Material _gridColorMaterial = null;
    [SerializeField] protected int _maxGridHorizontalDistance = 4;
    //[SerializeField] protected int _maxGridVerticalDistance = 2;

    public static EventHandler onAnyActionStarted = null;
    public static EventHandler onAnyActionCompleted = null;
    protected Action _onComplete = null;

    protected virtual void Awake()
    {
        _unit = GetComponent<Unit>();
    }

    public virtual bool IsValidGridPosition(GridPosition _gridPosition)
    {
        var _validGridPositions = GetValidActionGridPositions();
        return _validGridPositions.Contains(_gridPosition);
    }

    public virtual int GetActionPointsCost()
    {
        return 1;
    }

    protected void ActionStart(Action _onCompleteAction)
    {
        _isActive = true;
        _onComplete = _onCompleteAction;
        onAnyActionStarted?.Invoke(this, EventArgs.Empty);
    }

    protected void ActionComplete()
    {
        _isActive = false;
        _onComplete?.Invoke();
        onAnyActionCompleted?.Invoke(this, EventArgs.Empty);
    }

    public Unit GetUnit()
    {
        return _unit;
    }

    public Material GetGridColorMaterial()
    {
        return _gridColorMaterial;
    }

    public EnemyAiAction GetBestEnemyAiAction()
    {
        List<EnemyAiAction> _enemyAiActions = new List<EnemyAiAction>();
        List<GridPosition> _validActionGridPositions = GetValidActionGridPositions();

        foreach (var _gridPosition in _validActionGridPositions)
        {
            EnemyAiAction _enemyAiAction = GetEnemyAiAction(_gridPosition);
            _enemyAiActions.Add(_enemyAiAction);
        }

        if (_enemyAiActions.Count > 0)
        {
            _enemyAiActions.Sort((EnemyAiAction a, EnemyAiAction b) => b.actionValue - b.actionValue);
            return _enemyAiActions[0];
        }
        else
        {
            return null;
        }
    }

    public abstract void TakeAction(GridPosition _gridPosition, Action _onComplete);
    public abstract string GetActionName();
    public abstract List<GridPosition> GetValidActionGridPositions();
    public abstract EnemyAiAction GetEnemyAiAction(GridPosition _gridPosition);
}
