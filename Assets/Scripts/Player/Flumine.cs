using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flumine : Ability
{
    private Rigidbody rb;

    private List<Rigidbody> pickableObjects;
    private void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
    }
    public override void Primary()
    {

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

    public override void Tertiary()
    {

    }
}
