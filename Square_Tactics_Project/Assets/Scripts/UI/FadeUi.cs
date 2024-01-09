using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeUi : MonoBehaviour
{
    [SerializeField] CanvasGroup _canvasGroup = null;
    [SerializeField] float _duration = 1;

    private IEnumerator Start()
    {
        _canvasGroup.alpha = 1;
        _canvasGroup.blocksRaycasts = true;

        yield return new WaitForSeconds(1);

        _canvasGroup.DOFade(0, _duration).SetEase(Ease.Linear).OnComplete(() => 
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.blocksRaycasts = false;
        });
    }
}
