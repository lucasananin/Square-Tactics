using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] HealthSystem _healthSystem = null;
    [SerializeField] bool _isEnemy = false;
    [SerializeField, ReadOnly] BaseAction[] _baseActions = null;
    [SerializeField, ReadOnly] int _actionPoints = 2;

    private GridPosition _gridPosition = default;

    public static event EventHandler onAnyUnitSpawned = null;
    public static event EventHandler onAnyUnitDead = null;

    private void Awake()
    {
        _baseActions = GetComponents<BaseAction>();
    }

    private void Start()
    {
        _gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.AddUnitAtGridPosition(_gridPosition, this);
        onAnyUnitSpawned?.Invoke(this, EventArgs.Empty);
    }

    private void OnEnable()
    {
        _healthSystem.onDead += Die;
        TurnSystem.Instance.onTurnChanged += ResetActionPoints;
    }

    private void OnDisable()
    {
        _healthSystem.onDead -= Die;
        TurnSystem.Instance.onTurnChanged -= ResetActionPoints;
    }

    private void Update()
    {
        var _newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);

        if (_newGridPosition != _gridPosition)
        {
            var _oldGridPosition = _gridPosition;
            _gridPosition = _newGridPosition;
            LevelGrid.Instance.UnitMovedGridPosition(this, _oldGridPosition, _newGridPosition);
        }
    }

    public T GetAction<T>() where T : BaseAction
    {
        int _count = _baseActions.Length;

        for (int i = 0; i < _count; i++)
        {
            var _baseAction = _baseActions[i];

            if (_baseAction is T)
            {
                return _baseAction as T;
            }
        }

        return null;
    }

    public GridPosition GetGridPosition()
    {
        return _gridPosition;
    }

    public BaseAction[] GetBaseActions()
    {
        return _baseActions;
    }

    public bool TrySpendActionPointsToTakeAction(BaseAction _baseAction)
    {
        if (CanSpendActionPointsToTakeAction(_baseAction))
        {
            SpendActionPoints(_baseAction.GetActionPointsCost());
            return true;
        }

        return false;
    }

    public bool CanSpendActionPointsToTakeAction(BaseAction _baseAction)
    {
        return _actionPoints >= _baseAction.GetActionPointsCost();
    }

    public void SpendActionPoints(BaseAction _baseAction)
    {
        _actionPoints -= _baseAction.GetActionPointsCost();
    }

    public void SpendActionPoints(int _amount)
    {
        _actionPoints -= _amount;
    }

    private void ResetActionPoints()
    {
        //bool _isPlayerTurn = TurnSystem.Instance.IsPlayerTurn();
        //if ((!_isEnemy && _isPlayerTurn) || (_isEnemy && !_isPlayerTurn))
        _actionPoints = 2;
    }

    public int GetActionPoints()
    {
        return _actionPoints;
    }

    public bool IsEnemy()
    {
        return _isEnemy;
    }

    public float GetHealthNormalized()
    {
        return _healthSystem.GetHealthNormalized();
    }

    public bool IsMyHealthSystem(HealthSystem _value)
    {
        return _healthSystem == _value;
    }

    private void Die()
    {
        LevelGrid.Instance.RemoveUnitAtGridPosition(_gridPosition, this);
        onAnyUnitDead?.Invoke(this, EventArgs.Empty);
        //Destroy(gameObject);
    }

    [Button]
    private void Debug_IncreaseActionPoints()
    {
        SpendActionPoints(-4);
    }
}
