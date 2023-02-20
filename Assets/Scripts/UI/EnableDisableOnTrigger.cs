using UnityEngine;
using UnityEngine.InputSystem;

public class EnableDisableOnTrigger : MonoBehaviour
{
    // Public variables to hold the objects that will be toggled
    public GameObject objectToToggleForKeyboard;
    public GameObject objectToToggleForControler;

    // Private properties to get the player game objects from the GameManager
    private GameObject Player1 => GameManager.Instance.Player1;
    private GameObject Player2 => GameManager.Instance.Player2;

    // This method is called when a collider enters the trigger zone
    private void OnTriggerEnter(Collider other)
    {
        // Check if the GameManager instance is null
        if (GameManager.Instance == null)
        {
            // If it is, log an error and return
            Debug.LogError("Could not find gamemanager in the scene!");
            return;
        }

        // Check if the entering collider has a keyboard
        if (other.GetComponentInParent<PlayerInput>().currentControlScheme.Contains("Keyboard"))
        {
            // If it is, set the active state of objectToToggleForFlumine to true
            objectToToggleForKeyboard.SetActive(true);
        }
        else
        {
            // If it is, set the active state of objectToToggleForMothis to true
            objectToToggleForControler.SetActive(true);
        }
    }

    // This method is called when a collider exits the trigger zone
    private void OnTriggerExit(Collider other)
    {
        // Check if the GameManager instance is null
        if (GameManager.Instance == null)
        {
            // If it is, log an error and return
            Debug.LogError("Could not find gamemanager in the scene!");
            return;
        }

        if (other.GetComponentInParent<PlayerInput>().currentControlScheme.Contains("Keyboard"))
        {
            // If it is, set the active state of objectToToggleForFlumine to true
            objectToToggleForKeyboard.SetActive(false);
        }
        else
        {
            // If it is, set the active state of objectToToggleForMothis to true
            objectToToggleForControler.SetActive(false);
        }
    }
}