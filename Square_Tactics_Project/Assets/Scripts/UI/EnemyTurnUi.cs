using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurnUi : MonoBehaviour
{
    [SerializeField] GameObject _enemyTurnPanel = null;

    private void Awake()
    {
        _enemyTurnPanel.SetActive(false);
    }

    private void OnEnable()
    {
        TurnSystem.Instance.onTurnChanged += UpdateVisuals;
    }

    private void OnDisable()
    {
        TurnSystem.Instance.onTurnChanged += UpdateVisuals;
    }

    private void UpdateVisuals()
    {
        _enemyTurnPanel.SetActive(!TurnSystem.Instance.IsPlayerTurn());
    }
}
