using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public static class Printer
    {
        [System.Diagnostics.Conditional("ENABLE_LOG")]
        public static void Log(object _message)
        {
            Debug.Log(_message);
        }

        [System.Diagnostics.Conditional("ENABLE_LOG")]
        public static void Log(object _message, Object _context)
        {
            Debug.Log(_message, _context);
        }

        [System.Diagnostics.Conditional("ENABLE_LOG")]
        public static void LogWarning(object _message)
        {
            Debug.LogWarning(_message);
        }

        [System.Diagnostics.Conditional("ENABLE_LOG")]
        public static void LogWarning(object _message, Object _context)
        {
            Debug.LogWarning(_message, _context);
        }

        [System.Diagnostics.Conditional("ENABLE_LOG")]
        public static void LogError(object _message)
        {
            Debug.LogError(_message);
        }

        [System.Diagnostics.Conditional("ENABLE_LOG")]
        public static void LogError(object _message, Object _context)
        {
            Debug.LogError(_message, _context);
        }
    }
}
