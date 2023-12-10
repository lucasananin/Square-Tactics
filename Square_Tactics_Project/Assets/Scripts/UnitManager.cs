using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : Singleton<UnitManager>
{
    [SerializeField] List<Unit> _unitList = null;
    [SerializeField] List<Unit> _friendlyUnitList = null;
    [SerializeField] List<Unit> _enemyUnitList = null;

    private void OnEnable()
    {
        Unit.onAnyUnitSpawned += AddUnitToList;
        Unit.onAnyUnitDead += RemoveUnitFromList;
    }

    private void OnDisable()
    {
        Unit.onAnyUnitSpawned -= AddUnitToList;
        Unit.onAnyUnitDead -= RemoveUnitFromList;
    }

    private void AddUnitToList(object _sender, System.EventArgs _e)
    {
        Unit _unit = _sender as Unit;

        if (_unit.IsEnemy())
        {
            _enemyUnitList.Add(_unit);
        }
        else
        {
            _friendlyUnitList.Add(_unit);
        }

        _unitList.Add(_unit);
    }

    private void RemoveUnitFromList(object _sender, System.EventArgs _e)
    {
        Unit _unit = _sender as Unit;

        if (_unit.IsEnemy())
        {
            _enemyUnitList.Remove(_unit);
        }
        else
        {
            _friendlyUnitList.Remove(_unit);
        }

        _unitList.Remove(_unit);
    }

    public List<Unit> GetUnitList() => _unitList;
    public List<Unit> GetFriendlyUnitList() => _friendlyUnitList;
    public List<Unit> GetEnemyList() => _enemyUnitList;
}
