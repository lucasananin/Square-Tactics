using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode
{
    private GridPosition _gridPosition = default;
    private int _gCost = 0;
    private int _hCost = 0;
    private int _fCost = 0;
    private PathNode _cameFromPathNode = null;
    private bool _isWalkable = true;

    public PathNode(GridPosition _gridPosition)
    {
        this._gridPosition = _gridPosition;
    }

    public void SetGCost(int _value)
    {
        _gCost = _value;
    }

    public void SetHCost(int _value)
    {
        _hCost = _value;
    }

    public void CalculateFCost()
    {
        _fCost = _gCost + _hCost;
    }

    public void SetCameFromPathNode(PathNode _value)
    {
        _cameFromPathNode = _value;
    }

    public PathNode GetCameFromPathNode()
    {
        return _cameFromPathNode;
    }

    public int GetGCost()
    {
        return _gCost;
    }

    public int GetHCost()
    {
        return _hCost;
    }

    public int GetFCost()
    {
        return _fCost;
    }

    public void ResetCameFromPathNode()
    {
        _cameFromPathNode = null;
    }

    public GridPosition GetGridPosition()
    {
        return _gridPosition;
    }

    public bool IsWalkable()
    {
        return _isWalkable;
    }

    public void SetIsWalkable(bool _value)
    {
        _isWalkable = _value;
    }

    //public string GetInfoString()
    //{
    //    return $"{_gridPosition}";
    //}

    public override string ToString()
    {
        return $"{_gridPosition.x}, {_gridPosition.z}";
        //return _gridPosition.ToString();
    }
}
