using UnityEngine;

public class EnableDisableOnTrigger : MonoBehaviour
{
    public GameObject objectToToggleForFlumine;
    public GameObject objectToToggleForMothis;

    private GameObject Player1 => GameManager.Instance.Player1;
    private GameObject Player2 => GameManager.Instance.Player2;

    // This method is called when a collider enters the trigger zone
    private void OnTriggerEnter(Collider other)
    {
        if (GameManager.Instance == null)
        {
            Debug.LogError("Could not find gamemanager in the scene!");
            return;
        }
        // Check if the entering collider is tagged as a "Player"
        if (other.gameObject == Player1)
        {
            // Set the active state of the objectToToggle to true
            objectToToggleForFlumine.SetActive(true);
        }
        if (other.gameObject == Player2)
        {
            // Set the active state of the objectToToggle to true
            objectToToggleForMothis.SetActive(true);
        }
    }

    // This method is called when a collider exits the trigger zone
    private void OnTriggerExit(Collider other)
    {
        if (GameManager.Instance == null)
        {
            Debug.LogError("Could not find gamemanager in the scene!");
            return;
        }
        // Check if the exiting collider is tagged as a "Player"
        if (other.gameObject == Player1)
        {
            // Set the active state of the objectToToggle to true
            objectToToggleForFlumine.SetActive(false);
        }
        if (other.gameObject == Player2)
        {
            // Set the active state of the objectToToggle to true
            objectToToggleForMothis.SetActive(false);
        }
    }
}
