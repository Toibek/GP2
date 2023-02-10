using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flumine : Ability
{
    [SerializeField] private GameObject prefabOrangeBubble;
    [SerializeField] private GameObject prefabBlueBubble;
    private Rigidbody rb;

    private List<Rigidbody> pickableObjects;
    private List<Interactable> nearbyInteractable;
    private List<PebbleCreature> nearbyLapides;
    private void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
    }
    public override void Primary()
    {
        GameObject go = Instantiate(prefabBlueBubble, transform.position, Quaternion.identity);
        go.GetComponent<SphereEffect>().Run(GetComponent<SphereCollider>().bounds.size.x);
        for (int i = 0; i < nearbyLapides.Count; i++)
        {
            nearbyLapides[i].SetAwakeState(true);
        }
    }
    public override void Secondary()
    {
        if (nearbyInteractable != null)
            for (int i = 0; i < nearbyInteractable.Count; i++)
            {
                nearbyInteractable[i].Interact();
            }
    }
    public override void Tertiary()
    {
        GameObject go = Instantiate(prefabOrangeBubble, transform.position, Quaternion.identity);
        go.GetComponent<SphereEffect>().Run(GetComponent<SphereCollider>().bounds.size.x);
        for (int i = 0; i < nearbyLapides.Count; i++)
        {
            nearbyLapides[i].SetAwakeState(false);

        }

    }
    internal void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PebbleCreature ai))
        {
            if (nearbyLapides == null) nearbyLapides = new();
            if (!nearbyLapides.Contains(ai))
                nearbyLapides.Add(ai);
        }
        if (other.TryGetComponent(out Interactable interactable))
        {
            if (nearbyInteractable == null) nearbyInteractable = new();
            if (!nearbyInteractable.Contains(interactable))
                nearbyInteractable.Add(interactable);
        }
    }
    internal void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PebbleCreature ai))
        {
            if (nearbyLapides == null) nearbyLapides = new();
            if (nearbyLapides.Contains(ai))
                nearbyLapides.Remove(ai);
        }
        if (other.TryGetComponent(out Interactable interactable))
        {
            if (nearbyInteractable == null) nearbyInteractable = new();
            if (nearbyInteractable.Contains(interactable))
                nearbyInteractable.Remove(interactable);
        }
    }
}
