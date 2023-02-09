using UnityEngine;
using UnityEngine.UI;

public class Trigger : MonoBehaviour
{
    public Canvas canvas;

    private void OnTriggerEnter(Collider other)
    {
        canvas = other.GetComponentInChildren<Canvas>(includeInactive: true);

        if (canvas != null)
        {
            canvas.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        canvas = other.GetComponentInChildren<Canvas>(includeInactive: true);

        if (canvas != null)
        {
            canvas.gameObject.SetActive(false);
        }
    }
}

