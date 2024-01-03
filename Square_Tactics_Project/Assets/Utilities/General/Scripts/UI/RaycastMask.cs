using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Utilities
{
    public class RaycastMask : MonoBehaviour
    {
        // Basta colocar esse script no mesmo GameObject do componente "Button";
        private void Start()
        {
            GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
        }
    }
}
