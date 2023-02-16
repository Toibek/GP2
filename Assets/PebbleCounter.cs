using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PebbleCounter : MonoBehaviour
{
    [SerializeField] private UnityEvent<int> OnCountChanged;
    private List<GameObject> pebbles = new();
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Pebble") return;

        if (!pebbles.Contains(other.gameObject))
        {
            pebbles.Add(other.gameObject);
            OnCountChanged?.Invoke(pebbles.Count);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "Pebble") return;

        if (pebbles.Contains(other.gameObject))
        {
            pebbles.Remove(other.gameObject);
            OnCountChanged?.Invoke(pebbles.Count);
        }
    }
}
