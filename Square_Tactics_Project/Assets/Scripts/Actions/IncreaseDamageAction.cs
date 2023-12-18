using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseDamageAction : BaseAction
{
    [Title("// Buff")]
    [SerializeField] float _timeToActivate = 1f;
    [SerializeField] float _timeToEnd = 1f;
    [SerializeField] ParticleSystem _particleSystem = null;
    [SerializeField] int _maxTurnCount = 2;
    [SerializeField, ReadOnly] int _currentTurnCount = 0;

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
        return $"Increase Damage";
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

        yield return new WaitForSeconds(_timeToActivate);

        _currentTurnCount = _maxTurnCount;
        CheckEffect();

        yield return new WaitForSeconds(_timeToEnd);

        ActionComplete();
    }

    private void CheckEffect()
    {
        if (_currentTurnCount > 0 && !_particleSystem.isPlaying)
        {
            _particleSystem.Play();
        }
        else if (_currentTurnCount <= 0 && _particleSystem.isPlaying)
        {
            _particleSystem.Stop();
        }
    }

    private void DecreaseTurnCount()
    {
        _currentTurnCount--;
        _currentTurnCount = Mathf.Clamp(_currentTurnCount, 0, _maxTurnCount);
        CheckEffect();
    }

    public override bool IsBuffActive()
    {
        return _currentTurnCount > 0;
    }
}
