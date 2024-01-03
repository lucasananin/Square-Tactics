using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Audio
{
    public class MasterVolumeToggle : AudioVolumeToggle
    {
        public override bool IsVolumeEnabled()
        {
            return _audioManagerSO.IsMasterVolumeEnabled();
        }

        public override void SetVolume(bool _isOn)
        {
            float _value = _isOn ? 1 : 0;
            _audioManagerSO.SetMasterVolume(_value);
        }
    }
}
