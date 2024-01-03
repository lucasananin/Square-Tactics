using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Utilities.Audio
{
    [CreateAssetMenu(fileName = "Manager_Audio", menuName = "SO/Managers/Audio Manager")]
    public class AudioManagerSO : ScriptableObject
    {
        [Title("// General")]
        [SerializeField] AudioMixer _audioMixer = null;

        [Title("// Music")]
        [SerializeField, Range(0.2f, 8f)] float _musicFadeDuration = 1f;

        public float MusicFadeDuration { get => _musicFadeDuration; private set => _musicFadeDuration = value; }
        public float MIN_SLIDER_VALUE { get => 0.0001f; }
        public float MAX_SLIDER_VALUE { get => 1; }
        public string VOL_MASTER { get => "volume_master"; }
        public string VOL_MUSIC { get => "volume_music"; }
        public string VOL_SFX { get => "volume_sfx"; }
        public string KEY_MASTER { get => "key_vol_master"; }
        public string KEY_MUSIC { get => "key_vol_music"; }
        public string KEY_SFX { get => "key_vol_sfx"; }

        public void LoadAudioSetting()
        {
            SetMasterVolume(GetMasterVolume());
            SetMusicVolume(GetMusicVolume());
            SetSfxVolume(GetSfxVolume());
        }

        public void SetMusicVolume(float _value)
        {
            SetVolume(_value, VOL_MUSIC, KEY_MUSIC);
        }

        public void SetSfxVolume(float _value)
        {
            SetVolume(_value, VOL_SFX, KEY_SFX);
        }

        public void SetMasterVolume(float _value)
        {
            SetVolume(_value, VOL_MASTER, KEY_MASTER);
        }

        private void SetVolume(float _value, string _volumeParameter, string _key)
        {
            _value = Mathf.Clamp(_value, MIN_SLIDER_VALUE, MAX_SLIDER_VALUE);
            _audioMixer.SetFloat(_volumeParameter, Mathf.Log10(_value) * 20);
            PlayerPrefs.SetFloat(_key, _value);
            PlayerPrefs.Save();
        }

        public float GetMusicVolume()
        {
            return GetVolume(KEY_MUSIC);
        }

        public float GetSfxVolume()
        {
            return GetVolume(KEY_SFX);
        }

        public float GetMasterVolume()
        {
            return GetVolume(KEY_MASTER);
        }

        private float GetVolume(string _key)
        {
            return PlayerPrefs.GetFloat(_key, MAX_SLIDER_VALUE);
        }

        public bool IsMusicVolumeEnabled()
        {
            return GetMusicVolume() > MIN_SLIDER_VALUE;
        }

        public bool IsSfxVolumeEnabled()
        {
            return GetSfxVolume() > MIN_SLIDER_VALUE;
        }

        public bool IsMasterVolumeEnabled()
        {
            return GetMasterVolume() > MIN_SLIDER_VALUE;
        }
    }
}
