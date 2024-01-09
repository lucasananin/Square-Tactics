using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class IntroController : MonoBehaviour
{
    [SerializeField] bool _skipIntro = false;
    [SerializeField] Transform _cameraController = null;
    [SerializeField] CanvasGroup _gameCanvasGroup = null;
    [Space]
    [SerializeField] float _rotationSpeed = 15f;
    [SerializeField] float _slowDownMultiplier = 1f;
    [SerializeField] float _waitTime = 2f;
    [SerializeField] bool _canSlowDown = false;
    [Space]
    [SerializeField] CanvasGroup _fadeCanvasGroup = null;
    [SerializeField] float _duration = 1f;

    private void Start()
    {
        if (_skipIntro)
        {
            _fadeCanvasGroup.alpha = 0;
            _fadeCanvasGroup.blocksRaycasts = false;
            _fadeCanvasGroup.interactable = false;

            _rotationSpeed = 0;
            GameManager.Instance.IsPlaying = true;
            _gameCanvasGroup.alpha = 1;
            _gameCanvasGroup.blocksRaycasts = true;
            _gameCanvasGroup.interactable = true;
            Destroy(this);
            return;
        }

        StartCoroutine(SlowDown_routine());
        StartCoroutine(FadeUi_Routine());
    }

    private void Update()
    {
        if (GameManager.Instance.IsPlaying) return;

        if (_canSlowDown)
        {
            _rotationSpeed -= _slowDownMultiplier * Time.deltaTime;
        }

        _cameraController.Rotate(new Vector3(0, _rotationSpeed, 0) * Time.deltaTime);

        if (_rotationSpeed <= 0)
        {
            _rotationSpeed = 0;
            GameManager.Instance.IsPlaying = true;
            _gameCanvasGroup.alpha = 1;
            _gameCanvasGroup.blocksRaycasts = true;
            _gameCanvasGroup.interactable = true;
            Destroy(this);
        }
    }

    private IEnumerator SlowDown_routine()
    {
        _gameCanvasGroup.alpha = 0;
        _gameCanvasGroup.blocksRaycasts = false;
        _gameCanvasGroup.interactable = false;

        yield return new WaitForSeconds(_waitTime);

        _canSlowDown = true;
    }

    private IEnumerator FadeUi_Routine()
    {
        _fadeCanvasGroup.alpha = 1;
        _fadeCanvasGroup.blocksRaycasts = true;
        _fadeCanvasGroup.interactable = true;

        yield return new WaitForSeconds(1);

        _fadeCanvasGroup.DOFade(0, _duration).SetEase(Ease.Linear).OnComplete(() =>
        {
            _fadeCanvasGroup.alpha = 0;
            _fadeCanvasGroup.blocksRaycasts = false;
            _fadeCanvasGroup.interactable = false;
        });
    }
}
