using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] bool StartWhenConnected;
    internal GameObject Player1;
    internal GameObject Player2;

    public emptyDelegate OnGameStart;
    public emptyDelegate OnPlayerJoined;

    public delegate void emptyDelegate();
    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    public void PlayerJoined(GameObject player)
    {
        if (Player1 == null) Player1 = player;
        else if (Player2 == null) Player2 = player;
        OnPlayerJoined?.Invoke();
    }
    [ContextMenu("Start Game")]
    public void GameStart()
    {
        OnGameStart?.Invoke();
    }
}