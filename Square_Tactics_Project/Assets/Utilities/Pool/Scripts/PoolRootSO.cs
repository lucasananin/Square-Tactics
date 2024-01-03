using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Pools
{
    public abstract class PoolRootSO : ScriptableObject
    {
        public abstract void PopulateList(Transform _parent = null);
        public abstract void PopulateList(int _count, Transform _parent = null);
        public abstract void DisposePool();
        protected abstract void ClearCollections();
    }
}
