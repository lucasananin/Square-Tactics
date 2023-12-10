using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct GridPosition : IEquatable<GridPosition>
{
    public int x;
    public int z;
    public int floor;

    public GridPosition(int _xValue, int _zValue, int _floor)
    {
        x = _xValue;
        z = _zValue;
        floor = _floor;
    }

    public override string ToString()
    {
        return $"x: {x}; z: {z}; floor: {floor}";
    }

    public static bool operator ==(GridPosition _a, GridPosition _b)
    {
        return _a.x == _b.x && _a.z == _b.z && _a.floor == _b.floor;
    }

    public static bool operator !=(GridPosition _a, GridPosition _b)
    {
        return !(_a == _b);
    }

    public override bool Equals(object obj)
    {
        return obj is GridPosition position
            && x == position.x
            && z == position.z
            && floor == position.floor;
    }

    public bool Equals(GridPosition other)
    {
        return this == other;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(x, z, floor);
    }

    public static GridPosition operator +(GridPosition a, GridPosition b)
    {
        return new GridPosition(a.x + b.x, a.z + b.z, a.floor + b.floor);
    }

    public static GridPosition operator -(GridPosition a, GridPosition b)
    {
        return new GridPosition(a.x - b.x, a.z - b.z, a.floor - b.floor);
    }
}
