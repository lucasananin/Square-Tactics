using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Utilities.Events
{
    [CreateAssetMenu(fileName = "OnContext", menuName = "SO/Events/Context Action", order = 200)]
    public class ContextActionSO : AbstractActionSO1<InputAction.CallbackContext> { }
}
