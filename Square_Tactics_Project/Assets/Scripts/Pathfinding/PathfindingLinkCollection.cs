using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingLinkCollection : MonoBehaviour
{
    [SerializeField] PathfindingLinkBehaviour[] _pathfindingLinkBehaviours = null;

    public PathfindingLinkBehaviour[] PathfindingLinkBehaviours { get => _pathfindingLinkBehaviours; private set => _pathfindingLinkBehaviours = value; }

    [Button]
    private void PopulateCollection()
    {
        PathfindingLinkBehaviour[] _behaviours = GetComponentsInChildren<PathfindingLinkBehaviour>();
        _pathfindingLinkBehaviours = _behaviours;
    }
}
