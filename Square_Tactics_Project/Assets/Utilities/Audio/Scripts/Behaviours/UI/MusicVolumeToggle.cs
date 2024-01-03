using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Audio
{
    public class MusicVolumeToggle : AudioVolumeToggle
    {
        public override bool IsVolumeEnabled()
        {
            return _audioManagerSO.IsMusicVolumeEnabled();
        }

        public override void SetVolume(bool _isOn)
        {
            float _value = _isOn ? 1 : 0;
            _audioManagerSO.SetMusicVolume(_value);
        }
    }
}
