using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingUpdater : MonoBehaviour
{
    private void OnEnable()
    {
        HealthSystem.onAnyDead += HealthSystem_onAnyDead;
    }

    private void OnDisable()
    {
        HealthSystem.onAnyDead -= HealthSystem_onAnyDead;
    }

    private void HealthSystem_onAnyDead(object _sender, EventArgs _e)
    {
        HealthSystem _senderTransform = _sender as HealthSystem;
        GridPosition _myGridPosition = LevelGrid.Instance.GetGridPosition(_senderTransform.transform.position);
        Pathfinding.Instance.SetIsWalkableGridPosition(_myGridPosition, true);
    }
}
