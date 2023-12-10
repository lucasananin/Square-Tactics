using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelectedVisual : MonoBehaviour
{
    [SerializeField] Unit _unit = null;
    [SerializeField] MeshRenderer _meshRenderer = null;

    private void Start()
    {
        UpdateVisuals();
    }

    private void OnEnable()
    {
        UnitActionSystem.Instance.onSelectedUnitChanged += UpdateVisuals;
    }

    private void OnDisable()
    {
        UnitActionSystem.Instance.onSelectedUnitChanged -= UpdateVisuals;
    }

    private void UpdateVisuals()
    {
        _meshRenderer.enabled = _unit == UnitActionSystem.Instance.GetSelectedUnit();
    }
}
