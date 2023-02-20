using UnityEngine;

public class EnableDisableOnTrigger : MonoBehaviour
{
    public GameObject objectToToggle; 

    // This method is called when a collider enters the trigger zone
    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering collider is tagged as a "Player"
        if (other.CompareTag("Player"))
        {
            // Set the active state of the objectToToggle to true
            objectToToggle.SetActive(true);
        }
    }

    // This method is called when a collider exits the trigger zone
    private void OnTriggerExit(Collider other)
    {
        // Check if the exiting collider is tagged as a "Player"
        if (other.CompareTag("Player"))
        {
            // Set the active state of the objectToToggle to false
            objectToToggle.SetActive(false);
        }
    }
}
