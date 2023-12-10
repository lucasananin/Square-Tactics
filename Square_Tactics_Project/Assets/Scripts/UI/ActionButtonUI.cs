using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionButtonUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _actionNameText = null;
    [SerializeField] Button _button = null;
    [SerializeField] Image _borderImage = null;

    private BaseAction _baseAction = null;

    private void OnDisable()
    {
        UnitActionSystem.Instance.onSelectedActionChanged -= UpdateSelectedVisuals;
        _button.onClick.RemoveAllListeners();
    }

    //private void LateUpdate()
    //{
    //    UpdateSelectedVisuals();
    //}

    public void SetBaseAction(BaseAction _baseActionValue)
    {
        _baseAction = _baseActionValue;
        _actionNameText.text = _baseActionValue.GetActionName().ToUpper();

        UnitActionSystem.Instance.onSelectedActionChanged += UpdateSelectedVisuals;
        UpdateSelectedVisuals();

        _button.onClick.AddListener(() =>
        {
            UnitActionSystem.Instance.SetSelectedAction(_baseActionValue);
        });
    }

    public void UpdateSelectedVisuals()
    {
        _borderImage.enabled = UnitActionSystem.Instance.GetSelectedAction() == _baseAction;
    }
}
