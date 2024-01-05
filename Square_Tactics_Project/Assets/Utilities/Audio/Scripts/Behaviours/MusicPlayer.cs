using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Audio
{
    public class MusicPlayer : AudioPlayer
    {
        [SerializeField] protected bool _playOnStart = true;

        private IEnumerator Start()
        {
            yield return null;

            if (_playOnStart)
            {
                Play();
            }
        }

        [Button]
        public override void Play()
        {
            _channelSO.PlayMusicEvent(_audioSO);
        }
    }
}
