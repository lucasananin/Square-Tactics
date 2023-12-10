using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PathfindingGridDebugObject : GridDebugObject
{
    [SerializeField] TextMeshPro _gCostText = null;
    [SerializeField] TextMeshPro _hCostText = null;
    [SerializeField] TextMeshPro _fCostText  = null;
    [SerializeField] SpriteRenderer _isWalkableSprite = null;

    private PathNode _pathNode = null;

    public override void SetGridObject(object _gObject)
    {
        base.SetGridObject(_gObject);
        _pathNode = _gObject as PathNode;
    }

    protected override void UpdateTexts()
    {
        base.UpdateTexts();
        _gCostText.text = _pathNode.GetGCost().ToString();
        _hCostText.text = _pathNode.GetHCost().ToString();
        _fCostText.text = _pathNode.GetFCost().ToString();
        _isWalkableSprite.color = _pathNode.IsWalkable() ? Color.green : Color.red;
    }
}
