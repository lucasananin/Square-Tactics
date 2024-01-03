using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Audio
{
    public class MasterVolumeSlider : AudioVolumeSlider
    {
        public override float GetVolume()
        {
            return _audioManagerSO.GetMasterVolume();
        }

        public override void SetVolume(float _value)
        {
            _audioManagerSO.SetMasterVolume(_value);
        }
    }
}
