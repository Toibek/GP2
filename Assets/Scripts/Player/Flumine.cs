using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flumine : Ability
{
    [SerializeField] float jumpForce;
    [SerializeField] LayerMask groundingLayers;
    private Rigidbody rb;

    private List<Rigidbody> pickableObjects;
    private void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
    }
    public override void Primary()
    {
        if (Physics.SphereCast(new Ray(transform.position, -transform.up), 0.5f, 0.5f, groundingLayers))
            rb.AddForce(0, jumpForce, 0);
    }

    public override void Secondary()
    {

    }

    internal void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody == null) return;
        if (pickableObjects == null) pickableObjects = new();
        if (!pickableObjects.Contains(other.attachedRigidbody)) pickableObjects.Add(other.attachedRigidbody);
    }
    internal void OnTriggerExit(Collider other)
    {
        if (pickableObjects == null) pickableObjects = new();
        if (pickableObjects.Contains(other.attachedRigidbody)) pickableObjects.Remove(other.attachedRigidbody);
    }
}
