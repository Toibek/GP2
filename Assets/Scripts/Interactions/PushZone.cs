using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushZone : MonoBehaviour
{
    Pushable pushable;
    Montis montis;
    Coroutine pushRoutine;
    private void Start()
    {
        pushable = GetComponentInParent<Pushable>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Movement mov))
        {
            Transform child = other.transform.GetChild(0);
            if (child.TryGetComponent(out Montis m))
            {
                if (montis == null)
                {
                    montis = m;
                    if (pushRoutine == null)
                        pushRoutine = StartCoroutine(Pushing());
                }
            }
        }
    }
    IEnumerator Pushing()
    {
        pushable.PushStart();
        while (montis != null && montis.heldObject == null)
        {
            pushable.Push(pushable.transform.position - transform.position);
            yield return new WaitForFixedUpdate();
        }
        pushable.PushEnd();
        pushRoutine = null;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Movement mov))
        {
            Transform child = other.transform.GetChild(0);
            if (child.TryGetComponent(out Montis m))
            {
                if (montis == m)
                    montis = null;
            }
        }
    }
}
