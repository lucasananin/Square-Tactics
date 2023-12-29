using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaleManager : Singleton<TimeScaleManager>
{
    [SerializeField] float _slowMotionDuration = 1f;
    [SerializeField] float _slowMotionTimeScale = 0.3f;

    private void OnEnable()
    {
        HealthSystem.onAnyDead += HealthSystem_onAnyDead;
    }

    private void OnDisable()
    {
        HealthSystem.onAnyDead -= HealthSystem_onAnyDead;
    }

    private void HealthSystem_onAnyDead(object sender, System.EventArgs e)
    {
        Play();
    }

    public void Play()
    {
        Time.timeScale = 1;
        StopAllCoroutines();

        StartCoroutine(Play_routine());
    }

    private IEnumerator Play_routine()
    {
        Time.timeScale = _slowMotionTimeScale;
        yield return new WaitForSeconds(_slowMotionDuration);
        Time.timeScale = 1;
    }
}
