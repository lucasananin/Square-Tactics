using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public abstract class AbstractGroupSO<T> : ScriptableObject where T : ScriptableObject
    {
        [Title("// General")]
        [SerializeField] protected bool _populateDictionary = false;
        [SerializeField] protected List<T> _list = null;

        private Dictionary<string, T> _dictionary = null;

        public List<T> List { get => _list; private set => _list = value; }

        private void OnEnable()
        {
            VerifyDictionary();
        }

        public T GetRandomSO()
        {
            return _list[Random.Range(0, _list.Count)];
        }

        public T GetSO(int _index)
        {
            return _list[_index];
        }

        public T GetSO(string _id)
        {
            if (_populateDictionary)
            {
                return _dictionary[_id];
            }
            else
            {
                int _count = _list.Count;

                for (int i = 0; i < _count; i++)
                {
                    var _obj = _list[i];

                    if (_obj.name == _id)
                    {
                        return _obj;
                    }
                }

                return null;
            }
        }

        public int GetCount()
        {
            return _list.Count;
        }

        public void ClearCollections()
        {
            _list.Clear();
            _dictionary.Clear();
        }

        private void VerifyDictionary()
        {
            if (_populateDictionary && (_dictionary.Count <= 0 || _dictionary == null))
            {
                PopulateDictionary();
            }
        }

        private void PopulateDictionary()
        {
            _dictionary = new Dictionary<string, T>();

            int _count = _list.Count;

            for (int i = 0; i < _count; i++)
            {
                _dictionary.Add(_list[i].name, _list[i]);
            }
        }
    }
}
