using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Pools
{
    [CreateAssetMenu(fileName = "Pool_GenericObject", menuName = "SO/Pools/Generic Object Pool")]
    public class GenericObjectPoolSO : AbstractPoolSO<GenericPoolObject> { }
}
