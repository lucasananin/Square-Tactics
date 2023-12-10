using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject
{
    private GridSystem<GridObject> _gridSystem = null;
    private GridPosition _gridPosition = default;
    private List<Unit> _units = null;

    public GridObject(GridSystem<GridObject> _gSystem, GridPosition _gPos)
    {
        _gridSystem = _gSystem;
        _gridPosition = _gPos;
        _units = new List<Unit>();
    }

    public void AddUnit(Unit _unit)
    {
        _units.Add(_unit);
    }

    public void RemoveUnit(Unit _unit)
    {
        _units.Remove(_unit);
    }

    public List<Unit> GetUnits()
    {
        return _units;
    }

    public bool HasAnyUnit()
    {
        return _units.Count > 0;
    }

    //public string GetInfoString()
    //{
    //    string _unitString = string.Empty;

    //    foreach (var _unit in _units)
    //    {
    //        _unitString += $"{_unit}\n";
    //    }

    //    return $"{_gridPosition.x}, {_gridPosition.z}\n{_unitString}";
    //}

    public Unit GetUnit()
    {
        return HasAnyUnit() ? _units[0] : null;
    }

    public override string ToString()
    {
        string _unitString = string.Empty;

        foreach (var _unit in _units)
        {
            _unitString += $"{_unit}\n";
        }

        return $"{_gridPosition.x}, {_gridPosition.z}\n{_unitString}";
        //return _gridPosition.ToString();
    }
}
