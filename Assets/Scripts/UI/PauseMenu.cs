using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    // Flag to check if the game is paused.
    public bool GameIsPaused = false;

    // The Pause Menu UI GameObject.
    public GameObject PauseMenuUI;

    // The PlayerInput component for handling input.
    private PlayerInput playerInput;

    // Called when the script instance is being loaded.
    private void Awake()
    {
        // Get a reference to the PlayerInput component and subscribe to the onActionTriggered event.
        playerInput = GetComponent<PlayerInput>();
        playerInput.onActionTriggered += OnActionTriggered;
    }

    // Callback function for handling input events.
    private void OnActionTriggered(InputAction.CallbackContext context)
    {
        // Check if the "Pause" action was triggered in the "Started" phase.
        if (context.action.name == "Pause" && context.phase == InputActionPhase.Started)
        {
            // Toggle pause state and activate/deactivate the Pause Menu UI.
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    // Resume the game and deactivate the Pause Menu UI.
    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    // Pause the game and activate the Pause Menu UI.
    void Pause()
    {
        Time.timeScale = 0f;
        GameIsPaused = true;
        PauseMenuUI.SetActive(true);
    }

    // Quit the game.
    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
}