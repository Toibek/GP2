using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catcher : Ability
{
    private Transform heldObject;
    private List<Rigidbody> pickableObjects;
    public override void AbilityStart()
    {
        heldObject = closestObject();
        if (heldObject == null) return;
        heldObject.position = transform.position + transform.up * 2;
        heldObject.parent = transform;
        heldObject.GetComponent<Rigidbody>().isKinematic = true;
    }
    public override void AbilityStop()
    {
        if (heldObject == null) return;
        heldObject.parent = null;
        heldObject.position = transform.position + transform.forward;
        heldObject.GetComponent<Rigidbody>().isKinematic = false;

    }
    private Transform closestObject()
    {
        if (pickableObjects == null || pickableObjects.Count <= 0) return null;
        Rigidbody closest = null;
        float dis = Mathf.Infinity;
        for (int i = 0; i < pickableObjects.Count; i++)
        {
            float d = Vector3.Distance(transform.position, pickableObjects[i].transform.position);
            if (d < dis)
            {
                closest = pickableObjects[i];
                dis = d;
            }
        }
        return closest.transform;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") return;
        if (other.attachedRigidbody == null) return;
        if (pickableObjects == null) pickableObjects = new();
        if (!pickableObjects.Contains(other.attachedRigidbody)) pickableObjects.Add(other.attachedRigidbody);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player") return;
        if (pickableObjects == null) pickableObjects = new();
        if (pickableObjects.Contains(other.attachedRigidbody)) pickableObjects.Remove(other.attachedRigidbody);
    }
}
