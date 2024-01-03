using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Utilities.Audio
{
    public abstract class AudioVolumeSlider : MonoBehaviour
    {
        [Title("// General")]
        [SerializeField] protected AudioManagerSO _audioManagerSO = null;

        [Title("// UI - Sliders")]
        [SerializeField] protected Slider _slider = null;

        private IEnumerator Start()
        {
            yield return null;
            UpdateVisuals();
        }

        private void OnEnable()
        {
            _slider.onValueChanged.AddListener(SetVolume);
        }

        private void OnDisable()
        {
            _slider.onValueChanged.RemoveAllListeners();
        }

        public virtual void UpdateVisuals()
        {
            _slider.onValueChanged.RemoveAllListeners();
            _slider.minValue = _audioManagerSO.MIN_SLIDER_VALUE;
            _slider.value = GetVolume();
            _slider.maxValue = _audioManagerSO.MAX_SLIDER_VALUE;
            _slider.onValueChanged.AddListener(SetVolume);
        }

        public abstract void SetVolume(float _value);
        public abstract float GetVolume();
    }
}
