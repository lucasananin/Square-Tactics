using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Utilities.Audio
{
    public abstract class AudioVolumeToggle : MonoBehaviour
    {
        [Title("// General")]
        [SerializeField] protected AudioManagerSO _audioManagerSO = null;

        [Title("// UI - Toggles")]
        [SerializeField] protected Toggle _toggle = null;

        private IEnumerator Start()
        {
            yield return null;
            UpdateVisuals();
        }

        private void OnEnable()
        {
            _toggle.onValueChanged.AddListener(SetVolume);
        }

        private void OnDisable()
        {
            _toggle.onValueChanged.RemoveAllListeners();
        }

        public virtual void UpdateVisuals()
        {
            _toggle.onValueChanged.RemoveAllListeners();
            _toggle.isOn = IsVolumeEnabled();
            _toggle.onValueChanged.AddListener(SetVolume);
        }

        public abstract void SetVolume(bool _isOn);
        public abstract bool IsVolumeEnabled();
    }
}
