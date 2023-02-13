using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flumine : Ability
{
    [SerializeField] private GameObject prefabOrangeBubble;
    [SerializeField] private GameObject prefabBlueBubble;
    [SerializeField] private GameObject prefabWhiteBubble;
    [SerializeField] private GameObject prefabGreenBubble;
    private Rigidbody rb;

    private List<Rigidbody> pickableObjects;
    private List<Interactable> nearbyInteractables;
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
            nearbyLapides[i].SetAwakeState(false);
        }
    }
    public override void Secondary()
    {
        if (nearbyInteractables != null && nearbyInteractables.Count > 0)
        {
            GameObject go = Instantiate(prefabWhiteBubble, transform.position, Quaternion.identity);
            go.GetComponent<SphereEffect>().Run(GetComponent<SphereCollider>().bounds.size.x);
            for (int i = 0; i < nearbyInteractables.Count; i++)
            {
                nearbyInteractables[i].Interact();
            }
        }
        else
        {
            GameObject go = Instantiate(prefabGreenBubble, transform.position, Quaternion.identity);
            go.GetComponent<SphereEffect>().Run(GetComponent<SphereCollider>().bounds.size.x);

            for (int i = 0; i < nearbyLapides.Count; i++)
            {
                nearbyLapides[i].SetTamedState(true, transform);
            }
        }
    }
    public override void Tertiary()
    {
        GameObject go = Instantiate(prefabOrangeBubble, transform.position, Quaternion.identity);
        go.GetComponent<SphereEffect>().Run(GetComponent<SphereCollider>().bounds.size.x);
        for (int i = 0; i < nearbyLapides.Count; i++)
        {
            nearbyLapides[i].SetAwakeState(true);

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
            if (nearbyInteractables == null) nearbyInteractables = new();
            if (!nearbyInteractables.Contains(interactable))
                nearbyInteractables.Add(interactable);
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
            if (nearbyInteractables == null) nearbyInteractables = new();
            if (nearbyInteractables.Contains(interactable))
                nearbyInteractables.Remove(interactable);
        }
    }
}
