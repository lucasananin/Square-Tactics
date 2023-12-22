using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAction : BaseAction
{
    [Title("// Shoot")]
    [SerializeField] float _rotSpeed = 10f;
    [SerializeField] int _damage = 4;
    [SerializeField] int _minGridDistance = 1;
    [SerializeField] float _timeToEnd = 0.5f;
    [SerializeField] Transform _shoulderTarget = null;
    [SerializeField] LayerMask _obstaclesLayerMask = default;
    [SerializeField, ReadOnly] State _state = State.Aiming;
    [SerializeField, ReadOnly] float _stateTimer = 0;
    [SerializeField, ReadOnly] Unit _targetUnit = null;
    [SerializeField, ReadOnly] bool _canShootBullet = false;

    public int MinGridDistance { get => _minGridDistance; private set => _minGridDistance = value; }

    public event EventHandler<OnShootEventArgs> onShoot = null;
    public static event EventHandler<OnShootEventArgs> onAnyShoot = null;

    public class OnShootEventArgs : EventArgs
    {
        public Unit targetUnit = null;
        public Unit shootingUnit = null;
        public int damage = 4;
    }

    private void Update()
    {
        if (!_isActive) return;

        _stateTimer -= Time.deltaTime;

        if (_state == State.Shooting)
        {
            if (_canShootBullet)
            {
                _canShootBullet = false;
                Shoot();
            }
        }

        if (_stateTimer <= 0)
        {
            NextState();
        }

        if (_targetUnit == null) return;

        Vector3 _moveDir = (_targetUnit.transform.position - transform.position).normalized;
        _moveDir.y = 0f;
        Quaternion _newRot = Quaternion.LookRotation(_moveDir, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, _newRot, _rotSpeed * Time.deltaTime);
    }

    private void Shoot()
    {
        var _shootEventArgs = new OnShootEventArgs()
        {
            shootingUnit = _unit,
            targetUnit = _targetUnit,
            damage = _damage,
        };

        onShoot?.Invoke(this, _shootEventArgs);
        onAnyShoot?.Invoke(this, _shootEventArgs);
        //ScreenShaker.Instance.Shake();
        //_targetUnit.Debug_TakeDamage();
        //_targetUnit.GetComponent<HealthSystem>().TakeDamage(4);
    }

    private void NextState()
    {
        switch (_state)
        {
            case State.Aiming:
                _state = State.Shooting;
                _stateTimer = 0.1f;
                //Debug.Log($"// Shooting");
                break;
            case State.Shooting:
                _state = State.Cooloff;
                _stateTimer = _timeToEnd;
                //Debug.Log($"// Cooloff");
                break;
            case State.Cooloff:
                ActionComplete();
                //Debug.Log($"// End");
                break;
            default:
                break;
        }
    }

    public override string GetActionName()
    {
        return $"Shoot";
    }

    public override List<GridPosition> GetValidActionGridPositions()
    {
        GridPosition _myGridPosition = _unit.GetGridPosition();
        return GetValidActionGridPositions(_myGridPosition);
    }

    public List<GridPosition> GetValidActionGridPositions(GridPosition _myGridPosition)
    {
        List<GridPosition> _validGridPositions = new List<GridPosition>();

        for (int x = -_maxGridHorizontalDistance; x <= _maxGridHorizontalDistance; x++)
        {
            for (int z = -_maxGridHorizontalDistance; z <= _maxGridHorizontalDistance; z++)
            {
                for (int f = -_maxGridHorizontalDistance; f <= _maxGridHorizontalDistance; f++)
                {
                    GridPosition _offset = new GridPosition(x, z, f);
                    GridPosition _validGridPosition = _myGridPosition + _offset;

                    if (_validGridPosition == _myGridPosition)
                    {
                        continue;
                    }

                    int _testDistance = Mathf.Abs(x) + Mathf.Abs(z);

                    if (_testDistance > _maxGridHorizontalDistance)
                    {
                        continue;
                    }

                    if (!LevelGrid.Instance.IsValidGridPosition(_validGridPosition))
                    {
                        continue;
                    }

                    float _distance = Vector3.Distance(transform.position, LevelGrid.Instance.GetWorldPosition(_validGridPosition));

                    if (_distance < LevelGrid.Instance.GetCellSize() * _minGridDistance + 0.1)
                    {
                        continue;
                    }

                    if (!LevelGrid.Instance.HasAnyUnitOnThisGridPosition(_validGridPosition))
                    {
                        continue;
                    }

                    Unit _targetUnit = LevelGrid.Instance.GetUnitOnThisGridPosition(_validGridPosition);

                    if (_unit.IsEnemy() == _targetUnit.IsEnemy())
                    {
                        continue;
                    }

                    Vector3 _unitWorldPosition = LevelGrid.Instance.GetWorldPosition(_myGridPosition);
                    Vector3 _raycastOrigin = _unitWorldPosition + Vector3.up * GetShoulderTargetHeight();
                    Vector3 _shootDir = (_targetUnit.transform.position - _unitWorldPosition).normalized;
                    float _distanceBetweenTargets = (_targetUnit.transform.position - _unitWorldPosition).magnitude;

                    if (Physics.Raycast(_raycastOrigin, _shootDir, _distanceBetweenTargets, _obstaclesLayerMask))
                    {
                        continue;
                    }

                    _validGridPositions.Add(_validGridPosition);
                }
            }
        }

        return _validGridPositions;
    }

    public override List<GridPosition> GetFadedValidActionGridPositions()
    {
        List<GridPosition> _validGridPositions = new List<GridPosition>();
        GridPosition _myGridPosition = _unit.GetGridPosition();

        for (int x = -_maxGridHorizontalDistance; x <= _maxGridHorizontalDistance; x++)
        {
            for (int z = -_maxGridHorizontalDistance; z <= _maxGridHorizontalDistance; z++)
            {
                for (int f = -_maxGridHorizontalDistance; f <= _maxGridHorizontalDistance; f++)
                {
                    GridPosition _offset = new GridPosition(x, z, f);
                    GridPosition _validGridPosition = _myGridPosition + _offset;

                    if (_validGridPosition == _myGridPosition)
                    {
                        continue;
                    }

                    int _testDistance = Mathf.Abs(x) + Mathf.Abs(z);

                    if (_testDistance > _maxGridHorizontalDistance)
                    {
                        continue;
                    }

                    if (!LevelGrid.Instance.IsValidGridPosition(_validGridPosition))
                    {
                        continue;
                    }

                    float _distance = Vector3.Distance(transform.position, LevelGrid.Instance.GetWorldPosition(_validGridPosition));

                    if (_distance < LevelGrid.Instance.GetCellSize() * _minGridDistance + 0.1)
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
        _targetUnit = LevelGrid.Instance.GetUnitOnThisGridPosition(_gridPosition);
        _canShootBullet = true;
        _state = State.Aiming;
        _stateTimer = 1;
        ActionStart(_onComplete);
        //Debug.Log($"// Aiming");
    }

    public Unit GetTargetUnit()
    {
        return _targetUnit;
    }

    public float GetShoulderTargetHeight()
    {
        return _shoulderTarget.position.y;
    }

    //public Vector3 GetShoulderTargetPosition()
    //{
    //    return _shoulderTarget.position;
    //}

    public override bool HasFadedGridVisual()
    {
        return true;
    }

    public override EnemyAiAction GetEnemyAiAction(GridPosition _gridPosition)
    {
        Unit _targetUnit = LevelGrid.Instance.GetUnitOnThisGridPosition(_gridPosition);

        return new EnemyAiAction()
        {
            gridPosition = _gridPosition,
            actionValue = 100 + Mathf.RoundToInt((1 - _targetUnit.GetHealthNormalized()) * _actionValuePriority),
        };
    }

    public int GetTargetCountAtPosition(GridPosition _gridPosition)
    {
        return GetValidActionGridPositions(_gridPosition).Count;
    }

    private enum State
    {
        Aiming,
        Shooting,
        Cooloff,
    }
}
