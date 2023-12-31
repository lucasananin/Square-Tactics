using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitInfo : MonoBehaviour
{
    [SerializeField] Sprite _characterSprite = null;
    [SerializeField] string _characterName = null;
    [SerializeField] HealthSystem _characterHealth = null;
    [SerializeField] Unit _characterUnit = null;

    public Sprite GetSprite()
    {
        return _characterSprite;
    }

    public string GetDisplayName()
    {
        return _characterName;
    }

    public string GetHealthString()
    {
        return _characterHealth.GetHealthString();
    }

    public float GetHealthNormalized()
    {
        return _characterHealth.GetHealthNormalized();
    }

    public string GetActionPointsString()
    {
        return _characterUnit.GetActionPointsString();
    }

    public float GetActionPointsNormalized()
    {
        return _characterUnit.GetActionPointsNormalized();
    }

    [Button]
    private void GetReferences()
    {
        _characterHealth = GetComponent<HealthSystem>();
        _characterUnit = GetComponent<Unit>();
    }
}
