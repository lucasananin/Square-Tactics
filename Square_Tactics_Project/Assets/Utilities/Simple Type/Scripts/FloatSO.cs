using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.SimpleTypes
{
    [CreateAssetMenu(fileName = "Float_", menuName = "SO/Simple Types/Float")]
    public class FloatSO : AbstractSimpleType<float>
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
