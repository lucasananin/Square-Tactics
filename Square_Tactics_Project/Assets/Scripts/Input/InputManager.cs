using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{
    private PlayerInputActions _playerInputActions = null;

    protected override void Awake()
    {
        base.Awake();
        _playerInputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        _playerInputActions.Enable();
    }

    private void OnDisable()
    {
        _playerInputActions.Disable();
    }

    public Vector2 GetMousePosition()
    {
        return Mouse.current.position.ReadValue();
        //return Input.mousePosition;
    }

    public Vector2 GetMouseScrollDelta()
    {
        return Mouse.current.scroll.ReadValue().normalized;
        //return Input.mouseScrollDelta;
    }

    public float GetRotationHorizontalAxis()
    {
        return _playerInputActions.Gameplay.CameraRotationAxis.ReadValue<float>();
        //return Mouse.current.delta.x.ReadValue();
        //return Input.GetAxis("Mouse X") * 10;
    }

    public Vector2 GetMovementAxis()
    {
        return _playerInputActions.Gameplay.CameraMovement.ReadValue<Vector2>();
    }

    //public float GetHorizontalAxis()
    //{
    //    return Input.GetAxisRaw("Horizontal");
    //}

    //public float GetVerticalAxis()
    //{
    //    return Input.GetAxisRaw("Vertical");
    //}

    public bool HasPressedActionButtonDown()
    {
        return Mouse.current.leftButton.wasPressedThisFrame;
        //return Input.GetMouseButtonDown(0);
    }

    public bool HasPressedSelectionButtonDown()
    {
        return Mouse.current.rightButton.wasPressedThisFrame;
        //return Input.GetMouseButtonDown(1);
    }

    public bool IsHoldingRotationButton()
    {
        return Mouse.current.middleButton.isPressed;
        //return Input.GetMouseButton(2);
    }
}
