using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSystem : Singleton<TurnSystem>
{
    [SerializeField] int _turn = 1;
    [SerializeField, ReadOnly] bool _isPlayerTurn = true;

    public Action onTurnChanged = null;

    public void NextTurn()
    {
        _turn++;
        _isPlayerTurn = !_isPlayerTurn;
        onTurnChanged?.Invoke();
    }

    public int GetTurn()
    {
        return _turn;
    }

    public bool IsPlayerTurn()
    {
        return _isPlayerTurn;
    }
}
