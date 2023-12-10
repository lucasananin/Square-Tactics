using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionBusyUi : MonoBehaviour
{
    [SerializeField] GameObject _busyPanel = null;

    private void Awake()
    {
        _busyPanel.SetActive(false);
    }

    private void OnEnable()
    {
        UnitActionSystem.Instance.onBusyStateChanged += UpdateBusyPanel;
    }

    private void OnDisable()
    {
        UnitActionSystem.Instance.onBusyStateChanged -= UpdateBusyPanel;
    }

    private void UpdateBusyPanel(bool _isBusy)
    {
        _busyPanel.SetActive(_isBusy);
    }
}
