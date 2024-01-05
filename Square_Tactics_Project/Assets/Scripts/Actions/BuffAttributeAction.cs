using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Audio;

public class BuffAttributeAction : BaseAction
{
    [Title("// Buff")]
    [SerializeField] float _timeToActivate = 1f;
    [SerializeField] float _timeToEnd = 1f;
    [SerializeField] int _maxTurnCount = 2;
    [SerializeField, ReadOnly] int _currentTurnCount = 0;
    [Space]
    //[SerializeField] bool _playOneTimeVfxOnTakeAction = true;
    [SerializeField] ParticleSystem _oneTimeVfx = null;
    [SerializeField] ParticleSystem _persistentVfx = null;

    [Title("// Audio")]
    [SerializeField] AudioDataSO _sfx_1 = null;
    [SerializeField] AudioDataSO _sfx_2 = null;

    private void OnEnable()
    {
        TurnSystem.Instance.onTurnChanged += DecreaseTurnCount;
    }

    private void OnDisable()
    {
        TurnSystem.Instance.onTurnChanged -= DecreaseTurnCount;
    }

    public override string GetActionName()
    {
        return _displayName;
    }

    public override EnemyAiAction GetEnemyAiAction(GridPosition _gridPosition)
    {
        return new EnemyAiAction()
        {
            gridPosition = _gridPosition,
            actionValue = 0,
        };
    }

    public override List<GridPosition> GetValidActionGridPositions()
    {
        GridPosition _myGridPosition = _unit.GetGridPosition();
        return new List<GridPosition>() { _myGridPosition };
    }

    public override void TakeAction(GridPosition _gridPosition, Action _onComplete)
    {
        ActionStart(_onComplete);
        StartCoroutine(TakeAction_routine());
    }

    public IEnumerator TakeAction_routine()
    {
        GetComponent<UnitAnimator>().TriggerBuffAbility();
        _persistentVfx.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        _oneTimeVfx.Play();
        _sfx_1?.PlayAsSfx();

        yield return new WaitForSeconds(_timeToActivate);

        _currentTurnCount = _maxTurnCount;
        _sfx_2?.PlayAsSfx();
        CheckPersistentFx();

        yield return new WaitForSeconds(_timeToEnd);

        ActionComplete();
    }

    private void CheckPersistentFx()
    {
        if (_persistentVfx == null) return;

        if (_currentTurnCount > 0 && !_persistentVfx.isPlaying)
        {
            _persistentVfx.Play();
        }
        else if (_currentTurnCount <= 0 && _persistentVfx.isPlaying)
        {
            _persistentVfx.Stop();
        }
    }

    [Button]
    private void DecreaseTurnCount()
    {
        _currentTurnCount--;
        _currentTurnCount = Mathf.Clamp(_currentTurnCount, 0, _maxTurnCount);
        CheckPersistentFx();
    }

    public override bool IsBuffActive()
    {
        return _currentTurnCount > 0;
    }
}
