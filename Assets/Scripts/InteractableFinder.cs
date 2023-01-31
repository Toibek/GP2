using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableFinder : MonoBehaviour
{

    internal Interactable closest;
    private List<Interactable> nearby;
    private void OnTriggerEnter(Collider other)
    {
        if (nearby == null) nearby = new();
        if (other.TryGetComponent(out Interactable oth))
        {
            if (!nearby.Contains(oth)) nearby.Add(oth);
            else return;

            if (nearby.Count == 1) closest = nearby[0];
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (nearby == null) return;
        if (other.TryGetComponent(out Interactable oth))
        {
            if (nearby.Contains(oth)) nearby.Remove(oth);
            else return;
            if (closest != oth) return;
            else if (nearby.Count == 1) closest = nearby[0];
            else findClosest();
        }
    }
    void findClosest()
    {
        Interactable intr = null;
        float distance = Mathf.Infinity;
        for (int i = 0; i < nearby.Count; i++)
        {
            float dis = Vector3.Distance(nearby[i].transform.position, transform.position);
            if (dis < distance)
            {
                distance = dis;
                intr = nearby[i];
            }
        }
        closest = intr;
    }
}
