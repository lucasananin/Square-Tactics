using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public static class ClampMethods
    {
        public static double ClampMin(double _value, double _min)
        {
            return _value < _min ? _min : _value;
        }

        public static double ClampMax(double _value, double _max)
        {
            return _value > _max ? _max : _value;
        }

        public static double Clamp(double _value, double _min, double _max)
        {
            _value = ClampMin(_value, _min);
            _value = ClampMax(_value, _max);
            return _value;
        }

        public static float ClampMin(float _value, float _min)
        {
            return _value < _min ? _min : _value;
        }

        public static float ClampMax(float _value, float _max)
        {
            return _value > _max ? _max : _value;
        }

        public static float Clamp(float _value, float _min, float _max)
        {
            _value = ClampMin(_value, _min);
            _value = ClampMax(_value, _max);
            return _value;
        }

        public static int ClampMin(int _value, int _min)
        {
            return _value < _min ? _min : _value;
        }

        public static int ClampMax(int _value, int _max)
        {
            return _value > _max ? _max : _value;
        }

        public static int Clamp(int _value, int _min, int _max)
        {
            _value = ClampMin(_value, _min);
            _value = ClampMax(_value, _max);
            return _value;
        }
    }
}
