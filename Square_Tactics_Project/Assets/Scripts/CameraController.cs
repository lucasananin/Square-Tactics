using Cinemachine;
using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Title("// General")]
    [SerializeField] CinemachineVirtualCamera _virtualCamera = null;
    [SerializeField] bool _useInputToMove = false;
    [SerializeField] bool _useSmoothMovement = true;

    [Title("// Movement")]
    [SerializeField] float _moveSpeed = 10f;
    //[SerializeField] float _selectUnitMoveDuration = 0.8f;
    [SerializeField, ReadOnly] Transform _target = null;

    [Title("// Rotation")]
    [SerializeField] float _rotationSpeed = 10f;
    [SerializeField] bool _isDragging = false;

    [Title("// Zoom")]
    [SerializeField] float _zoomSpeed = 20f;
    [SerializeField] float _minZoom = 5f;
    [SerializeField] float _maxZoom = 10;
    [SerializeField] float _rotationMinDrag = 0.2f;
    [SerializeField, ReadOnly] Vector3 _zoomDir = Vector3.zero;

    private CinemachineTransposer _transposer = null;

    private void Awake()
    {
        _transposer = _virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        _zoomDir = _transposer.m_FollowOffset;
    }

    private void OnEnable()
    {
        UnitActionSystem.Instance.onSelectedUnitChanged += MoveToUnit;
        EnemyAi.onEnemyUnitSelected += MoveToEnemyUnit;
    }

    private void OnDisable()
    {
        UnitActionSystem.Instance.onSelectedUnitChanged -= MoveToUnit;
        EnemyAi.onEnemyUnitSelected -= MoveToEnemyUnit;
    }

    private void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleZoom();
    }

    private void HandleMovement()
    {
        if (_useInputToMove)
        {
            Vector2 _movementAxis = InputManager.Instance.GetMovementAxis();
            float _inputX = _movementAxis.x;
            float _inputY = _movementAxis.y;
            Vector3 _moveVector = transform.forward * _inputY + transform.right * _inputX;
            Vector3 _velocity = _moveVector.normalized * _moveSpeed * Time.deltaTime;
            transform.position += _velocity;
        }
        else
        {
            if (_target == null) return;

            float _step = _moveSpeed * Time.deltaTime;
            Vector3 _currentVelocity = Vector3.zero;
            Vector3 _position = _useSmoothMovement ?
                Vector3.SmoothDamp(transform.position, _target.position, ref _currentVelocity, _step) :
                Vector3.MoveTowards(transform.position, _target.position, _step);

            transform.position = _position;
        }
    }

    private void HandleRotation()
    {
        float _inputX = InputManager.Instance.GetRotationHorizontalAxis();
        _isDragging = InputManager.Instance.IsHoldingRotationButton();
        //bool _isHoldingMouseButton = Input.GetMouseButton(1);
        //_isDragging = _isHoldingMouseButton && Mathf.Abs(_mouseX) > _fodase;
        //Cursor.visible = !_isDragging;
        //Cursor.lockState = _isDragging ? CursorLockMode.Locked : CursorLockMode.None;

        if (_isDragging && Mathf.Abs(_inputX) > _rotationMinDrag)
        {
            transform.eulerAngles += new Vector3(0, _inputX, 0) * _rotationSpeed * Time.deltaTime;
        }
    }

    private void HandleZoom()
    {
        Vector2 _scroll = InputManager.Instance.GetMouseScrollDelta();

        if (_scroll != Vector2.zero)
        {
            _zoomDir = _transposer.m_FollowOffset + new Vector3(0, -_scroll.y, _scroll.y);

            if (_zoomDir.y > _maxZoom)
            {
                _zoomDir = new Vector3(0, _maxZoom, -_maxZoom);
            }
            else if (_zoomDir.y < _minZoom)
            {
                _zoomDir = new Vector3(0, _minZoom, -_minZoom);
            }
        }

        if (_transposer.m_FollowOffset.y != _zoomDir.y)
        {
            float _step = _zoomSpeed * Time.deltaTime;
            _transposer.m_FollowOffset = Vector3.MoveTowards(_transposer.m_FollowOffset, _zoomDir, _step);
        }
    }

    private void MoveToUnit()
    {
        if (UnitActionSystem.Instance.HasUnitSelected())
        {
            _target = UnitActionSystem.Instance.GetSelectedUnit().transform;
            //transform.DOKill();
            //transform.DOMove(UnitActionSystem.Instance.GetSelectedUnit().transform.position, _selectUnitMoveDuration);
        }
    }

    private void MoveToEnemyUnit(Unit _enemyUnit)
    {
        _target = _enemyUnit.transform;
    }

    public void SetTargetTransform(Transform _transform)
    {
        _target = _transform;
    }
}
