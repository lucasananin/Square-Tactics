using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAttackAction : BaseAction
{
    [Title("// Spin Attack")]
    [SerializeField] float _timeToAttack = 1f;
    [SerializeField] float _timeToEnd = 1f;
    [SerializeField] int _damage = 1;
    [SerializeField] LayerMask _hitLayers = default;

    public static event EventHandler onAnyHit = null;

    public override string GetActionName()
    {
        return "Spin Slash";
    }

    public override EnemyAiAction GetEnemyAiAction(GridPosition _gridPosition)
    {
        return new EnemyAiAction()
        {
            gridPosition = _gridPosition,
            actionValue = 10,
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

        yield return new WaitForSeconds(_timeToAttack);

        Vector3 _halfExtents = Vector3.one * LevelGrid.Instance.GetCellSize() * _maxGridHorizontalDistance;
        Collider[] _collidersHit = Physics.OverlapBox(transform.position, _halfExtents, Quaternion.identity, _hitLayers);
        int _count = _collidersHit.Length;

        for (int i = 0; i < _count; i++)
        {
            if (_collidersHit[i].TryGetComponent(out HealthSystem _healthSystem))
            {
                if (_unit.IsMyHealthSystem(_healthSystem)) continue;

                _healthSystem.TakeDamage(_damage  * GetDamageBuffMultiplier());
            }
        }

        if (_count > 0)
        {
            onAnyHit?.Invoke(this, EventArgs.Empty);
        }

        yield return new WaitForSeconds(_timeToEnd);

        ActionComplete();
    }
}
