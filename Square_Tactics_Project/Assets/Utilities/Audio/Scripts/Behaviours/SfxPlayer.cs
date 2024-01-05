using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Audio
{
    public class SfxPlayer : AudioPlayer
    {
        [Button]
        public override void Play()
        {
            _channelSO.PlaySfxEvent(_audioSO, transform.position);
        }
    }
}
