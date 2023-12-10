using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructablePathfindingUpdater : MonoBehaviour
{
    private void OnDestroy()
    {
        GridPosition _myGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        Pathfinding.Instance.SetIsWalkableGridPosition(_myGridPosition, true);
    }
}
