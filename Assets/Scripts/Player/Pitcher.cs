using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pitcher : Ability
{
    [SerializeField] private float chargeTime;
    [SerializeField] private Vector2 force;
    [SerializeField] private List<Rigidbody> hittableObjects;
    private float curForce;
    private Coroutine forceRoutine;
    public override void AbilityStart()
    {
        forceRoutine = StartCoroutine(ForceCalc());
    }
    private IEnumerator ForceCalc()
    {
        while (true)
        {
            for (float f = 0; f < chargeTime; f += Time.deltaTime)
            {
                curForce = ((f / chargeTime) * (force.y - force.x)) + force.x;
                yield return new WaitForEndOfFrame();
            }
            for (float f = chargeTime; f >= 0; f -= Time.deltaTime)
            {
                curForce = ((f / chargeTime) * (force.y - force.x)) + force.x;
                yield return new WaitForEndOfFrame();
            }
            yield return new WaitForEndOfFrame();
        }
    }
    public override void AbilityStop()
    {
        StopCoroutine(forceRoutine);
        Debug.Log("Pitcher stop: " + curForce);
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
            hittableObjects[i].AddForce(direction * curForce);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") return;
        if (other.attachedRigidbody == null) return;
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
