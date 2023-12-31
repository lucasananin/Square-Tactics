using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInfoUi : MonoBehaviour
{
    [SerializeField] Image _image = null;
    [SerializeField] Image _hpFill = null;
    [SerializeField] Image _apFill = null;
    [SerializeField] TextMeshProUGUI _nameText = null;
    [SerializeField] TextMeshProUGUI _hpText = null;
    [SerializeField] TextMeshProUGUI _apText = null;

    private void Update()
    {
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        if (TurnSystem.Instance.IsPlayerTurn())
        {
            Unit _unit = UnitActionSystem.Instance.GetSelectedUnit();
            SetInfo(_unit);
        }
        else
        {
            Unit _unit = EnemyAi.Instance.GetSelectedUnit();
            SetInfo(_unit);
        }
    }

    private void SetInfo(Unit _unit)
    {
        if (_unit == null) return;

        UnitInfo _unitInfo = _unit.GetUnitInfo();

        _image.sprite = _unitInfo.GetSprite();
        _hpFill.fillAmount = _unitInfo.GetHealthNormalized();
        _apFill.fillAmount = _unitInfo.GetActionPointsNormalized();
        _nameText.text = _unitInfo.GetDisplayName();
        _hpText.text = _unitInfo.GetHealthString();
        _apText.text = _unitInfo.GetActionPointsString();
    }
}
