using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Liftable : MonoBehaviour
{
    bool flying;
    bool hadRb;
    Vector3 direction;
    Rigidbody rb;
    IEnumerator throwEnum()
    {
        while (flying)
        {
            rb.velocity = direction;
            yield return new WaitForFixedUpdate();
        }
        while (rb.velocity.magnitude == 0)
        {
            yield return new WaitForEndOfFrame();
        }
        if (!hadRb) Destroy(rb);
        else rb.useGravity = true;
    }
    public virtual void Throw(Vector3 direction)
    {
        if (TryGetComponent(out rb))
        {
            hadRb = true;
        }
        else
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }
        this.direction = direction;
        rb.useGravity = false;
        flying = true;
        StartCoroutine(throwEnum());
    }
    private void OnCollisionEnter(Collision collision)
    {
        flying = false;
    }
}