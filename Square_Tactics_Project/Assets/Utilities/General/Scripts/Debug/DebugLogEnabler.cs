using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public class DebugLogEnabler : MonoBehaviour
    {
        private void Start()
        {
#if UNITY_EDITOR
            Debug.unityLogger.logEnabled = true;
#else
            Debug.unityLogger.logEnabled = false;
#endif
        }
    }
}
