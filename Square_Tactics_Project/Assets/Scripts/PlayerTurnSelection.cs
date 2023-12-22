using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnSelection : MonoBehaviour
{
    [SerializeField] bool _canCheck = false;
    [SerializeField] int _currentUnitIndex = 0;
    [SerializeField] float _timer = 0;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(1);
        _canCheck = true;
    }

    private void Update()
    {
        if (!_canCheck) return;
        if (!TurnSystem.Instance.IsPlayerTurn()) return;

        // seleciona a unit.
        // fica checando os actionpoints, se terminar. seleciona outra.
        // se o index for maior que a lista, troca de turno.

        _timer -= Time.deltaTime;

        if (_timer > 0) return;

        var _units = UnitManager.Instance.GetFriendlyUnitList();
        UnitActionSystem.Instance.SelectUnit(_units[_currentUnitIndex]);

        if (UnitActionSystem.Instance.GetSelectedUnit().GetActionPoints() <= 0 && !UnitActionSystem.Instance.IsBusy())
        {
            _currentUnitIndex++;
            _timer = 1f;

            if (_currentUnitIndex >= _units.Count)
            {
                _currentUnitIndex = 0;
                UnitActionSystem.Instance.SetSelectedAction(null);
                TurnSystem.Instance.NextTurn();
            }
        }
    }
}
