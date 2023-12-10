using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Door : AbstractInteractable
{
    [Title("// Door")]
    [SerializeField] Transform _doorRight = null;
    [SerializeField] Transform _doorLeft = null;
    [SerializeField, ReadOnly] bool _isOpen = false;

    //private IEnumerator Start()
    //{
    //    yield return _interactionWaitTime;
    //    SetIsWalkable();
    //}

    public override void Interact(Action _onComplete)
    {
        _isOpen = !_isOpen;

        float _scaleX = _isOpen ? 0.1f : 1f;
        float _duration = _interactionTime * 0.8f;
        _doorRight.DOScaleX(_scaleX, _duration);
        _doorLeft.DOScaleX(_scaleX, _duration);
        //Vector3 _scale = new Vector3(_isOpen ? 0.1f : 1f, 1f, 1f);
        //_doorRight.localScale = _scale;
        //_doorLeft.localScale = _scale;

        GridPosition _myGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        Pathfinding.Instance.SetIsWalkableGridPosition(_myGridPosition, _isOpen);

        StartCoroutine(InteractRoutine(_onComplete));
    }

    //private void SetIsWalkable()
    //{
    //    var _myGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
    //    Pathfinding.Instance.SetIsWalkableGridPosition(_myGridPosition, _isOpen);
    //}
}
