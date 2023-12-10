using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitActionSystemUI : MonoBehaviour
{
    [SerializeField] ActionButtonUI _actionButtonPrefab = null;
    [SerializeField] Transform _actionButtonParent = null;
    [SerializeField] List<ActionButtonUI> _actionButtonUIs = null;

    private void OnEnable()
    {
        UnitActionSystem.Instance.onSelectedUnitChanged += UpdateButtons;
    }

    private void OnDisable()
    {
        UnitActionSystem.Instance.onSelectedUnitChanged -= UpdateButtons;
    }

    public void UpdateButtons()
    {
        var _unit = UnitActionSystem.Instance.GetSelectedUnit();
        int _count = _actionButtonParent.childCount;

        for (int i = 0; i < _count; i++)
        {
            Destroy(_actionButtonParent.GetChild(i).gameObject);
        }

        _actionButtonUIs.Clear();

        if (!UnitActionSystem.Instance.HasUnitSelected()) return;

        _count = _unit.GetBaseActions().Length;

        for (int i = 0; i < _count; i++)
        {
            var _instance = Instantiate(_actionButtonPrefab, _actionButtonParent);
            _instance.SetBaseAction(_unit.GetBaseActions()[i]);
            _actionButtonUIs.Add(_instance);
        }
    }
}
