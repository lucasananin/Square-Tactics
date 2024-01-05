using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Events;

namespace Utilities.Audio
{
    [CreateAssetMenu(fileName = "AudioChannel", menuName = "SO/Audio/Audio Channel")]
    public class AudioChannelSO : ScriptableObject
    {
        public event Delegates.Function<AudioEmitter, AudioDataSO> playMusic = null;
        public event Delegates.Function<AudioEmitter, AudioDataSO, Vector3> playSfx = null;

        public event Delegates.Function<bool> stopMusic = null;
        public event Delegates.Function<bool, AudioEmitter> stopSfx = null;

        public AudioEmitter PlayMusicEvent(AudioDataSO _audioValue)
        {
            return playMusic?.Invoke(_audioValue);
        }

        public AudioEmitter PlaySfxEvent(AudioDataSO _audioValue, Vector3 _positionValue)
        {
            return playSfx?.Invoke(_audioValue, _positionValue);
        }

        public AudioEmitter PlaySfxEvent(AudioDataSO _audioValue)
        {
            return playSfx?.Invoke(_audioValue, default);
        }

        public bool StopMusicEvent()
        {
            return (bool)(stopMusic?.Invoke());
        }

        public bool StopSfxEvent(AudioEmitter _audioEmitter)
        {
            return (bool)(stopSfx?.Invoke(_audioEmitter));
        }
    }
}
