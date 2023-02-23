using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Montis : Ability
{
    [SerializeField] private float throwForce;
    public Transform heldObject;
    List<Liftable> liftableOjbects;
    private Animator anim;

    private List<Rigidbody> pickableObjects;
    private void Start()
    {
        liftableOjbects = new();
        anim = GetComponentInParent<Animator>();
    }
    private void Update()
    {
        if (heldObject != null) heldObject.position = transform.position + transform.up * 1.5f + transform.forward * 1.5f;
    }
    public override void Primary()
    {
        if (heldObject == null)
        {
            var lift = ClosestLiftable();
            if (lift == null) return;

            anim.SetBool("IsHolding", true);

            heldObject = lift.transform;
            heldObject.position = transform.position + transform.up * 1.5f + transform.forward * 1.5f;
            if (heldObject.TryGetComponent(out Rigidbody rb))
            {
                rb.isKinematic = true;
            }
        }
        else
        {
            if (heldObject == null) return;
            anim.SetBool("IsHolding", false);
            heldObject.position = transform.position + transform.up * 1.5f + transform.forward * 1.5f;
            if (heldObject.TryGetComponent(out Rigidbody rb))
            {
                rb.isKinematic = false;
            }
            heldObject = null;
        }
    }

    public override void Secondary()
    {

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
        if (heldObject == null) return;

        if (heldObject.TryGetComponent(out Rigidbody rb))
        {
            rb.isKinematic = false;
        }

        anim.SetBool("IsHolding", false);
        Vector3 direction = transform.forward;
        heldObject.gameObject.GetComponent<Liftable>().Throw(direction * throwForce, transform.parent.gameObject);

        heldObject = null;
    }
}
