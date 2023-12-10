using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem<TGridObject>
{
    private int _width = 0;
    private int _height = 0;
    private float _cellSize = 0f;
    private int _floor = 3;
    private float _floorHeight = 3f;
    private TGridObject[,] _gridObjects = null;

    public GridSystem(int _w, int _h, float _cSize, int _floor, float _floorHeight, Func<GridSystem<TGridObject>, GridPosition, TGridObject> _createGridObject)
    {
        _width = _w;
        _height = _h;
        _cellSize = _cSize;
        this._floor = _floor;
        this._floorHeight = _floorHeight;
        _gridObjects = new TGridObject[_width, _height];

        for (int x = 0; x < _width; x++)
        {
            for (int z = 0; z < _height; z++)
            {
                GridPosition _gridPosition = new GridPosition(x, z, this._floor);
                _gridObjects[x, z] = _createGridObject(this, _gridPosition);
                //Debug.DrawLine(GetWorldPosition(x, z), GetWorldPosition(x, z) + Vector3.right * 0.8f, Color.white, 1234);
                //Debug.DrawLine(GetWorldPosition(x, z), GetWorldPosition(x, z) + Vector3.forward * 0.8f, Color.white, 1234);
            }
        }
    }

    public Vector3 GetWorldPosition(GridPosition _gridPosition)
    {
        return new Vector3(_gridPosition.x, 0, _gridPosition.z) * _cellSize +
            new Vector3(0, _gridPosition.floor, 0) * _floorHeight;
    }

    public GridPosition GetGridPosition(Vector3 _worldPosition)
    {
        return new GridPosition(
            Mathf.RoundToInt(_worldPosition.x / _cellSize),
            Mathf.RoundToInt(_worldPosition.z / _cellSize),
            _floor);
    }

    public TGridObject GetGridObject(GridPosition _gridPosition)
    {
        //return _gridObjects[_gridPosition.x, _gridPosition.z];
        //if (IsValidGridPosition(_gridPosition))
        //{
        //    return _gridObjects[_gridPosition.x, _gridPosition.z];
        //}
        //else
        //{
        //    IsValidGridPosition(_gridPosition);
        //    return default;
        //}
        return IsValidGridPosition(_gridPosition) ? _gridObjects[_gridPosition.x, _gridPosition.z] : default;
    }

    public bool IsValidGridPosition(GridPosition _gridPosition)
    {
        return _gridPosition.x >= 0
            && _gridPosition.x < _width
            && _gridPosition.z >= 0
            && _gridPosition.z < _height
            && _gridPosition.floor == _floor;
    }

    public int GetWidth()
    {
        return _width;
    }

    public int GetHeight()
    {
        return _height;
    }

    public void CreateDebugObjects(GameObject _prefab, Transform _parent)
    {
        for (int x = 0; x < _width; x++)
        {
            for (int z = 0; z < _height; z++)
            {
                var _gridPosition = new GridPosition(x, z, _floor);
                var _instance = UnityEngine.Object.Instantiate(_prefab, GetWorldPosition(_gridPosition), Quaternion.identity, _parent);
                var _debugObject = _instance.GetComponent<GridDebugObject>();
                _debugObject.SetGridObject(GetGridObject(_gridPosition));
            }
        }
    }
}
