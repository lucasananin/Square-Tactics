using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Audio;

public class HealAction : BaseAction
{
    [Title("// Heal")]
    [SerializeField] float _timeToHeal = 1f;
    [SerializeField] float _timeToEnd = 1f;
    [SerializeField] int _healValue = 1;
    [SerializeField] LayerMask _hitLayers = default;
    [SerializeField] ParticleSystem _healVfxPrefab = null;

    [Title("// Audio")]
    [SerializeField] AudioDataSO _healAudio = null;

    public static event EventHandler onAnyHit = null;

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
        GetComponent<UnitAnimator>().TriggerSpecialAttack();

        yield return new WaitForSeconds(_timeToHeal);

        Vector3 _halfExtents = Vector3.one * LevelGrid.Instance.GetCellSize() * _maxGridHorizontalDistance * 0.5f;
        Collider[] _collidersHit = Physics.OverlapBox(transform.position, _halfExtents, Quaternion.identity, _hitLayers);
        int _count = _collidersHit.Length;

        for (int i = 0; i < _count; i++)
        {
            if (_collidersHit[i].TryGetComponent(out HealthSystem _healthSystem))
            {
                //if (_unit.IsMyHealthSystem(_healthSystem)) continue;
                if (_healthSystem.GetComponent<Unit>().IsEnemy())
                {
                    _healthSystem.TakeDamage(_healValue);
                }
                else
                {
                    _healthSystem.RecoverHealth(_healValue);
                }

                Instantiate(_healVfxPrefab, _collidersHit[i].transform.position, Quaternion.identity);
            }
        }

        if (_count > 0)
        {
            _healAudio?.PlayAsSfx();
            onAnyHit?.Invoke(this, EventArgs.Empty);
        }

        yield return new WaitForSeconds(_timeToEnd);

        ActionComplete();
    }
}
