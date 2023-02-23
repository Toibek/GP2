using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField] bool StartWhenConnected;
    internal GameObject Player1;
    internal GameObject Player2;

    public emptyDelegate OnGameStart;
    public emptyDelegate OnPlayerJoined;
    public emptyDelegate OnPause;
    public emptyDelegate OnResume;

    // Flag to check if the game is paused.
    public bool GameIsPaused = false;

    // The Pause Menu UI GameObject.
    public GameObject PauseMenuUI;

    public delegate void emptyDelegate();
    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
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
    public void Pause()
    {
        // Toggle pause state and activate/deactivate the Pause Menu UI.
        if (GameIsPaused)
        {
            Resume();
        }
        else
        {
            IngamePause();
        }
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        OnResume?.Invoke();
    }

    // Pause the game and activate the Pause Menu UI.
    public void IngamePause()
    {
        Time.timeScale = 0f;
        GameIsPaused = true;
        OnPause?.Invoke();
    }

    // Quit the game.
    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
}