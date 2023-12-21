using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    [SerializeField, ReadOnly] State _state = State.WaitingForEnemyTurn;
    [SerializeField, ReadOnly] float _timer = 0f;

    private void OnEnable()
    {
        TurnSystem.Instance.onTurnChanged += ResetTimer;
    }

    private void OnDisable()
    {
        TurnSystem.Instance.onTurnChanged -= ResetTimer;
    }

    private void Update()
    {
        if (TurnSystem.Instance.IsPlayerTurn()) return;

        switch (_state)
        {
            case State.WaitingForEnemyTurn:
                break;
            case State.TakingTurn:
                _timer -= Time.deltaTime;

                if (_timer <= 0)
                {
                    //TurnSystem.Instance.NextTurn();

                    if (TryTakeEnemyAiAction(SetStateTakingTurn))
                    {
                        _state = State.Busy;
                    }
                    else
                    {
                        TurnSystem.Instance.NextTurn();
                    }
                }
                break;
            case State.Busy:
                break;
            default:
                break;
        }
    }

    private void SetStateTakingTurn()
    {
        _timer = 1f;
        _state = State.TakingTurn;
    }

    private bool TryTakeEnemyAiAction(System.Action _onEnemyAiActionComplete)
    {
        var _unitList = UnitManager.Instance.GetEnemyList();
        int _count = _unitList.Count;

        for (int i = 0; i < _count; i++)
        {
            Unit _enemyUnit = _unitList[i];

            if (TryTakeEnemyAiAction(_enemyUnit, _onEnemyAiActionComplete))
            {
                return true;
            }
        }

        return false;
    }

    private bool TryTakeEnemyAiAction(Unit _enemyUnit, System.Action _onEnemyAiActionComplete)
    {
        EnemyAiAction _bestEnemyAiAction = null;
        BaseAction _bestBaseAction = null;

        foreach (var _baseAction in _enemyUnit.GetBaseActions())
        {
            if (!_enemyUnit.CanSpendActionPointsToTakeAction(_baseAction)) continue;
            if (_baseAction is EndTurnAction) continue;

            if (_bestEnemyAiAction == null)
            {
                _bestEnemyAiAction = _baseAction.GetBestEnemyAiAction();
                _bestBaseAction = _baseAction;
            }
            else
            {
                EnemyAiAction _testEnemyAiAction = _baseAction.GetBestEnemyAiAction();

                if (_testEnemyAiAction != null && _testEnemyAiAction.actionValue > _bestEnemyAiAction.actionValue)
                {
                    _bestEnemyAiAction = _baseAction.GetBestEnemyAiAction();
                    _bestBaseAction = _baseAction;
                }
            }
        }

        //if (_bestEnemyAiAction != null && _bestEnemyAiAction.actionValue > 0 && _enemyUnit.TrySpendActionPointsToTakeAction(_bestBaseAction))
        if (_bestEnemyAiAction != null && _enemyUnit.TrySpendActionPointsToTakeAction(_bestBaseAction))
        {
            _bestBaseAction.TakeAction(_bestEnemyAiAction.gridPosition, _onEnemyAiActionComplete);
            return true;
        }
        else
        {
            return false;
        }

        //SpinAction _spinAction = _enemyUnit.GetComponent<SpinAction>();
        //GridPosition _actionGridPosition = _enemyUnit.GetGridPosition();

        //bool _hasEnoughActionPoints = _enemyUnit.CanSpendActionPointsToTakeAction(_spinAction);
        //bool _isValidGridPosition = _spinAction.IsValidGridPosition(_actionGridPosition);

        //if (_hasEnoughActionPoints && _isValidGridPosition)
        //{
        //    _enemyUnit.SpendActionPoints(_spinAction);
        //    _spinAction.TakeAction(_actionGridPosition, _onEnemyAiActionComplete);
        //    return true;
        //}

        //return false;
    }

    private void ResetTimer()
    {
        if (!TurnSystem.Instance.IsPlayerTurn())
        {
            _state = State.TakingTurn;
            _timer = 2f;
        }
    }

    private enum State
    {
        WaitingForEnemyTurn,
        TakingTurn,
        Busy,
    }
}
