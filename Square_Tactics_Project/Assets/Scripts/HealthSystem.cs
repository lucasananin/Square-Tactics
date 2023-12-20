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
    [SerializeField] BaseAction _buffDefenseAction = null;

    public event Action onTakeDamage = null;
    public event Action onDead = null;
    public static event EventHandler onAnyDead = null;

    private void Awake()
    {
        ResetHealth();
    }

    public void TakeDamage(int _damageAmount)
    {
        if (_isInvincible) return;
        if (IsDead()) return;

        _damageAmount /= GetDefenseReduction();
        _damageAmount = Mathf.Clamp(_damageAmount, 1, int.MaxValue);

        _currentHealth -= _damageAmount;

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

    public void RecoverHealth(int _amount)
    {
        _currentHealth += _amount;

        if (_currentHealth > _maxHealth)
        {
            _currentHealth = _maxHealth;
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

    public int GetDefenseReduction()
    {
        int _reduction = _buffDefenseAction != null && _buffDefenseAction.IsBuffActive() ? 2 : 1;
        return _reduction;
    }

    [Button]
    private void Debug_Die()
    {
        TakeDamage(_currentHealth);
    }
}
