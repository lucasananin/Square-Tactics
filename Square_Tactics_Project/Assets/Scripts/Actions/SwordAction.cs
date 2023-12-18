using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAction : BaseAction
{
    [Title("// Sword")]
    [SerializeField] float _rotSpeed = 10f;
    [SerializeField] int _damage = 1;
    [SerializeField] float _timeToDamage = 0.7f;
    [SerializeField, ReadOnly] State _state = State.SwingingSwordBeforeHit;
    [SerializeField, ReadOnly] float _stateTimer = 0;
    [SerializeField, ReadOnly] Unit _targetUnit = null;
    [SerializeField, ReadOnly] Vector3 _targetGridWorldPosition = default;

    public static event EventHandler onAnySwordHit = null;
    public event EventHandler onSwordActionStarted = null;
    public event EventHandler onSwordActionCompleted = null;

    private void Update()
    {
        if (!_isActive) return;

        _stateTimer -= Time.deltaTime;

        switch (_state)
        {
            case State.SwingingSwordBeforeHit:
                break;
            case State.SwingingSwordAfterHit:
                break;
            default:
                break;
        }

        if (_stateTimer <= 0)
        {
            NextState();
        }

        //if (_targetUnit == null) return;

        //Vector3 _moveDir = (_targetUnit.transform.position - transform.position).normalized;
        Vector3 _moveDir = (_targetGridWorldPosition - transform.position).normalized;
        Quaternion _newRot = Quaternion.LookRotation(_moveDir, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, _newRot, _rotSpeed * Time.deltaTime);
    }

    private void NextState()
    {
        switch (_state)
        {
            case State.SwingingSwordBeforeHit:
                _state = State.SwingingSwordAfterHit;
                _stateTimer = 0.5f;

                if (_targetUnit != null)
                {
                    _targetUnit.GetComponent<HealthSystem>().TakeDamage(_damage * GetDamageBuffMultiplier());
                    onAnySwordHit?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.SwingingSwordAfterHit:
                onSwordActionCompleted?.Invoke(this, EventArgs.Empty);

                // _slashCount++;
                // if (_slashCount < _maxSlashCount)
                // ataca novamente.
                // else 
                ActionComplete();
                break;
            default:
                break;
        }
    }

    public override string GetActionName()
    {
        return "Sword";
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
        _targetUnit = LevelGrid.Instance.GetUnitOnThisGridPosition(_gridPosition);
        _targetGridWorldPosition = LevelGrid.Instance.GetWorldPosition(_gridPosition);

        _state = State.SwingingSwordBeforeHit;
        _stateTimer = _timeToDamage;

        onSwordActionStarted?.Invoke(this, EventArgs.Empty);
        ActionStart(_onComplete);
    }

    private enum State
    {
        SwingingSwordBeforeHit,
        SwingingSwordAfterHit,
    }
}
