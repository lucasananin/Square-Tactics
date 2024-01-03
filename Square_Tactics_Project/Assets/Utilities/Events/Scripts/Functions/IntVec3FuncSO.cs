using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Events
{
    [CreateAssetMenu(fileName = "OnIntVec3", menuName = "SO/Events/Int Vec3 Func", order = 1000)]
    public class IntVec3FuncSO : AbstractFuncSO1<Vector3, int> { }
}
