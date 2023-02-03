using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pitcher : Ability
{
    [SerializeField] float force;
    [SerializeField] List<Rigidbody> hittableObjects;
    public override void AbilityStart()
    {
        for (int i = hittableObjects.Count - 1; i >= 0; i--)
        {
            if (hittableObjects[i] == null)
            {
                hittableObjects.RemoveAt(i);
                continue;
            }
            float dot = Vector3.Dot(transform.forward, hittableObjects[i].transform.position - transform.position);
            if (dot < -0.3f) continue;

            Vector3 direction = (hittableObjects[i].transform.transform.position - transform.position + new Vector3(0, 2, 0) + transform.forward).normalized;
            hittableObjects[i].AddForce(direction * force);
        }
    }
    public override void AbilityStop()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") return;
        if (hittableObjects == null) hittableObjects = new();
        if (!hittableObjects.Contains(other.attachedRigidbody)) hittableObjects.Add(other.attachedRigidbody);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player") return;
        if (hittableObjects == null) hittableObjects = new();
        if (hittableObjects.Contains(other.attachedRigidbody)) hittableObjects.Remove(other.attachedRigidbody);
    }
}
