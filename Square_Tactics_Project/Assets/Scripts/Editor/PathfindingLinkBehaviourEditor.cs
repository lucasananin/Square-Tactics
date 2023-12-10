using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PathfindingLinkBehaviour))]
public class PathfindingLinkBehaviourEditor : Editor
{
    //private void OnSceneGUI()
    //{
    //    PathfindingLinkBehaviour _pathfindingLinkBehaviour = (PathfindingLinkBehaviour)target;

    //    EditorGUI.BeginChangeCheck();
    //    Vector3 _newLinkPositionA = Handles.PositionHandle(_pathfindingLinkBehaviour.LinkPositionA, Quaternion.identity);
    //    Vector3 _newLinkPositionB = Handles.PositionHandle(_pathfindingLinkBehaviour.LinkPositionB, Quaternion.identity);

    //    if (EditorGUI.EndChangeCheck())
    //    {
    //        Undo.RecordObject(_pathfindingLinkBehaviour, "Change Link Position");
    //        _pathfindingLinkBehaviour.LinkPositionA = _newLinkPositionA;
    //        _pathfindingLinkBehaviour.LinkPositionB = _newLinkPositionB;
    //    }
    //}
}
