using UnityEngine;
using UnityEngine.InputSystem;

public class EnableDisableOnTrigger : MonoBehaviour
{
    // Public variables to hold the objects that will be toggled
    public GameObject ToggleKeyboardFlumine;
    public GameObject ToggleKeyboardMothis;
    public GameObject ToggleControlerMothis;
    public GameObject ToggleControlerFlumine;

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
        if (other.gameObject == Player1 && other.GetComponentInParent<PlayerInput>().currentControlScheme.Contains("Keyboard"))
        {
            // If it is, set the active state of objectToToggleForFlumine to true
            ToggleKeyboardFlumine.SetActive(true);
        }
        if (other.gameObject == Player2 && other.GetComponentInParent<PlayerInput>().currentControlScheme.Contains("Keyboard"))
        {
            // If it is, set the active state of objectToToggleForFlumine to true
            ToggleKeyboardMothis.SetActive(true);
        }
        if (other.gameObject == Player2 && !other.GetComponentInParent<PlayerInput>().currentControlScheme.Contains("Keyboard"))
        {
            // If it is, set the active state of objectToToggleForFlumine to true
            ToggleControlerMothis.SetActive(true);
        }
        if (other.gameObject == Player1 && !other.GetComponentInParent<PlayerInput>().currentControlScheme.Contains("Keyboard"))
        {
            // If it is, set the active state of objectToToggleForFlumine to true
            ToggleControlerFlumine.SetActive(true);
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

        if (other.gameObject == Player1 && other.GetComponentInParent<PlayerInput>().currentControlScheme.Contains("Keyboard"))
        {
            // If it is, set the active state of objectToToggleForFlumine to true
            ToggleKeyboardFlumine.SetActive(false);
        }
        if (other.gameObject == Player2 && other.GetComponentInParent<PlayerInput>().currentControlScheme.Contains("Keyboard"))
        {
            // If it is, set the active state of objectToToggleForFlumine to true
            ToggleKeyboardMothis.SetActive(false);
        }
        if (other.gameObject == Player2 && !other.GetComponentInParent<PlayerInput>().currentControlScheme.Contains("Keyboard"))
        {
            // If it is, set the active state of objectToToggleForFlumine to true
            ToggleControlerMothis.SetActive(false);
        }
        if (other.gameObject == Player1 && !other.GetComponentInParent<PlayerInput>().currentControlScheme.Contains("Keyboard"))
        {
            // If it is, set the active state of objectToToggleForFlumine to true
            ToggleControlerFlumine.SetActive(false);
        }
    }
}