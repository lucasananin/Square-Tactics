using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Audio
{
    public class SfxVolumeSlider : AudioVolumeSlider
    {
        public override float GetVolume()
        {
            return _audioManagerSO.GetSfxVolume();
        }

        public override void SetVolume(float _value)
        {
            _audioManagerSO.SetSfxVolume(_value);
        }
    }
}
