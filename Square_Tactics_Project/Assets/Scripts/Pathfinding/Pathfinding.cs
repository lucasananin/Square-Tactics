using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : Singleton<Pathfinding>
{
    [SerializeField] GameObject _gridDebugPrefab = null;
    [SerializeField] Transform _pathfindingLinkParent = null;
    [SerializeField] LayerMask _obstaclesLayerMask = default;
    [SerializeField] LayerMask _mouseLayerMask = default;

    private List<GridSystem<PathNode>> _gridSystemList = null;
    private List<PathfindingLink> _pathfindingLinks = null;
    private int _width = 0;
    private int _height = 0;
    private float _cellSize = 0;
    private int _floorAmount = 3;

    private const int MOVE_STRAIGHT_COST = 10;
    private const int MOVE_DIAGONAL_COST = 14;

    public void Setup(int _width, int _height, float _cellSize, int _floorAmount)
    {
        this._width = _width;
        this._height = _height;
        this._cellSize = _cellSize;
        this._floorAmount = _floorAmount;

        _gridSystemList = new List<GridSystem<PathNode>>();

        for (int f = 0; f < _floorAmount; f++)
        {
            var _gridSystem = new GridSystem<PathNode>(_width, _height, _cellSize, f, LevelGrid.FLOOR_HEIGHT, (GridSystem<PathNode> _g, GridPosition _gridPosition) => new PathNode(_gridPosition));
            this._gridSystemList.Add(_gridSystem);
        }

        //_gridSystem.CreateDebugObjects(_gridDebugPrefab, transform);

        for (int x = 0; x < _width; x++)
        {
            for (int z = 0; z < _height; z++)
            {
                for (int f = 0; f < _floorAmount; f++)
                {
                    GridPosition _gridPosition = new GridPosition(x, z, f);
                    //Vector3 _worldPosition = LevelGrid.Instance.GetWorldPosition(_gridPosition);

                    //var _colliders = Physics.OverlapBox(_worldPosition, Vector3.one * (_cellSize * 0.25f), Quaternion.identity, _obstaclesLayerMask);
                    Collider[] _colliders = GetObstaclesOnGridPosition(_gridPosition);

                    if (_colliders.Length > 0)
                    {
                        GetNode(x, z, f).SetIsWalkable(false);
                    }

                    Collider[] _floors = GetFloorsOnGridPosition(_gridPosition);

                    if (_floors.Length <= 0)
                    {
                        GetNode(x, z, f).SetIsWalkable(false);
                    }
                }
            }
        }

        _pathfindingLinks = new List<PathfindingLink>();

        foreach (Transform _pathfindingLinkTransform in _pathfindingLinkParent)
        {
            if (_pathfindingLinkTransform.TryGetComponent(out PathfindingLinkBehaviour _pathfindingLinkBehaviour))
            {
                _pathfindingLinks.Add(_pathfindingLinkBehaviour.GetPathfindingLink());
            }
        }

        //_pathfindingLinks.Add(new PathfindingLink 
        //{ 
        //    gridPositionA = new GridPosition(0, 4, 0),
        //    gridPositionB = new GridPosition(0, 5, 1),
        //});
    }

    public Collider[] GetObstaclesOnGridPosition(GridPosition _gridPosition)
    {
        Vector3 _worldPosition = LevelGrid.Instance.GetWorldPosition(_gridPosition);
        return Physics.OverlapBox(_worldPosition, Vector3.one * (_cellSize * 0.25f), Quaternion.identity, _obstaclesLayerMask);
    }

    public Collider[] GetFloorsOnGridPosition(GridPosition _gridPosition)
    {
        Vector3 _worldPosition = LevelGrid.Instance.GetWorldPosition(_gridPosition);
        return Physics.OverlapBox(_worldPosition, Vector3.one * (_cellSize * 0.25f), Quaternion.identity, _mouseLayerMask);
    }

    public List<GridPosition> FindPath(GridPosition _startGridPosition, GridPosition _endGridPosition, out int _pathLength)
    {
        for (int x = 0; x < _width; x++)
        {
            for (int z = 0; z < _height; z++)
            {
                for (int f = 0; f < _floorAmount; f++)
                {
                    GridPosition _gridPosition = new GridPosition(x, z, f);
                    PathNode _pathNode = GetGridSystem(f).GetGridObject(_gridPosition);

                    _pathNode.SetGCost(int.MaxValue);
                    _pathNode.SetHCost(0);
                    _pathNode.CalculateFCost();
                    _pathNode.ResetCameFromPathNode();
                }
            }
        }

        List<PathNode> _openList = new List<PathNode>();
        List<PathNode> _closedList = new List<PathNode>();

        PathNode _startNode = GetGridSystem(_startGridPosition.floor).GetGridObject(_startGridPosition);
        PathNode _endNode = GetGridSystem(_endGridPosition.floor).GetGridObject(_endGridPosition);
        _openList.Add(_startNode);

        _startNode.SetGCost(0);
        _startNode.SetHCost(CalculateDistance(_startGridPosition, _endGridPosition));
        _startNode.CalculateFCost();

        while (_openList.Count > 0)
        {
            PathNode _currentNode = GetLowestFCostPathNode(_openList);

            if (_currentNode == _endNode)
            {
                // Reached final node.
                _pathLength = _endNode.GetFCost();
                return CalculatePath(_endNode);
            }

            _openList.Remove(_currentNode);
            _closedList.Add(_currentNode);

            foreach (var _neighbourNode in GetNeighbourList(_currentNode))
            {
                if (_closedList.Contains(_neighbourNode)) continue;

                if (LevelGrid.Instance.HasAnyUnitOnThisGridPosition(_neighbourNode.GetGridPosition()))
                {
                    _closedList.Add(_neighbourNode);
                    continue;
                }

                if (!_neighbourNode.IsWalkable())
                {
                    _closedList.Add(_neighbourNode);
                    continue;
                }

                int _tentativeGCost = _currentNode.GetGCost() + CalculateDistance(_currentNode.GetGridPosition(), _neighbourNode.GetGridPosition());

                if (_tentativeGCost < _neighbourNode.GetGCost())
                {
                    _neighbourNode.SetCameFromPathNode(_currentNode);
                    _neighbourNode.SetGCost(_tentativeGCost);
                    _neighbourNode.SetHCost(CalculateDistance(_neighbourNode.GetGridPosition(), _endGridPosition));
                    _neighbourNode.CalculateFCost();

                    if (!_openList.Contains(_neighbourNode))
                    {
                        _openList.Add(_neighbourNode);
                    }
                }
            }
        }

        _pathLength = 0;
        return null;
    }

    private List<GridPosition> CalculatePath(PathNode _endNode)
    {
        List<PathNode> _pathNodeList = new List<PathNode>();
        _pathNodeList.Add(_endNode);
        PathNode _currentNode = _endNode;

        while (_currentNode.GetCameFromPathNode() != null)
        {
            _pathNodeList.Add(_currentNode.GetCameFromPathNode());
            _currentNode = _currentNode.GetCameFromPathNode();
        }

        _pathNodeList.Reverse();

        List<GridPosition> _gridPositionList = new List<GridPosition>();

        foreach (var _pathNode in _pathNodeList)
        {
            _gridPositionList.Add(_pathNode.GetGridPosition());
        }

        return _gridPositionList;
    }

    public int CalculateDistance(GridPosition _gridPositionA, GridPosition _gridPositionB)
    {
        //GridPosition _gridPositionDistance = _gridPositionA - _gridPositionB;
        //int _distance = Mathf.Abs(_gridPositionDistance.x) + Mathf.Abs(_gridPositionDistance.z);
        //return _distance * MOVE_STRAIGHT_COST;

        GridPosition _gridPositionDistance = _gridPositionA - _gridPositionB;
        int _xDistance = Mathf.Abs(_gridPositionDistance.x);
        int _zDistance = Mathf.Abs(_gridPositionDistance.z);
        int _remaining = Mathf.Abs(_xDistance - _zDistance);
        return MOVE_DIAGONAL_COST * Mathf.Min(_xDistance, _zDistance) + MOVE_STRAIGHT_COST * _remaining;
    }

    private PathNode GetLowestFCostPathNode(List<PathNode> _pathNodeList)
    {
        PathNode _lowestFCostPathNode = _pathNodeList[0];

        for (int i = 0; i < _pathNodeList.Count; i++)
        {
            if (_pathNodeList[i].GetFCost() < _lowestFCostPathNode.GetFCost())
            {
                _lowestFCostPathNode = _pathNodeList[i];
            }
        }

        return _lowestFCostPathNode;
    }

    private List<PathNode> GetNeighbourList(PathNode _currentNode)
    {
        List<PathNode> _neighbourList = new List<PathNode>();

        GridPosition _gridPosition = _currentNode.GetGridPosition();

        // Up.
        _neighbourList.Add(GetNode(_gridPosition.x + 0, _gridPosition.z + 1, _gridPosition.floor));
        // Up Right.
        //_neighbourList.Add(GetNode(_gridPosition.x + 1, _gridPosition.z + 1));
        // Right.
        _neighbourList.Add(GetNode(_gridPosition.x + 1, _gridPosition.z + 0, _gridPosition.floor));
        // Right Down.
        //_neighbourList.Add(GetNode(_gridPosition.x + 1, _gridPosition.z - 1));
        // Down.
        _neighbourList.Add(GetNode(_gridPosition.x + 0, _gridPosition.z - 1, _gridPosition.floor));
        // Down Left.
        //_neighbourList.Add(GetNode(_gridPosition.x - 1, _gridPosition.z - 1));
        // Left.
        _neighbourList.Add(GetNode(_gridPosition.x - 1, _gridPosition.z + 0, _gridPosition.floor));
        // Left Up.
        //_neighbourList.Add(GetNode(_gridPosition.x - 1, _gridPosition.z + 1));

        int _count = _neighbourList.Count;

        for (int i = _count - 1; i >= 0; i--)
        {
            if (_neighbourList[i] == null)
            {
                _neighbourList.RemoveAt(i);
            }
        }

        List<PathNode> _totalNeighbourList = new List<PathNode>();
        _totalNeighbourList.AddRange(_neighbourList);

        List<GridPosition> _pathfindingLinkGridPositionList = GetPathfindingLinkConnectedGridPositions(_gridPosition);
        _count = _pathfindingLinkGridPositionList.Count;

        for (int i = 0; i < _count; i++)
        {
            _totalNeighbourList.Add(GetNode(_pathfindingLinkGridPositionList[i]));
        }

        //_count = _neighbourList.Count;

        //for (int i = 0; i < _count; i++)
        //{
        //    PathNode _pathNode = _neighbourList[i];
        //    GridPosition _neighbourGridPosition = _pathNode.GetGridPosition();

        //    if (_neighbourGridPosition.floor - 1 >= 0)
        //    {
        //        _totalNeighbourList.Add(GetNode(_neighbourGridPosition.x, _neighbourGridPosition.z, _neighbourGridPosition.floor - 1));
        //    }

        //    if (_neighbourGridPosition.floor + 1 < _floorAmount)
        //    {
        //        _totalNeighbourList.Add(GetNode(_neighbourGridPosition.x, _neighbourGridPosition.z, _neighbourGridPosition.floor + 1));
        //    }
        //}

        return _totalNeighbourList;
    }

    private List<GridPosition> GetPathfindingLinkConnectedGridPositions(GridPosition _gridPosition)
    {
        List<GridPosition> _gridPositions = new List<GridPosition>();
        int _count = _pathfindingLinks.Count;

        for (int i = 0; i < _count; i++)
        {
            PathfindingLink _pathfindingLink = _pathfindingLinks[i];

            if (_pathfindingLink.gridPositionA == _gridPosition)
            {
                _gridPositions.Add(_pathfindingLink.gridPositionB);
            }

            if (_pathfindingLink.gridPositionB == _gridPosition)
            {
                _gridPositions.Add(_pathfindingLink.gridPositionA);
            }
        }

        return _gridPositions;
    }

    private PathNode GetNode(int _x, int _z, int _floor)
    {
        return GetGridSystem(_floor).GetGridObject(new GridPosition(_x, _z, _floor));
    }

    private PathNode GetNode(GridPosition _gridPosition)
    {
        return GetGridSystem(_gridPosition.floor).GetGridObject(_gridPosition);
    }

    public void SetIsWalkableGridPosition(GridPosition _gridPosition, bool _isWalkable)
    {
        GetGridSystem(_gridPosition.floor).GetGridObject(_gridPosition).SetIsWalkable(_isWalkable);
    }

    public bool IsWalkableGridPosition(GridPosition _gridPosition)
    {
        PathNode _gridObject = GetGridSystem(_gridPosition.floor).GetGridObject(_gridPosition);
        return _gridObject.IsWalkable();
    }

    public bool CanTakePath(GridPosition _startGridPosition, GridPosition _endGridPosition, int _maxDistance)
    {
        List<GridPosition> _pathList = FindPath(_startGridPosition, _endGridPosition, out int _pathLength);
        bool _hasPath = _pathList != null;
        bool _hasLength = _pathLength <= _maxDistance * MOVE_STRAIGHT_COST;
        return _hasPath && _hasLength;
    }

    private GridSystem<PathNode> GetGridSystem(int _floor)
    {
        return _gridSystemList[_floor];
    }

    //public bool HasPath(GridPosition _startGridPosition, GridPosition _endGridPosition)
    //{
    //    return FindPath(_startGridPosition, _endGridPosition, out int _pathLength) != null;
    //}

    //public int GetPathLength(GridPosition _startGridPosition, GridPosition _endGridPosition)
    //{
    //    FindPath(_startGridPosition, _endGridPosition, out int _pathLength);
    //    return _pathLength;
    //}

    //[Button]
    //private void Debug_GetGridPosition()
    //{
    //    var _a = _gridSystem.GetGridObject(new GridPosition(-1, -1));
    //    Debug.Log($"// {_a}");
    //}
}
