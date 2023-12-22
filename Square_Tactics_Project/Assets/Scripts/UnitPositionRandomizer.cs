using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPositionRandomizer : MonoBehaviour
{
    [SerializeField] List<Unit> _playerUnits = null;
    [SerializeField] List<Unit> _enemyUnits = null;
    [SerializeField] int _obstaclesCount = 8;
    [SerializeField] GameObject _obstaclePrefab = null;
    [SerializeField, ReadOnly] List<Vector3> _positionsTaken = null;

    public void SetUnitPositions()
    {
        int _count = _playerUnits.Count;
        Vector3 _position;

        for (int i = 0; i < _count; i++)
        {
            do
            {
                int _x = Random.Range(0, LevelGrid.Instance.GetWidth());
                int _z = Random.Range(0, LevelGrid.Instance.GetHeight() / 2);
                _position = new Vector3(_x, 0, _z) * LevelGrid.Instance.GetCellSize();

            } while (_positionsTaken.Contains(_position));

            _playerUnits[i].transform.position = _position;
            _positionsTaken.Add(_position);
        }

        _count = _enemyUnits.Count;

        for (int i = 0; i < _count; i++)
        {
            do
            {
                int _x = Random.Range(0, LevelGrid.Instance.GetWidth());
                int _z = Random.Range(LevelGrid.Instance.GetHeight() / 2, LevelGrid.Instance.GetHeight());
                _position = new Vector3(_x, 0, _z) * LevelGrid.Instance.GetCellSize();

            } while (_positionsTaken.Contains(_position));

            _enemyUnits[i].transform.position = _position;
            _positionsTaken.Add(_position);
        }

        _count = _obstaclesCount;

        for (int i = 0; i < _count; i++)
        {
            do
            {
                int _x = Random.Range(0, LevelGrid.Instance.GetWidth());
                int _z = Random.Range(0, LevelGrid.Instance.GetHeight());
                _position = new Vector3(_x, 0, _z) * LevelGrid.Instance.GetCellSize();

            } while (_positionsTaken.Contains(_position));

            Instantiate(_obstaclePrefab, _position, Quaternion.identity, transform);
            _positionsTaken.Add(_position);
        }

        _obstaclePrefab.SetActive(false);
    }
}
