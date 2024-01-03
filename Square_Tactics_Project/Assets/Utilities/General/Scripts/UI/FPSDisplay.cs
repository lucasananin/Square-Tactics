using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Utilities
{
    public class FPSDisplay : MonoBehaviour
    {
        [Title("// UI - Texts")]
		[SerializeField] TextMeshProUGUI _text = null;

		private float _deltaTime = 0f;
        private float _msec = 0f;
        private float _fps = 0f;

        void Update()
		{
			_deltaTime += (Time.unscaledDeltaTime - _deltaTime) * 0.1f;
            _msec = _deltaTime * 1000.0f;
            _fps = 1.0f / _deltaTime;
            _text.SetText("{0:0.0} ms ({1:0.} fps)", _msec, _fps);
		}

        //void OnGUI()
        //{
        //    int w = Screen.width, h = Screen.height;

        //    GUIStyle style = new GUIStyle();

        //    Rect rect = new Rect(0, 0, w, h * 2 / 100);
        //    style.alignment = TextAnchor.UpperLeft;
        //    style.fontSize = h * 4 / 100;
        //    style.normal.textColor = new Color(0.0f, 0.0f, 0.5f, 1.0f);
        //    float msec = _deltaTime * 1000.0f;
        //    float fps = 1.0f / _deltaTime;
        //    string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
        //    GUI.Label(rect, text, style);
        //}
    }
}
