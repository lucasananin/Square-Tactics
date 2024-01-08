using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Utilities.Audio;

public class UnitActionSystem : Singleton<UnitActionSystem>
{
    [SerializeField] bool _canSelectUnitByInput = false;
    [SerializeField] LayerMask _unitsLayerMask = default;
    [SerializeField, ReadOnly] Unit _selectedUnit = null;
    [SerializeField, ReadOnly] BaseAction _selectedAction = null;
    [SerializeField, ReadOnly] bool _isBusy = false;

    [Title("// Audio")]
    [SerializeField] AudioDataSO _takeActionAudio = null;

    //[Title("// Debug")]
    //[SerializeField] Collider2D _collider2D = null;

    public event System.Action onSelectedUnitChanged = null;
    public event System.Action onSelectedActionChanged = null;
    public event System.Action<bool> onBusyStateChanged = null;

    public bool CanSelectUnitByInput { get => _canSelectUnitByInput; private set => _canSelectUnitByInput = value; }

    private void Update()
    {
        //Debug.Log($"// {LevelGrid.Instance.GetGridPosition(MouseWorld.Instance.GetHitPoint())}");

        if (_isBusy) return;
        if (EventSystem.current.IsPointerOverGameObject()) return;
        if (!TurnSystem.Instance.IsPlayerTurn()) return;

        if (_canSelectUnitByInput && InputManager.Instance.HasPressedSelectionButtonDown())
        {
            HandleUnitSelection();
        }

        HandleSelectedAction();
    }

    private void HandleUnitSelection()
    {
        Ray _ray = Camera.main.ScreenPointToRay(InputManager.Instance.GetMousePosition());
        Physics.Raycast(_ray, out RaycastHit _hitInfo, float.MaxValue, _unitsLayerMask);
        Unit _unit = _hitInfo.collider?.GetComponent<Unit>();

        if (_unit != null && _unit.IsEnemy()) return;

        SelectUnit(_unit);

        //Unit _lastUnitSelected = _selectedUnit;
        //_selectedUnit = _unit;

        //if (_selectedUnit != _lastUnitSelected)
        //{
        //    SetSelectedAction(HasUnitSelected() ? _unit.GetAction<MoveAction>() : null);
        //    onSelectedUnitChanged?.Invoke();
        //}
    }

    public void SelectUnit(Unit _unit)
    {
        Unit _lastUnitSelected = _selectedUnit;
        _selectedUnit = _unit;

        if (_selectedUnit != _lastUnitSelected)
        {
            SetSelectedAction(HasUnitSelected() ? _unit.GetAction<MoveAction>() : null);
            onSelectedUnitChanged?.Invoke();
        }
    }

    private void HandleSelectedAction()
    {
        if (!HasUnitSelected()) return;
        //if (_collider2D.bounds.Contains(Input.mousePosition)) return;

        if (InputManager.Instance.HasPressedActionButtonDown())
        {
            GridPosition _mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.Instance.GetHitPoint());
            //bool _hasEnoughActionPoints = _selectedUnit.TrySpendActionPointsToTakeAction(_selectedAction);
            bool _hasEnoughActionPoints = _selectedUnit.CanSpendActionPointsToTakeAction(_selectedAction);
            bool _isValidGridPosition = _selectedAction.IsValidGridPosition(_mouseGridPosition);

            if (_hasEnoughActionPoints && _isValidGridPosition)
            {
                SetBusy();
                _selectedUnit.SpendActionPoints(_selectedAction);
                _selectedAction.TakeAction(_mouseGridPosition, ClearBusy);

                if (TurnSystem.Instance.IsPlayerTurn())
                {
                    _takeActionAudio?.PlayAsSfx();
                }
            }

            //switch (_selectedAction)
            //{
            //    case MoveAction:
            //        var _mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.Instance.GetHitPoint());

            //        if (_selectedUnit.GetMoveAction().IsValidGridPosition(_mouseGridPosition))
            //        {
            //            SetBusy();
            //            _selectedUnit.GetMoveAction().Move(_mouseGridPosition, ClearBusy);
            //        }
            //        break;
            //    case SpinAction:
            //        SetBusy();
            //        _selectedUnit.GetComponent<SpinAction>().Spin(ClearBusy);
            //        break;
            //    default:
            //        break;
            //}
        }
    }

    public Unit GetSelectedUnit()
    {
        return _selectedUnit;
    }

    public BaseAction GetSelectedAction()
    {
        return _selectedAction;
    }

    public bool HasUnitSelected()
    {
        return _selectedUnit != null;
    }

    public void SetSelectedAction(BaseAction _baseAction)
    {
        if (_selectedAction != _baseAction)
        {
            _selectedAction = _baseAction;
            onSelectedActionChanged?.Invoke();
        }
    }

    private void SetBusy()
    {
        if (!_isBusy)
        {
            _isBusy = true;
            onBusyStateChanged?.Invoke(true);
        }
    }

    private void ClearBusy()
    {
        if (_isBusy)
        {
            _isBusy = false;
            onBusyStateChanged?.Invoke(false);
        }
    }

    public bool IsBusy()
    {
        return _isBusy;
    }

    //public bool IsBusy()
    //{
    //    return _isBusy;
    //}
}
