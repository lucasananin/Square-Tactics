using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Audio;
using DG.Tweening;

public class OutroController : MonoBehaviour
{
    [SerializeField] CameraController _cameraController = null;
    [SerializeField] bool _canMoveCamera = false;
    [SerializeField] float _moveSpeed = 5f;
    [SerializeField] GameObject _gamePanel = null;
    [SerializeField] AudioManager _audioManager = null;
    [SerializeField] CanvasGroup _thanksPanel = null;
    [SerializeField] CanvasGroup _youLostPanel = null;
    [SerializeField] float _fadeDuration = 1f;

    private void OnEnable()
    {
        UnitManager.onAllEnemyUnitsDied += UnitManager_onAllEnemyUnitsDied;
        UnitManager.onAllPlayerUnitsDied += UnitManager_onAllPlayerUnitsDied;
    }

    private void OnDisable()
    {
        UnitManager.onAllEnemyUnitsDied -= UnitManager_onAllEnemyUnitsDied;
        UnitManager.onAllPlayerUnitsDied -= UnitManager_onAllPlayerUnitsDied;
    }

    private void Update()
    {
        if (_canMoveCamera)
        {
            _cameraController.transform.position += Vector3.up * _moveSpeed * Time.deltaTime;
        }
    }

    private void UnitManager_onAllEnemyUnitsDied()
    {
        StartCoroutine(ThanksRoutine());
    }

    private IEnumerator ThanksRoutine()
    {
        GameManager.Instance.IsPlaying = false;

        yield return new WaitForSeconds(2);

        _cameraController.ResetTarget();
        _canMoveCamera = true;
        _gamePanel.SetActive(false);
        _audioManager.StopMusic();

        yield return new WaitForSeconds(2);

        _thanksPanel.DOFade(1, 1);
        _thanksPanel.blocksRaycasts = true;
        _thanksPanel.interactable = true;
    }

    private void UnitManager_onAllPlayerUnitsDied()
    {
        StartCoroutine(LostRoutine());
    }

    private IEnumerator LostRoutine()
    {
        GameManager.Instance.IsPlaying = false;

        yield return new WaitForSeconds(2);

        _cameraController.ResetTarget();
        _canMoveCamera = true;
        _gamePanel.SetActive(false);
        _audioManager.StopMusic();

        yield return new WaitForSeconds(2);

        _youLostPanel.DOFade(1, 1);
        _youLostPanel.blocksRaycasts = true;
        _youLostPanel.interactable = true;
    }
}
