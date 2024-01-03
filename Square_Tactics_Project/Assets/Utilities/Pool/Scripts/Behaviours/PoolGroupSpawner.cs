using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Managers;

namespace Utilities.Pools
{
    public class PoolGroupSpawner : MonoBehaviour
    {
        [Title("// General")]
        [SerializeField] GameManagerSO _gameManagerSO = null;
        [SerializeField] PoolGroupSO _poolGroup = null;

        [Title("// Parent")]
        [SerializeField] bool _overrideUseParent = false;
        [SerializeField, ShowIf(nameof(_overrideUseParent))] bool _useParent = false;

        private void Start()
        {
            bool _useParentValue = _overrideUseParent ? _useParent : _gameManagerSO.UseParent;
            int _count = _poolGroup.GetCount();

            for (int i = 0; i < _count; i++)
            {
                var _pool = _poolGroup.GetSO(i);
                _pool.PopulateList(_useParentValue ? transform : null);
            }
        }

        private void OnDestroy()
        {
            int _count = _poolGroup.GetCount();

            for (int i = 0; i < _count; i++)
            {
                var _pool = _poolGroup.GetSO(i);
                _pool.DisposePool();
            }
        }
    }
}
