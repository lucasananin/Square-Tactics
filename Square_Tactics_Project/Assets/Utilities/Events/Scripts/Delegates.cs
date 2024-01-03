using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Events
{
    public static class Delegates
    {
        public delegate void Action();

        public delegate void Action<T>(T _value);
        public delegate void Action<T, U>(T _value1, U _value2);
        public delegate void Action<T, U, V>(T _value1, U _value2, V _value3);

        public delegate T Function<T>();
        public delegate T Function<T, U>(U _value);
        public delegate T Function<T, U, V>(U _value1, V _value2);
    }
}
