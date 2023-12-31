using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionButtonUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _actionNameText = null;
    [SerializeField] TextMeshProUGUI _apCostText = null;
    [SerializeField] Button _button = null;
    [SerializeField] Image _borderImage = null;

    private BaseAction _baseAction = null;

    private void OnDisable()
    {
        UnitActionSystem.Instance.onSelectedActionChanged -= UpdateSelectedVisuals;
        UnitActionSystem.Instance.onBusyStateChanged -= Instance_onBusyStateChanged;
        _button.onClick.RemoveAllListeners();
    }

    //private void LateUpdate()
    //{
    //    UpdateSelectedVisuals();
    //    UpdateInteractable();
    //}

    public void SetBaseAction(BaseAction _baseActionValue)
    {
        _baseAction = _baseActionValue;
        _actionNameText.text = $"{_baseActionValue.GetActionName()}";

        if (_baseActionValue is WaitAction)
            _apCostText.text = $"--";
        else
            _apCostText.text = $"{_baseActionValue.GetActionPointsCost():D2}";

        UnitActionSystem.Instance.onSelectedActionChanged += UpdateSelectedVisuals;
        UnitActionSystem.Instance.onBusyStateChanged += Instance_onBusyStateChanged;
        UpdateSelectedVisuals();

        _button.onClick.AddListener(() =>
        {
            UnitActionSystem.Instance.SetSelectedAction(_baseActionValue);
        });
    }

    public void UpdateSelectedVisuals()
    {
        bool _isSelected = _baseAction == UnitActionSystem.Instance.GetSelectedAction();
        _borderImage.enabled = _isSelected;
    }

    public void UpdateInteractable()
    {
        bool _hasEnoughAp = UnitActionSystem.Instance.GetSelectedUnit().GetActionPoints() >= _baseAction.GetActionPointsCost();
        _button.interactable = _hasEnoughAp;
    }

    private void Instance_onBusyStateChanged(bool _isBusy)
    {
        if (!_isBusy)
        {
            UpdateInteractable();
        }
    }
}
