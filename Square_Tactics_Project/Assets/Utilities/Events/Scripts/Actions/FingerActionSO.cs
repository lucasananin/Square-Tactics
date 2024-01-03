using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

namespace Utilities.Events
{
    [CreateAssetMenu(fileName = "OnFinger", menuName = "SO/Events/Finger Action", order = 200)]
    public class FingerActionSO : AbstractActionSO1<Finger> { }
}
