using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableCrate : MonoBehaviour
{
    [SerializeField] HealthSystem _healthSystem = null;

    private void OnEnable()
    {
        _healthSystem.onDead += Die;
    }

    private void OnDisable()
    {
        _healthSystem.onDead -= Die;
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
