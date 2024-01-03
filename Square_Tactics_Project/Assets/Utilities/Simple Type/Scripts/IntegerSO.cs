using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.SimpleTypes
{
    [CreateAssetMenu(fileName = "Integer_", menuName = "SO/Simple Types/Integer")]
    public class IntegerSO : AbstractSimpleType<int>
    {
        protected override void ClampCurrentValue()
        {
            if (_clampMin)
                _currentValue = ClampMethods.ClampMin(_currentValue, _minValue);

            if (_clampMax)
                _currentValue = ClampMethods.ClampMax(_currentValue, _maxValue);
        }
    }
}
