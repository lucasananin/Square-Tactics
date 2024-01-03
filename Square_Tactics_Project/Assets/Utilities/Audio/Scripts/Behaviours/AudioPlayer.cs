using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Audio
{
    public abstract class AudioPlayer : MonoBehaviour
    {
        [Title("// General")]
        [SerializeField] protected AudioChannelSO _channelSO = null;
        [SerializeField] protected AudioDataSO _audioSO = null;

        public abstract void Play();
    }
}
