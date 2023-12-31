using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInfoUi : MonoBehaviour
{
    [SerializeField] CanvasGroup _canvasGroup = null;
    [SerializeField] bool _updateOnHover = false;
    [SerializeField] Image _image = null;
    [SerializeField] Image _hpFill = null;
    [SerializeField] Image _apFill = null;
    [SerializeField] TextMeshProUGUI _nameText = null;
    [SerializeField] TextMeshProUGUI _hpText = null;
    [SerializeField] TextMeshProUGUI _apText = null;

    private void Update()
    {
        if (_updateOnHover)
        {
            UpdateToHovered();
        }
        else
        {
            UpdateToSelected();
        }
    }

    private void UpdateToHovered()
    {
        var _hitinfo = MouseWorld.Instance.GetHitInfo();

        if (_hitinfo.collider == null)
        {
            Hide();
            return;
        }

        var _unit = _hitinfo.collider.GetComponent<Unit>();

        if (_unit == null || _unit == UnitActionSystem.Instance.GetSelectedUnit() || _unit == EnemyAi.Instance.GetSelectedUnit())
        {
            Hide();
            return;
        }

        Show();
        SetInfo(_unit);
    }

    private void UpdateToSelected()
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

    private void Show()
    {
        _canvasGroup.alpha = 1;
    }

    private void Hide()
    {
        _canvasGroup.alpha = 0;
    }
}
