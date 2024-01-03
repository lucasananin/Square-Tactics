using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Audio
{
    public class AudioEmitter : MonoBehaviour
    {
        [Title("// General")]
        [SerializeField] AudioEmitterPoolSO _myPool = null;
        [SerializeField] AudioSource _audioSource = null;

        private float _timeToReturn = 0f;

        public AudioSource AudioSource { get => _audioSource; private set => _audioSource = value; }

        private void OnValidate()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void Play(AudioDataSO _audio)
        {
            _audio.ApplySettings(ref _audioSource);
            _audioSource.Play();

            _timeToReturn = _audio.GetClipLength() + 0.2f;
            StartCoroutine(ReturnRoutine());
        }

        public void Stop(bool _returnToPool = true)
        {
            StopAllCoroutines();
            _audioSource.Stop();

            if (_returnToPool)
                _myPool.ReturnToPool(this);
        }

        public bool IsPlaying()
        {
            return _audioSource.isPlaying;
        }

        public AudioClip GetClip()
        {
            return _audioSource.clip;
        }

        private IEnumerator ReturnRoutine()
        {
            yield return new WaitForSeconds(_timeToReturn);

            _myPool.ReturnToPool(this);
        }
    }
}
