using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitButton : MonoBehaviour
{
    [SerializeField] Button _button = null;

    private void OnEnable()
    {
        _button.onClick.AddListener(QuitApplication);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveAllListeners();
    }

    private void QuitApplication()
    {
        Application.Quit();
    }
}
