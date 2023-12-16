using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] int _currentHealth = 12;
    [SerializeField] int _maxHealth = 12;
    [SerializeField] bool _isInvincible = false;

    public event Action onTakeDamage = null;
    public event Action onDead = null;
    public static event EventHandler onAnyDead = null;

    private void Awake()
    {
        ResetHealth();
    }

    public void TakeDamage(int _amount)
    {
        if (_isInvincible) return;
        if (IsDead()) return;

        _currentHealth -= _amount;

        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            onDead?.Invoke();
            onAnyDead?.Invoke(this, null);
        }
        else
        {
            onTakeDamage?.Invoke();
        }
    }

    private void ResetHealth()
    {
        _currentHealth = _maxHealth;
    }

    public bool IsDead()
    {
        return _currentHealth <= 0;
    }

    public float GetHealthNormalized()
    {
        return (float)_currentHealth / _maxHealth;
    }

    [Button]
    private void Debug_Die()
    {
        TakeDamage(_currentHealth);
    }
}
