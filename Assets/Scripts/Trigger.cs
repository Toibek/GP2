using UnityEngine;
using UnityEngine.UI;

public class Trigger : MonoBehaviour
{
    public Canvas canvas;
    private void Start()
    {
        canvas = GameObject.Find("ControllsIndicator-Controller").GetComponent<Canvas>();
    }
    private void OnTriggerEnter(Collider other)
    {
        canvas.gameObject.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        canvas.gameObject.SetActive(false);
    }
}

