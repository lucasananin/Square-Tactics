using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Utilities.Audio
{
    public class SfxPlayerButton : AudioPlayer
    {
        [Title("// UI - Buttons")]
        [SerializeField] Button _button = null;

        private void OnEnable()
        {
            _button.onClick.AddListener(Play);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveAllListeners();
        }

        [Button]
        public override void Play()
        {
            _channelSO.InvokePlaySfx(_audioSO, Vector3.zero);
        }
    }
}
