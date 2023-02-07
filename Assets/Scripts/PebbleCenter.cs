using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PebbleCenter : MonoBehaviour
{
    List<Transform> pebbles;
    private void Start()
    {
        pebbles = new();
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Pebble");
        for (int i = 0; i < gos.Length; i++)
        {
            pebbles.Add(gos[i].transform);
        }
    }
    void Update()
    {
        if (pebbles.Count <= 0) return;

        Vector3 position = Vector3.zero;
        for (int i = 0; i < pebbles.Count; i++)
        {
            position += pebbles[i].position;
        }
        position /= pebbles.Count;
        transform.position = position;
    }
}
