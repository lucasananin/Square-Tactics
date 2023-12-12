using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid : Singleton<LevelGrid>
{
    [SerializeField] GameObject _gridDebugPrefab = null;
    [SerializeField] int _width = 16;
    [SerializeField] int _height = 16;
    [SerializeField] float _cellSize = 2f;
    [Space]
    [SerializeField] int _floorAmount = 3;

    public const float FLOOR_HEIGHT = 3f;

    private List<GridSystem<GridObject>> _gridSystemList = null;

    public event System.Action onAnyUnitMovedGridPosition = null;

    protected override void Awake()
    {
        base.Awake();
        _gridSystemList = new List<GridSystem<GridObject>>();

        for (int i = 0; i < _floorAmount; i++)
        {
            var _gridSystem = new GridSystem<GridObject>(_width, _height, _cellSize, i, FLOOR_HEIGHT, (GridSystem<GridObject> _g, GridPosition _gridPosition) => new GridObject(_g, _gridPosition));
            this._gridSystemList.Add(_gridSystem);
        }

        //_gridSystemList[0].CreateDebugObjects(_gridDebugPrefab, transform);
    }

    private void Start()
    {
        Pathfinding.Instance.Setup(_width, _height, _cellSize, _floorAmount);
    }

    public void AddUnitAtGridPosition(GridPosition _gridPosition, Unit _unit)
    {
        var _gridoObject = GetGridSystem(_gridPosition.floor).GetGridObject(_gridPosition);
        _gridoObject.AddUnit(_unit);
    }

    public List<Unit> GetUnitsAtGridPosition(GridPosition _gridPosition)
    {
        var _gridObject = GetGridSystem(_gridPosition.floor).GetGridObject(_gridPosition);
        return _gridObject.GetUnits();
    }

    public void RemoveUnitAtGridPosition(GridPosition _gridPosition, Unit _unit)
    {
        var _gridoObject = GetGridSystem(_gridPosition.floor).GetGridObject(_gridPosition);
        _gridoObject.RemoveUnit(_unit);
    }

    public void UnitMovedGridPosition(Unit _unit, GridPosition _fromGridPosition, GridPosition _toGridPosition)
    {
        RemoveUnitAtGridPosition(_fromGridPosition, _unit);
        AddUnitAtGridPosition(_toGridPosition, _unit);
        onAnyUnitMovedGridPosition?.Invoke();
    }

    public Vector3 GetWorldPosition(GridPosition _gridPosition)
    {
        return GetGridSystem(_gridPosition.floor).GetWorldPosition(_gridPosition);
    }

    public GridPosition GetGridPosition(Vector3 _worldPosition)
    {
        int _floor = GetFloor(_worldPosition);
        return GetGridSystem(_floor).GetGridPosition(_worldPosition);
    }

    public bool IsValidGridPosition(GridPosition _gridPosition)
    {
        if (_gridPosition.floor < 0 || _gridPosition.floor >= _floorAmount) return false;

        return GetGridSystem(_gridPosition.floor).IsValidGridPosition(_gridPosition);
    }

    public int GetWidth()
    {
        return GetGridSystem(0).GetWidth();
    }

    public int GetHeight()
    {
        return GetGridSystem(0).GetHeight();
    }

    public int GetFloorAmount()
    {
        return _floorAmount;
    }

    public float GetCellSize()
    {
        return _cellSize;
    }

    public bool HasAnyUnitOnThisGridPosition(GridPosition _gridPosition)
    {
        var _gridObject = GetGridSystem(_gridPosition.floor).GetGridObject(_gridPosition);
        return _gridObject.HasAnyUnit();
    }

    public Unit GetUnitOnThisGridPosition(GridPosition _gridPosition)
    {
        var _gridObject = GetGridSystem(_gridPosition.floor).GetGridObject(_gridPosition);
        return _gridObject.GetUnit();
    }

    public int GetFloor(Vector3 _worldPosition)
    {
        return Mathf.FloorToInt(_worldPosition.y / _floorAmount);
    }

    private GridSystem<GridObject> GetGridSystem(int _floor)
    {
        return _gridSystemList[_floor];
    }
}
