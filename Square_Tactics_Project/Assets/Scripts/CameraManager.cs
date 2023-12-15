using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] GameObject _actionCamera = null;

    private void Awake()
    {
        HideActionCamera();
    }

    private void OnEnable()
    {
        BaseAction.onAnyActionStarted += SetActionCamera;
        BaseAction.onAnyActionCompleted += HideActionCamera;
    }

    private void OnDisable()
    {
        BaseAction.onAnyActionStarted -= SetActionCamera;
        BaseAction.onAnyActionCompleted -= HideActionCamera;
    }

    private void ShowActionCamera() => _actionCamera.SetActive(true);

    private void HideActionCamera() => _actionCamera.SetActive(false);

    private void SetActionCamera(object _sender, EventArgs _e)
    {
        switch (_sender)
        {
            case ShootAction _shootAction:
                //SetCameraToShoulder(_shootAction);
                break;
        }
    }

    private void HideActionCamera(object _sender, EventArgs _e)
    {
        HideActionCamera();
    }

    private void SetCameraToShoulder(ShootAction _shootAction)
    {
        Unit _shooterUnit = _shootAction.GetUnit();
        Unit _targetUnit = _shootAction.GetTargetUnit();
        Vector3 _cameraHeight = Vector3.up * 1.7f;
        Vector3 _shootDir = (_targetUnit.transform.position - _shooterUnit.transform.position).normalized;
        float _shoulderOffsetAmount = 0.5f;
        Vector3 _shoulderOffset = Quaternion.Euler(0, 90, 0) * _shootDir * _shoulderOffsetAmount;
        Vector3 _actionCameraPosition = _shooterUnit.transform.position + _cameraHeight + _shoulderOffset + (_shootDir * -1);
        _actionCamera.transform.position = _actionCameraPosition;
        _actionCamera.transform.LookAt(_targetUnit.transform.position + _cameraHeight);
        ShowActionCamera();
    }
}
