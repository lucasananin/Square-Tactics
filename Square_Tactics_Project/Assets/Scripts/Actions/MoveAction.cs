using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class MoveAction : BaseAction
{
    [Title("// Move")]
    [SerializeField] float _rotationSpeed = 15f;
    [SerializeField, Range(0.01f, 0.5f)] float _stoppingDistance = 0.1f;
    [SerializeField, ReadOnly] int _currentPositionIndex = 0;
    [SerializeField, ReadOnly] List<Vector3> _positionsList = null;

    [Title("// Velocity")]
    [SerializeField] float _velocityMultiplier = 10f;
    [SerializeField, ReadOnly] Vector3 _velocity = default;
    [SerializeField, ReadOnly] Vector3 _targetVelocity = default;

    [Title("// Jump")]
    [SerializeField] float _jumpForce = 1f;
    [SerializeField] float _jumpDuration = 1f;
    [SerializeField, ReadOnly] bool _isChangingFloors = false;
    [SerializeField, ReadOnly] float _lastYPosition = 0;

    public const float COLLIDER_SKIN_WIDTH = 0.08f;
    public const int NUM_OF_JUMPS = 1;

    private void Update()
    {
        CalculateVelocity();

        if (!_isActive)
        {
            ResetTargetVelocity();
            return;
        }

        if (_isChangingFloors)
        {
            CheckIfMovingUpOrDown();
            return;
        }

        Vector3 _myPosition = transform.position;
        Vector3 _targetPosition = _positionsList[_currentPositionIndex];
        Vector3 _moveDirection = (_targetPosition - _myPosition).normalized;
        Vector3 _movementVelocity = _moveDirection * 5f * Time.deltaTime;
        float _distance = (_myPosition - _targetPosition).sqrMagnitude;

        if (_distance < _stoppingDistance * _stoppingDistance)
        {
            _currentPositionIndex++;

            if (_currentPositionIndex >= _positionsList.Count)
            {
                ActionComplete();
            }
            else
            {
                _targetPosition = _positionsList[_currentPositionIndex];
                GridPosition _targetGridPosition = LevelGrid.Instance.GetGridPosition(_targetPosition);
                GridPosition _myGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);

                if (_myGridPosition.floor < _targetGridPosition.floor)
                {
                    JumpUp(_targetPosition);
                }
                else if (_myGridPosition.floor > _targetGridPosition.floor)
                {
                    JumpDown(_targetPosition);
                }
                else
                {
                    return;
                }

                _moveDirection = (_targetPosition - _myPosition).normalized;
                RotateToDirection(_moveDirection, false);
                ResetVelocity();
                SetTargetVelocity(Vector3.up);
            }
        }
        else
        {
            transform.position += _movementVelocity;
            SetTargetVelocity(Vector3.right);
            RotateToDirection(_moveDirection);
        }

        _lastYPosition = transform.position.y;
    }

    private void RotateToDirection(Vector3 _moveDirection, bool _interpolate = true)
    {
        _moveDirection.y = 0;
        Quaternion _targetRotation = Quaternion.LookRotation(_moveDirection, Vector3.up);
        transform.rotation = _interpolate ? Quaternion.Slerp(transform.rotation, _targetRotation, _rotationSpeed * Time.deltaTime) : _targetRotation;
    }

    private void JumpUp(Vector3 _targetPosition)
    {
        JumpTo(_targetPosition);
    }

    private void JumpDown(Vector3 _targetPosition)
    {
        JumpTo(_targetPosition, 2f);
    }

    private void JumpTo(Vector3 _targetPosition, float _forceMultiplier = 1)
    {
        _isChangingFloors = true;
        _targetPosition += Vector3.up * COLLIDER_SKIN_WIDTH;

        transform.DOJump(_targetPosition, _jumpForce * _forceMultiplier, NUM_OF_JUMPS, _jumpDuration).
            SetEase(Ease.Linear).
            OnComplete(() =>
            {
                _isChangingFloors = false;
            });
    }

    private void CheckIfMovingUpOrDown()
    {
        float _currentYPosition = transform.position.y;

        if (_currentYPosition < _lastYPosition)
        {
            SetTargetVelocity(Vector3.zero);
        }
        else if (_currentYPosition > _lastYPosition)
        {
            SetTargetVelocity(Vector3.up);
        }

        _lastYPosition = _currentYPosition;
    }

    private void CalculateVelocity()
    {
        float _step = _velocityMultiplier * Time.deltaTime;
        _velocity = Vector3.MoveTowards(_velocity, _targetVelocity, _step);
    }

    private void SetTargetVelocity(Vector3 _value)
    {
        _targetVelocity = _value;
    }

    private void ResetVelocity()
    {
        _velocity = Vector3.zero;
    }

    private void ResetTargetVelocity()
    {
        _targetVelocity = Vector3.zero;
    }

    public Vector3 GetVelocity()
    {
        return _velocity;
    }

    public bool IsChangingFloors()
    {
        return _isChangingFloors;
    }

    public override void TakeAction(GridPosition _gridPosition, Action _onComplete)
    {
        _positionsList.Clear();

        List<GridPosition> _pathGridPositions = Pathfinding.Instance.FindPath(_unit.GetGridPosition(), _gridPosition, out int _pathLength);
        int _count = _pathGridPositions.Count;

        for (int i = 0; i < _count; i++)
        {
            Vector3 _position = LevelGrid.Instance.GetWorldPosition(_pathGridPositions[i]);
            _positionsList.Add(_position);
        }

        _currentPositionIndex = 0;
        ActionStart(_onComplete);
    }

    public override List<GridPosition> GetValidActionGridPositions()
    {
        var _validGridPositions = new List<GridPosition>();
        var _myGridPosition = _unit.GetGridPosition();

        for (int x = -_maxGridHorizontalDistance; x <= _maxGridHorizontalDistance; x++)
        {
            for (int z = -_maxGridHorizontalDistance; z <= _maxGridHorizontalDistance; z++)
            {
                for (int f = -_maxGridHorizontalDistance; f <= _maxGridHorizontalDistance; f++)
                {
                    GridPosition _offset = new GridPosition(x, z, f);
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

                    if (LevelGrid.Instance.HasAnyUnitOnThisGridPosition(_validGridPosition))
                    {
                        continue;
                    }

                    if (!Pathfinding.Instance.IsWalkableGridPosition(_validGridPosition))
                    {
                        continue;
                    }

                    // Vai poder cruzar a posicao de uma Unit apenas se for do mesmo time.
                    if (!Pathfinding.Instance.CanTakePath(_myGridPosition, _validGridPosition, _maxGridHorizontalDistance))
                    {
                        continue;
                    }

                    //if (!Pathfinding.Instance.HasPath(_myGridPosition, _validGridPosition))
                    //{
                    //    continue;
                    //}

                    //int _moveStraightCost = 10;

                    //if (Pathfinding.Instance.GetPathLength(_myGridPosition, _validGridPosition) > _maxMoveDistance * _moveStraightCost)
                    //{
                    //    continue;
                    //}

                    _validGridPositions.Add(_validGridPosition);
                }
            }
        }

        return _validGridPositions;
    }

    public override string GetActionName()
    {
        return "Move";
    }

    public override EnemyAiAction GetEnemyAiAction(GridPosition _gridPosition)
    {
        bool _isShootAction = _unit.GetAction<ShootAction>() != null;

        if (_isShootAction)
        {
            var _shootAction = _unit.GetAction<ShootAction>();
            int _targetCountAtGridPosition = _shootAction.GetTargetCountAtPosition(_gridPosition);
            //_targetCountAtGridPosition = Mathf.Clamp(_targetCountAtGridPosition, 0, 1);

            var _gridWorldPosition = LevelGrid.Instance.GetWorldPosition(_gridPosition);
            var _nearestUnit = TransformMethods.GetNearest(_gridWorldPosition, UnitManager.Instance.GetFriendlyUnitList().ToArray());
            float _distanceToNearest = Vector3.Distance(_gridWorldPosition, _nearestUnit.position);
            int _distanceMultiplier = Mathf.RoundToInt(_distanceToNearest);

            float _distanceToNeighbour = Vector3.Distance(transform.position, _nearestUnit.position);
            int _neighbourDistance = _distanceToNeighbour < LevelGrid.Instance.GetCellSize() * _shootAction.MinGridDistance + 0.1 ? 100 : 1;
            //Debug.Log($"// {name} => _nearestUnit: {_nearestUnit.name} _m: {_neighbourDistance}");

            return new EnemyAiAction()
            {
                gridPosition = _gridPosition,
                actionValue = _targetCountAtGridPosition * _distanceMultiplier * _neighbourDistance,
            };
        }
        else
        {
            var _gridWorldPosition = LevelGrid.Instance.GetWorldPosition(_gridPosition);
            var _nearestUnit = TransformMethods.GetNearest(_gridWorldPosition, UnitManager.Instance.GetFriendlyUnitList().ToArray());
            float _distanceToNearest = Vector3.Distance(_gridWorldPosition, _nearestUnit.position) * 10;
            int _distanceMultiplier = Mathf.RoundToInt(10 - _distanceToNearest);

            return new EnemyAiAction()
            {
                gridPosition = _gridPosition,
                actionValue = _distanceMultiplier * 10,
            };
        }
    }
}
