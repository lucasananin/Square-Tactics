using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSpawner : MonoBehaviour
{
    [SerializeField] bool _isEnable = true;
    [SerializeField] int _maxLength = 10;
    [SerializeField] int _floorSize = 10;
    [SerializeField] GameObject _floorPrefab = null;

    private void Awake()
    {
        if (!_isEnable) return;

        for (int x = -_maxLength; x <= _maxLength; x++)
        {
            for (int z = -_maxLength; z <= _maxLength; z++)
            {
                Vector3 _pos = new Vector3(x * _floorSize, 0, z * _floorSize);
                var _instance = Instantiate(_floorPrefab, _pos, Quaternion.identity, transform);
                //_instance.gameObject.SetActive(true);
            }
        }

        _floorPrefab.SetActive(false);
    }
}
