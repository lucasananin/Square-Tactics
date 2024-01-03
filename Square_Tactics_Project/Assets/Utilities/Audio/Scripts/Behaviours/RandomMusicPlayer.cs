using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Audio
{
    public class RandomMusicPlayer : MonoBehaviour
    {
        [Title("// General")]
        [SerializeField] AudioChannelSO _channelSO = null;
        [SerializeField] bool _playOnStart = true;
        [SerializeField] float _timeBetweenSongs = 1f;
        [Space]
        [SerializeField] List<AudioDataSO> _musics = null;

        [Title("// Debug")]
        [SerializeField] float _nextMusic = 0;
        [SerializeField] float _timer = 0;
        [SerializeField] bool _isPlaying = false;

        private IEnumerator Start()
        {
            yield return null;

            if (_playOnStart)
            {
                PlayRandomMusic();
            }
            else
            {
                _isPlaying = false;
            }
        }

        private void Update()
        {
            if (!_isPlaying) return;

            _timer += Time.deltaTime;

            if (_timer > _nextMusic)
            {
                PlayRandomMusic();
            }
        }

        [Button]
        public void PlayRandomMusic()
        {
            _isPlaying = true;

            int _index = Random.Range(1, _musics.Count);
            var _audio = _musics[_index];

            _musics[_index] = _musics[0];
            _musics[0] = _audio;

            _timer = 0;
            _nextMusic = _audio.GetClipLength() + _timeBetweenSongs;

            _channelSO.InvokePlayMusic(_audio);
        }
    }
}
