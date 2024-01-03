using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Pools
{
    public abstract class AbstractPoolSO<T> : PoolRootSO where T : MonoBehaviour
    {
        [Title("// General")]
        [SerializeField] protected T _prefab = default;
        [SerializeField, Range(1, 99)] protected int _initialSpawn = 4;
        [Space]
        [SerializeField] protected bool _enableOnSpawn = false;
        [SerializeField] protected bool _enableOnGet = true;
        [SerializeField] protected bool _enableOnReturn = false;

        [Title("// Lists")]
        [SerializeField] protected List<T> _inPoolList = new List<T>();
        [SerializeField] protected List<T> _inUseList = new List<T>();

        private void OnEnable()
        {
            ClearCollections();
        }

        public override void PopulateList(Transform _parentValue = null)
        {
            PopulateList(_initialSpawn, _parentValue);
        }

        public override void PopulateList(int _count, Transform _parentValue = null)
        {
            for (int i = 0; i < _count; i++)
            {
                var _tempObject = Instantiate(_prefab, _parentValue);
                _tempObject.gameObject.SetActive(_enableOnSpawn);
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
            _tempObject.gameObject.SetActive(_enableOnGet);

            return _tempObject;
        }

        public T GetInUse(int _indexValue)
        {
            return _inUseList[_indexValue];
        }

        public void ReturnToPool(T _item)
        {
            _inUseList.Remove(_item);
            _inPoolList.Add(_item);
            _item.gameObject.SetActive(_enableOnReturn);
        }

        public override void DisposePool()
        {
            if (_inPoolList != null && _inPoolList.Count > 0)
            {
                var _count = _inPoolList.Count;

                for (int i = 0; i < _count; i++)
                    if (_inPoolList[i] != null)
                        Destroy(_inPoolList[i].gameObject);
            }

            if (_inUseList != null && _inUseList.Count > 0)
            {
                var _count = _inUseList.Count;

                for (int i = 0; i < _count; i++)
                    if (_inUseList[i] != null)
                        Destroy(_inUseList[i].gameObject);
            }

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

        public bool HasInPool(T _value)
        {
            return _inPoolList.Contains(_value);
        }

        public bool HasInUse(T _value)
        {
            return _inUseList.Contains(_value);
        }

        public int GetInPoolIndexOf(T _value)
        {
            return _inPoolList.IndexOf(_value);
        }

        public int GetInUseIndexOf(T _value)
        {
            return _inUseList.IndexOf(_value);
        }
    }
}