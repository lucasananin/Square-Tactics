using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Pools;

namespace Utilities.Audio
{
    [CreateAssetMenu(fileName = "Pool_AudioEmitter", menuName = "SO/Pools/Audio Emitter Pool")]
    public class AudioEmitterPoolSO : AbstractPoolSO<AudioEmitter> { }
}
