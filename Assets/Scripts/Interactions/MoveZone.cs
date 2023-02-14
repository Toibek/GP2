using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveZone : MonoBehaviour
{
    List<Transform> objectsToMove;
    public void Move(Vector3 dir)
    {
        if (objectsToMove == null) return;
        for (int i = 0; i < objectsToMove.Count; i++)
        {
            objectsToMove[i].position += (dir);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (objectsToMove == null) objectsToMove = new();
        if (!objectsToMove.Contains(other.transform))
            objectsToMove.Add(other.transform);
    }
    private void OnTriggerExit(Collider other)
    {
        if (objectsToMove.Contains(other.transform))
            objectsToMove.Remove(other.transform);
    }
}
