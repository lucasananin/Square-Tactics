using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    //[SerializeField] GameObject _gridDebugPrefab = null;

    //private GridSystem _gridSystem = null;

    //private void Start()
    //{
    //    _gridSystem = new GridSystem(12, 8, 2f);
    //    _gridSystem.CreateDebugObjects(_gridDebugPrefab);
    //}

    //private void Update()
    //{
    //    //Debug.Log($"{_gridSystem.GetGridPosition(MouseWorld.Instance.GetHitPoint())}");

    //    if (Input.GetKeyDown(KeyCode.Alpha1))
    //    {
    //        GridPosition _mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.Instance.GetHitPoint());
    //        GridPosition _startGridPosition = new GridPosition(0, 0);

    //        var _gridPositionList = Pathfinding.Instance.FindPath(_startGridPosition, _mouseGridPosition);

    //        for (int i = 0; i < _gridPositionList.Count - 1; i++)
    //        {
    //            Debug.DrawLine(
    //                LevelGrid.Instance.GetWorldPosition(_gridPositionList[i]),
    //                LevelGrid.Instance.GetWorldPosition(_gridPositionList[i + 1]),
    //                Color.red,
    //                10f);
    //        }
    //    }
    //}

    //void OnDrawGizmos()
    //{
    //    for (int x = 0; x < 12; x++)
    //    {
    //        for (int z = 0; z < 8; z++)
    //        {
    //            Gizmos.color = Color.yellow;
    //            Gizmos.DrawCube(_gridSystem.GetWorldPosition(x, z), new Vector3(2f * 0.9f, 0.5f, 2f * 0.5f));
    //        }
    //    }
    //}
}
