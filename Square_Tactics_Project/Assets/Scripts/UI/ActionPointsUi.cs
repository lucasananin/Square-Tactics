using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ActionPointsUi : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text = null;

    //private void OnEnable()
    //{
    //    UnitActionSystem.Instance.onSelectedUnitChanged += UpdateVisuals;
    //    UnitActionSystem.Instance.onBusyStateChanged += UpdateVisuals;
    //}

    //private void OnDisable()
    //{
    //    UnitActionSystem.Instance.onSelectedActionChanged -= UpdateVisuals;
    //    UnitActionSystem.Instance.onBusyStateChanged -= UpdateVisuals;
    //}

    private void LateUpdate()
    {
        UpdateVisuals();
    }

    //private void UpdateVisuals(bool _value)
    //{
    //    UpdateVisuals();
    //}

    private void UpdateVisuals()
    {
        if (!UnitActionSystem.Instance.HasUnitSelected()) return;

        _text.text = $"AP: {UnitActionSystem.Instance.GetSelectedUnit().GetActionPoints()}";
    }
}
