using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Pools
{
    [System.Serializable]
    public abstract class AbstractPool<T> : PoolRoot where T : MonoBehaviour
    {
        [Title("// General")]
        [SerializeField] protected T _prefab = default;
        [SerializeField, Range(1, 99)] protected int _initialSpawn = 4;
        [Space]
        [SerializeField] protected bool _enabledOnInstantiated = false;
        [SerializeField] protected bool _enabledOnGet = true;
        [SerializeField] protected bool _enabledOnReturn = false;

        [Title("// Lists")]
        [SerializeField] protected List<T> _inPoolList = new List<T>();
        [SerializeField] protected List<T> _inUseList = new List<T>();

        public AbstractPool(T _prefabValue, int _initialSpawnValue, Transform _parentValue = null)
        {
            _prefab = _prefabValue;
            _initialSpawn = _initialSpawnValue;

            _enabledOnInstantiated = false;
            _enabledOnGet = true;
            _enabledOnReturn = false;

            ClearCollections();
            PopulateList(_parentValue);
        }

        public override void PopulateList(Transform _parentValue = null)
        {
            PopulateList(_initialSpawn, _parentValue);
        }

        public override void PopulateList(int _count, Transform _parentValue = null)
        {
            for (int i = 0; i < _count; i++)
            {
                var _tempObject = Object.Instantiate(_prefab, _parentValue);
                _tempObject.gameObject.SetActive(_enabledOnInstantiated);
                _inPoolList.Add(_tempObject);
            }
        }

        public T GetFromPool(Transform _parentValue = null)
        {
            var _count = _inPoolList.Count;

            if (_count <= 0)
                PopulateList(1, _parentValue);

            var _tempObject = _inPoolList[0];
            _inPoolList.Remove(_tempObject);
            _inUseList.Add(_tempObject);
            _tempObject.gameObject.SetActive(_enabledOnGet);

            return _tempObject;
        }

        public void ReturnToPool(T _item)
        {
            _inUseList.Remove(_item);
            _inPoolList.Add(_item);
            _item.gameObject.SetActive(_enabledOnReturn);
        }

        public override void DisposePool()
        {
            var _count = _inPoolList.Count;

            for (int i = 0; i < _count; i++)
                if (_inPoolList[i] != null)
                    Object.Destroy(_inPoolList[i].gameObject);

            _count = _inUseList.Count;

            for (int i = 0; i < _count; i++)
                if (_inUseList[i] != null)
                    Object.Destroy(_inUseList[i].gameObject);

            ClearCollections();
        }

        [Button("Clear Collections ()")]
        protected override void ClearCollections()
        {
            _inPoolList.Clear();
            _inUseList.Clear();
        }

        public List<T> GetInUseList()
        {
            return _inUseList;
        }
    }
}
