using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Montis : Ability
{
    [SerializeField] private float throwForce;
    public Transform heldObject;
    List<Liftable> liftableOjbects;

    private List<Rigidbody> pickableObjects;
    private void Start()
    {
        liftableOjbects = new();
    }
    public override void Primary()
    {
        if (heldObject == null)
        {
            var lift = ClosestLiftable();
            if (lift == null) return;
            heldObject = lift.transform;
            heldObject.position = transform.position + transform.up * 2;
            heldObject.parent = transform;
            if (heldObject.TryGetComponent(out Rigidbody rb))
            {
                rb.isKinematic = true;
            }
        }
        else
        {
            if (heldObject == null) return;
            heldObject.parent = null;
            heldObject.position = transform.position + transform.forward;
            if (heldObject.TryGetComponent(out Rigidbody rb))
            {
                rb.isKinematic = false;
            }
            heldObject = null;
        }
    }

    public override void Secondary()
    {
        if (heldObject == null) return;

        heldObject.parent = null;
        if (heldObject.TryGetComponent(out Rigidbody rb))
        {
            rb.isKinematic = false;
        }

        Vector3 direction = transform.forward;
        heldObject.transform.position = transform.position + (transform.forward * 2);
        heldObject.gameObject.GetComponent<Liftable>().Throw(direction * throwForce);

        heldObject = null;

    }
    private Liftable ClosestLiftable()
    {
        float d = Mathf.Infinity;
        Liftable closest = null;
        for (int i = 0; i < liftableOjbects.Count; i++)
        {
            float dis = Vector3.Distance(liftableOjbects[i].transform.position, transform.position);
            if (dis < d)
            {
                d = dis;
                closest = liftableOjbects[i];
            }
        }
        return closest;
    }
    internal void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Liftable lift))
        {
            if (!liftableOjbects.Contains(lift))
                liftableOjbects.Add(lift);
        }
    }
    internal void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Liftable lift))
        {
            if (liftableOjbects.Contains(lift))
                liftableOjbects.Remove(lift);
        }
    }

    public override void Tertiary()
    {
        Debug.Log("Montis don't have a third skill, silly!");
    }
}
