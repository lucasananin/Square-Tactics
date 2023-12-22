using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnSelection : MonoBehaviour
{
    [SerializeField, ReadOnly] bool _canCheck = false;
    [SerializeField, ReadOnly] int _currentUnitIndex = 0;
    [SerializeField, ReadOnly] float _timer = 0;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(1);
        _canCheck = true;
    }

    private void OnEnable()
    {
        TurnSystem.Instance.onTurnChanged += ResetValues;
    }

    private void OnDisable()
    {
        TurnSystem.Instance.onTurnChanged -= ResetValues;
    }

    private void Update()
    {
        if (!_canCheck) return;
        if (!TurnSystem.Instance.IsPlayerTurn()) return;
        if (UnitActionSystem.Instance.CanSelectUnitByInput) return;

        _timer -= Time.deltaTime;

        if (_timer > 0) return;

        List<Unit> _units = UnitManager.Instance.GetFriendlyUnitList();
        UnitActionSystem.Instance.SelectUnit(_units[_currentUnitIndex]);

        if (UnitActionSystem.Instance.GetSelectedUnit().GetActionPoints() <= 0 && !UnitActionSystem.Instance.IsBusy())
        {
            _currentUnitIndex++;
            _timer = 1f;

            if (_currentUnitIndex >= _units.Count)
            {
                ResetValues();
                TurnSystem.Instance.NextTurn();
            }
        }
    }

    private void ResetValues()
    {
        _timer = 1f;
        _currentUnitIndex = 0;
        UnitActionSystem.Instance.SelectUnit(null);
        UnitActionSystem.Instance.SetSelectedAction(null);
    }
}
