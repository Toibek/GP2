using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    [SerializeField] private int TutorialToShow;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        Transform parent = other.transform;
        for (int i = 0; i < parent.childCount; i++)
        {
            if (parent.GetChild(i).TryGetComponent(out Popup popup))
            {
                Debug.Log("Found it");
                popup.Show(TutorialToShow);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Transform parent = other.transform;
        for (int i = 0; i < parent.childCount; i++)
        {
            if (parent.GetChild(i).TryGetComponent(out Popup popup))
            {
                popup.Hide();
            }
        }
    }
}
