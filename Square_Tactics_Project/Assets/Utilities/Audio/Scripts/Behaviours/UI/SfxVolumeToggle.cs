using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Audio
{
    public class SfxVolumeToggle : AudioVolumeToggle
    {
        public override bool IsVolumeEnabled()
        {
            return _audioManagerSO.IsSfxVolumeEnabled();
        }

        public override void SetVolume(bool _isOn)
        {
            float _value = _isOn ? 1 : 0;
            _audioManagerSO.SetSfxVolume(_value);
        }
    }
}
