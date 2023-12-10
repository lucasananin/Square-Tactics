using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitWorldUi : MonoBehaviour
{
    [Title("// General")] 
    [SerializeField] Unit _unit = null;
    [SerializeField] HealthSystem _healthSystem = null;

    [Title("// Action Points")]
    [SerializeField] TextMeshProUGUI _actionPointsText = null;

    [Title("// Health")]
    [SerializeField] Image _healthBar = null;
    [SerializeField] Gradient _healthGradient = null;

    //private void OnEnable()
    //{
    //    UnitActionSystem.Instance.onBusyStateChanged += UpdateActionPointsText;
    //}

    //private void OnDisable()
    //{
    //    UnitActionSystem.Instance.onBusyStateChanged -= UpdateActionPointsText;
    //}

    //private void UpdateActionPointsText(bool _value)
    //{
    //    UpdateActionPointsText();
    //}

    private void LateUpdate()
    {
        UpdateActionPointsText();
        UpdateHealthBar();
    }

    private void UpdateActionPointsText()
    {
        _actionPointsText.text = $"{_unit.GetActionPoints()}";
    }

    private void UpdateHealthBar()
    {
        float _healthNormalized = _healthSystem.GetHealthNormalized();
        _healthBar.fillAmount = _healthNormalized;
        _healthBar.color = _healthGradient.Evaluate(_healthNormalized);
    }
}
