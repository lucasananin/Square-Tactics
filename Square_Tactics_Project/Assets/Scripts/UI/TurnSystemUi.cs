using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurnSystemUi : MonoBehaviour
{
    [SerializeField] Button _endTurnButton = null;
    [SerializeField] TextMeshProUGUI _text = null;

    private void Start()
    {
        UpdateTurnText();
    }

    private void OnEnable()
    {
        _endTurnButton.onClick.AddListener(TurnSystem.Instance.NextTurn);
        TurnSystem.Instance.onTurnChanged += UpdateTurnText;
    }

    private void OnDisable()
    {
        _endTurnButton.onClick.RemoveAllListeners();
        TurnSystem.Instance.onTurnChanged -= UpdateTurnText;
    }

    private void UpdateTurnText()
    {
        _text.text = $"Turn <size=72>{TurnSystem.Instance.GetTurn():D2}";
    }
}
