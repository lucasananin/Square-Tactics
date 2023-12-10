using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingLinkBehaviour : MonoBehaviour
{
    [SerializeField] Vector3 _linkPositionA = default;
    [SerializeField] Vector3 _linkPositionB = default;

    public Vector3 LinkPositionA { get => _linkPositionA; set => _linkPositionA = value; }
    public Vector3 LinkPositionB { get => _linkPositionB; set => _linkPositionB = value; }

    public PathfindingLink GetPathfindingLink()
    {
        return new PathfindingLink()
        {
            gridPositionA = LevelGrid.Instance.GetGridPosition(_linkPositionA),
            gridPositionB = LevelGrid.Instance.GetGridPosition(_linkPositionB),
        };
    }
}
