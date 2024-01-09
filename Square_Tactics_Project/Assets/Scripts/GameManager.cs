using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] bool _isPlaying = false;

    public bool IsPlaying { get => _isPlaying; set => _isPlaying = value; }
}
