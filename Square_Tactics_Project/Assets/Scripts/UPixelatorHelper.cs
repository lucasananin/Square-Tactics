using Abiogenesis3d;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UPixelatorHelper : MonoBehaviour
{
    [SerializeField] UPixelator _uPixelator = null;

    private IEnumerator Start()
    {
        yield return null;

#if UNITY_WEBGL
        _uPixelator.pixelMultiplier--;
#endif
    }
}
