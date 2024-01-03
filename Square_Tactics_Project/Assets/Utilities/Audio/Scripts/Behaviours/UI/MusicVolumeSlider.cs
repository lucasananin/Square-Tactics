using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Audio
{
    public class MusicVolumeSlider : AudioVolumeSlider
    {
        public override float GetVolume()
        {
            return _audioManagerSO.GetMusicVolume();
        }

        public override void SetVolume(float _value)
        {
            _audioManagerSO.SetMusicVolume(_value);
        }
    }
}
