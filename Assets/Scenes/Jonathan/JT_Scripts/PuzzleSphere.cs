using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSphere : MonoBehaviour
{
    [SerializeField] private GameObject windGust;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PuzzleSphere") 
        {
            windGust.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "PuzzleSphere")
        {
            windGust.gameObject.SetActive(false);
        }
    }
}
