using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Managers
{
    [CreateAssetMenu(fileName = "Manager_Game", menuName = "SO/Managers/Game Manager")]
    public class GameManagerSO : ScriptableObject
    {
        [Header("// Pause")]
        [SerializeField] bool _canPause = true;
        [SerializeField] bool _isPaused = false;

        [Header("// Input")]
        [SerializeField] bool _multiTouchEnabled = true;

        [Header("// Pool")]
        [SerializeField] bool _useParent = true;

        [Header("// Time")]
        [SerializeField, Range(0, 10)] float _defaultTimeScale = 1;

        public bool IsPaused { get => _isPaused; set => _isPaused = value; }
        public bool CanPause { get => _canPause; set => _canPause = value; }
        public bool MultiTouchEnabled { get => _multiTouchEnabled; private set => _multiTouchEnabled = value; }
        public bool UseParent { get => _useParent; private set => _useParent = value; }
        public float DefaultTimeScale { get => _defaultTimeScale; private set => _defaultTimeScale = value; }

        private void OnEnable()
        {
            ResetRuntimeValues();
        }

        public void ResetRuntimeValues()
        {
            _canPause = true;
            _isPaused = false;
        }
    }
}