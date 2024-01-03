using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Audio
{
    public class AudioManager : MonoBehaviour
    {
        [Title("// General")]
        [SerializeField] AudioManagerSO _audioManagerSO = null;
        [SerializeField] AudioChannelSO _channelSO = null;
        [SerializeField] AudioEmitter _musicEmitter = null;

        [Title("// Pool")]
        [SerializeField] AudioEmitterPoolSO _pool = null;

        private void Start()
        {
            _audioManagerSO.LoadAudioSetting();
        }

        private void OnEnable()
        {
            _channelSO.playMusic += PlayMusic;
            _channelSO.playSfx += PlaySfx;
            _channelSO.stopMusic += StopMusic;
            _channelSO.stopSfx += StopSfx;
        }

        private void OnDisable()
        {
            _channelSO.playMusic -= PlayMusic;
            _channelSO.playSfx -= PlaySfx;
            _channelSO.stopMusic -= StopMusic;
            _channelSO.stopSfx -= StopSfx;
        }

        private AudioEmitter PlayMusic(AudioDataSO _audioValue)
        {
            if (_musicEmitter.IsPlaying())
            {
                _musicEmitter.AudioSource.DOFade(0, _audioManagerSO.MusicFadeDuration).
                    OnComplete(() => PlayMusicInstantly(_audioValue));
            }
            else
            {
                PlayMusicInstantly(_audioValue);
            }

            return _musicEmitter;
        }

        private void PlayMusicInstantly(AudioDataSO _value)
        {
            AudioSource _musicSource = _musicEmitter.AudioSource;
            _value.ApplySettings(ref _musicSource);
            _musicSource.Play();
        }

        private AudioEmitter PlaySfx(AudioDataSO _audio, Vector3 _position)
        {
            var _sfxSource = _pool.GetFromPool(transform);
            _sfxSource.Play(_audio);
            return _sfxSource;
        }

        private bool StopMusic()
        {
            if (_musicEmitter.IsPlaying())
            {
                _musicEmitter.AudioSource.DOFade(0, _audioManagerSO.MusicFadeDuration).
                    OnComplete(StopMusicInstantly);
                return true;
            }
            else
            {
                return false;
            }
        }

        private void StopMusicInstantly()
        {
            _musicEmitter.Stop(false);
        }

        private bool StopSfx(AudioEmitter _emitterValue)
        {
            if (_pool.HasInUse(_emitterValue))
            {
                var _index = _pool.GetInUseIndexOf(_emitterValue);
                var _foundEmitter = _pool.GetInUse(_index);
                _foundEmitter.Stop();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsPlayingThisMusic(AudioClip _musicClip)
        {
            return _musicClip == _musicEmitter.GetClip();
        }
    }
}
