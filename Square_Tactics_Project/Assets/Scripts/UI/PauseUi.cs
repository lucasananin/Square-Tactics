using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUi : MonoBehaviour
{
    [SerializeField] CanvasGroup _canvasGroup = null;
    [SerializeField] Button _quitButton = null;
    [SerializeField, ReadOnly] bool _isPaused = false;
    [SerializeField, ReadOnly] float _timeScale = 0;

    private void Awake()
    {
        Hide();
    }

    private void OnEnable()
    {
        _quitButton.onClick.AddListener(QuitGame);
    }

    private void OnDisable()
    {
        _quitButton.onClick.RemoveAllListeners();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isPaused)
            {
                _isPaused = false;
                Time.timeScale = _timeScale;
                Hide();
            }
            else
            {
                _isPaused = true;
                _timeScale = Time.timeScale;
                Time.timeScale = 0;
                Show();
            }
        }
    }

    private void Show()
    {
        _canvasGroup.alpha = 1;
        _canvasGroup.blocksRaycasts = true;
        _canvasGroup.interactable = true;
    }

    private void Hide()
    {
        _canvasGroup.alpha = 0;
        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.interactable = false;
    }

    private void QuitGame()
    {
        Application.Quit();
    }
}
