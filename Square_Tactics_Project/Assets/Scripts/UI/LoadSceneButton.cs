using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneButton : MonoBehaviour
{
    [SerializeField] CanvasGroup _fadeCanvasGroup = null;
    [SerializeField] GameObject _buttonsPanel = null;
    [SerializeField] Button _button = null;
    [SerializeField] string _sceneName = null;

    private void OnEnable()
    {
        _button.onClick.AddListener(FadeAndLoadScene);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveAllListeners();
    }

    private void FadeAndLoadScene()
    {
        _buttonsPanel.SetActive(false);

        _fadeCanvasGroup.DOFade(1, 1).SetEase(Ease.Linear).OnComplete(() => 
        {
            SceneManager.LoadScene(_sceneName);
        });
    }
}
