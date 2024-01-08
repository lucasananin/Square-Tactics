using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ActionDescriptionUi : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text = null;

    private void OnEnable()
    {
        UnitActionSystem.Instance.onSelectedActionChanged += Instance_onSelectedActionChanged;
    }

    private void OnDisable()
    {
        UnitActionSystem.Instance.onSelectedActionChanged -= Instance_onSelectedActionChanged;
    }

    private void Instance_onSelectedActionChanged()
    {
        var _selectedAction = UnitActionSystem.Instance.GetSelectedAction();
        _text.text = _selectedAction.GetDescription();
    }
}
