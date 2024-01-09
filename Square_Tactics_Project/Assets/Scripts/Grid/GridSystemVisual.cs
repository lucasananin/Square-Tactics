using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystemVisual : MonoBehaviour
{
    [SerializeField] GridSystemVisualSingle _gridSystemVisualSinglePrefab = null;
    [SerializeField, ReadOnly] GridSystemVisualSingle[,,] _gridSystemVisualSingles = null;

    private void Start()
    {
        int _width = LevelGrid.Instance.GetWidth();
        int _height = LevelGrid.Instance.GetHeight();
        int _floorAmount = LevelGrid.Instance.GetFloorAmount();

        _gridSystemVisualSingles = new GridSystemVisualSingle[_width, _height, _floorAmount];

        for (int x = 0; x < _width; x++)
        {
            for (int z = 0; z < _height; z++)
            {
                for (int f = 0; f < _floorAmount; f++)
                {
                    GridPosition _gridPosition = new GridPosition(x, z, f);
                    Vector3 _worldPosition = LevelGrid.Instance.GetWorldPosition(_gridPosition);
                    GridSystemVisualSingle _instance = Instantiate(_gridSystemVisualSinglePrefab, _worldPosition, Quaternion.identity, transform);
                    _gridSystemVisualSingles[x, z, f] = _instance;

                    //if (Physics.Raycast(_instance.transform.position + Vector3.up, -_instance.transform.up, out RaycastHit _hitInfo, 1f))
                    //{
                    //    _instance.transform.rotation = _hitInfo.transform.rotation;
                    //}
                }
            }
        }

        HideAllGridPositions();
    }

    private void OnEnable()
    {
        UnitActionSystem.Instance.onSelectedActionChanged += UpdateVisuals;
        UnitActionSystem.Instance.onBusyStateChanged += UpdateVisuals;
        //LevelGrid.Instance.onAnyUnitMovedGridPosition += UpdateVisuals;
    }

    private void OnDisable()
    {
        UnitActionSystem.Instance.onSelectedActionChanged -= UpdateVisuals;
        UnitActionSystem.Instance.onBusyStateChanged -= UpdateVisuals;
        //LevelGrid.Instance.onAnyUnitMovedGridPosition -= UpdateVisuals;
    }

    //private void LateUpdate()
    //{
    //    UpdateVisuals();
    //}

    private void UpdateVisuals(bool _isBusy)
    {
        if (!_isBusy)
        {
            UpdateVisuals();
        }
        else
        {
            HideAllGridPositions();
        }
    }

    public void UpdateVisuals()
    {
        var _unit = UnitActionSystem.Instance.GetSelectedUnit();
        BaseAction _selectedAction = UnitActionSystem.Instance.GetSelectedAction();

        if (_selectedAction != null)
        {
            HideAllGridPositions();

            if (!_unit.HasActionPoints())
            {
                return;
            }

            if (_selectedAction.HasFadedGridVisual())
            {
                var _validFadedGridPositions = _selectedAction.GetFadedValidActionGridPositions();
                ShowGridPositions(_validFadedGridPositions, _selectedAction.GetFadedGridColorMaterial());
            }

            var _validGridPositions = _selectedAction.GetValidActionGridPositions();
            ShowGridPositions(_validGridPositions, _selectedAction.GetGridColorMaterial());
        }
        else
        {
            HideAllGridPositions();
        }
    }

    public void HideAllGridPositions()
    {
        for (int x = 0; x < LevelGrid.Instance.GetWidth(); x++)
        {
            for (int z = 0; z < LevelGrid.Instance.GetHeight(); z++)
            {
                for (int f = 0; f < LevelGrid.Instance.GetFloorAmount(); f++)
                {
                    _gridSystemVisualSingles[x, z, f].Hide();
                }
            }
        }
    }

    public void ShowGridPositions(List<GridPosition> _gridPositions, Material _material)
    {
        int _count = _gridPositions.Count;

        for (int i = 0; i < _count; i++)
        {
            GridSystemVisualSingle _gridVisual = _gridSystemVisualSingles[_gridPositions[i].x, _gridPositions[i].z, _gridPositions[i].floor];
            _gridVisual.Show();
            _gridVisual.SetMaterial(_material);
        }
    }
}
