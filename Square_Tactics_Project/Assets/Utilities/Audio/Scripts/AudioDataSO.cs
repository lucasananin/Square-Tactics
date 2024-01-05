using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Utilities.Audio
{
    [CreateAssetMenu(fileName = "AudioData", menuName = "SO/Audio/Audio Data")]
    public class AudioDataSO : ScriptableObject
    {
        [Title("// General")]
        [SerializeField] AudioChannelSO _audioChannelSO = null;
        [SerializeField] AudioClip _clip = null;
        [SerializeField] AudioMixerGroup _group = null;
        [Space]
        [SerializeField] bool _loop = false;
        [Space]
        [SerializeField, Range(0, 256)] int _priority = 128;
        [SerializeField, Range(0, 1)] float _volume = 1;
        [SerializeField, Range(-3, 3)] float _pitch = 1;
        [SerializeField, Range(-1, 1)] float _stereoPan = 0;
        [SerializeField, Range(0, 1)] float _spatialBlend = 0;
        [SerializeField, Range(0, 1.1f)] float _reverbZoneMix = 1;

        [Title("// 3D Settings")]
        [SerializeField, Range(0, 5)] float _dopplerLevel = 1;
        [SerializeField, Range(0, 360)] float _spread = 0;
        [SerializeField] AudioRolloffMode _volumeRolloff = AudioRolloffMode.Logarithmic;
        [SerializeField, Range(0, 360)] float _minDistance = 1;
        [SerializeField, Range(0, 360)] float _maxDistance = 500;

        [Title("// Delay")]
        [SerializeField, Range(0, 10)] float _delayInSeconds = 0f;

        public float DelayInSeconds { get => _delayInSeconds; }

        public float GetClipLength()
        {
            return _clip.length + _delayInSeconds;
        }

        public void ApplySettings(ref AudioSource _source)
        {
            _source.clip = _clip;
            _source.outputAudioMixerGroup = _group;

            _source.loop = _loop;

            _source.priority = _priority;
            _source.volume = _volume;
            _source.pitch = _pitch;
            _source.panStereo = _stereoPan;
            _source.spatialBlend = _spatialBlend;
            _source.reverbZoneMix = _reverbZoneMix;

            _source.dopplerLevel = _dopplerLevel;
            _source.spread = _spread;
            _source.rolloffMode = _volumeRolloff;
            _source.minDistance = _minDistance;
            _source.maxDistance = _maxDistance;
        }

        public AudioEmitter PlayAsMusic()
        {
            return _audioChannelSO.PlayMusicEvent(this);
        }

        public AudioEmitter PlayAsSfx()
        {
            return _audioChannelSO.PlaySfxEvent(this);
        }

        public AudioEmitter PlayAsSfx(Vector3 _positionValue)
        {
            return _audioChannelSO.PlaySfxEvent(this, _positionValue);
        }
    }
}
