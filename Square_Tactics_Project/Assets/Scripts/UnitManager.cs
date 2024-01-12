using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : Singleton<UnitManager>
{
    [SerializeField] List<Unit> _unitList = null;
    [SerializeField] List<Unit> _friendlyUnitList = null;
    [SerializeField] List<Unit> _enemyUnitList = null;

    public static event System.Action onAllEnemyUnitsDied = null;
    public static event System.Action onAllPlayerUnitsDied = null;

    private IEnumerator Start()
    {
        yield return null;
        Shuffle(_enemyUnitList);
        Shuffle(_friendlyUnitList);
    }

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

            if (_enemyUnitList.Count <= 0)
            {
                onAllEnemyUnitsDied?.Invoke();
            }
        }
        else
        {
            _friendlyUnitList.Remove(_unit);

            if (_friendlyUnitList.Count <= 0)
            {
                onAllPlayerUnitsDied?.Invoke();
            }
        }

        _unitList.Remove(_unit);
    }

    private void Shuffle(List<Unit> _list)
    {
        int _count = _list.Count;

        for (int i = 0; i < _count; i++)
        {
            var _tempUnit = _list[i];
            var _randomIndex = Random.Range(0, _count);
            _list[i] = _list[_randomIndex];
            _list[_randomIndex] = _tempUnit;
        }
    }

    public List<Unit> GetUnitList() => _unitList;
    public List<Unit> GetFriendlyUnitList() => _friendlyUnitList;
    public List<Unit> GetEnemyList() => _enemyUnitList;
}
