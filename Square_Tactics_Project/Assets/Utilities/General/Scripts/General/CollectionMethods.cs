using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public static class CollectionMethods
    {
        public static IList<T> Swap<T>(this IList<T> _list, int _indexA, int _indexB)
        {
            T _temp = _list[_indexA];
            _list[_indexA] = _list[_indexB];
            _list[_indexB] = _temp;
            return _list;
        }
    }
}
