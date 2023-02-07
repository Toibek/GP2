using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Player1;
    public GameObject Player2;
    public GameObject Herd;
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
        Herd = GameObject.FindGameObjectWithTag("Herd");
    }
    public void PlayerJoined(GameObject player)
    {
        if (Player1 == null) Player1 = player;
        else if (Player2 == null) Player2 = player;
        OnPlayerJoined?.Invoke();
    }
}