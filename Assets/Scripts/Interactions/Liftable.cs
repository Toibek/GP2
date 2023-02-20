using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Liftable : MonoBehaviour
{
    public bool flying;
    bool hadRb;
    Vector3 direction;
    Rigidbody rb;
    GameObject thrower;
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
    public virtual void Throw(Vector3 direction, GameObject thrower)
    {
        if (TryGetComponent(out rb))
        {
            hadRb = true;
        }
        else
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }
        if (TryGetComponent(out Movement mov))
            mov.disabled = true;
        this.thrower = thrower;
        this.direction = direction;
        rb.useGravity = false;
        flying = true;
        StartCoroutine(throwEnum());
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject != thrower)
        {
            if (TryGetComponent(out Movement mov))
                mov.disabled = false;
            flying = false;
            thrower = null;
        }
    }
}