using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitActionSystemUI : MonoBehaviour
{
    [SerializeField] ActionButtonUI _actionButtonPrefab = null;
    [SerializeField] Transform _actionButtonParent = null;
    [SerializeField] CanvasGroup _canvasGroup = null;
    [SerializeField] List<ActionButtonUI> _actionButtonUis = null;

    private void OnEnable()
    {
        UnitActionSystem.Instance.onSelectedUnitChanged += UpdateButtons;
        UnitActionSystem.Instance.onBusyStateChanged += Instance_onBusyStateChanged;
    }

    private void OnDisable()
    {
        UnitActionSystem.Instance.onSelectedUnitChanged -= UpdateButtons;
        UnitActionSystem.Instance.onBusyStateChanged -= Instance_onBusyStateChanged;
    }

    public void UpdateButtons()
    {
        var _unit = UnitActionSystem.Instance.GetSelectedUnit();
        int _count = _actionButtonParent.childCount;

        for (int i = 0; i < _count; i++)
        {
            Destroy(_actionButtonParent.GetChild(i).gameObject);
        }

        _actionButtonUis.Clear();

        if (!UnitActionSystem.Instance.HasUnitSelected())
        {
            Hide();
            return;
        }

        _count = _unit.GetBaseActions().Length;

        for (int i = 0; i < _count; i++)
        {
            var _instance = Instantiate(_actionButtonPrefab, _actionButtonParent);
            _instance.SetBaseAction(_unit.GetBaseActions()[i]);
            _actionButtonUis.Add(_instance);
        }

        Show();
    }

    private void Instance_onBusyStateChanged(bool _isBusy)
    {
        if (_isBusy)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }

    private void Show()
    {
        _canvasGroup.alpha = 1;
        _canvasGroup.blocksRaycasts = true;
        _canvasGroup.interactable = true;
    }

    private void Hide()
    {
        _canvasGroup.alpha = 0;
        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.interactable = false;
    }
}
