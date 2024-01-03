using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Audio
{
    public abstract class AudioStopper : MonoBehaviour
    {
        [Title("// General")]
        [SerializeField] protected AudioChannelSO _channelSO = null;

        public abstract void Stop();
    }
}
