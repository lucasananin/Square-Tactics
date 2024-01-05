using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Audio
{
    public class MusicStopper : AudioStopper
    {
        [SerializeField] protected bool _stopOnStart = false;

        private IEnumerator Start()
        {
            yield return null;

            if (_stopOnStart)
            {
                Stop();
            }
        }

        [Button]
        public override void Stop()
        {
            _channelSO.StopMusicEvent();
        }
    }
}
