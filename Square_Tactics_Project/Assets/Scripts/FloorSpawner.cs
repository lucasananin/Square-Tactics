using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSpawner : MonoBehaviour
{
    [SerializeField] bool _isEnable = true;
    //[SerializeField] int _maxLength = 5;
    [SerializeField] int _width = 12;
    [SerializeField] int _height = 16;
    [SerializeField] int _floorSize = 10;
    [SerializeField] GameObject _floorPrefab = null;
    [SerializeField] Transform _floorParent = null;

    private void Awake()
    {
        if (!_isEnable) return;

        //for (int x = -_maxLength; x <= _maxLength; x++)
        //{
        //    for (int z = -_maxLength; z <= _maxLength; z++)
        //    {
        //        Vector3 _pos = new Vector3(x * _floorSize, 0, z * _floorSize);
        //        var _instance = Instantiate(_floorPrefab, _pos, Quaternion.identity, transform);
        //        _instance.gameObject.SetActive(true);
        //    }
        //}

        for (int x = 0; x < _width; x++)
        {
            for (int z = 0; z < _height; z++)
            {
                Vector3 _pos = new Vector3(x * _floorSize, 0, z * _floorSize);
                //Quaternion _randomRotation = Quaternion.Euler(0, Random.Range(0, 3) * 90, 0);
                //var _instance = Instantiate(_floorPrefab, _pos, _randomRotation, transform);
                var _instance = Instantiate(_floorPrefab, _pos, Quaternion.identity, _floorParent);
                _instance.gameObject.SetActive(true);
            }
        }

        _floorPrefab.SetActive(false);
    }
}
