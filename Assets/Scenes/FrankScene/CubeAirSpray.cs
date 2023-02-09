using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeAirSpray : MonoBehaviour
{
    public ParticleSystem partSystem;
    public List<ParticleCollisionEvent> collisionEvents;

    private void Start()
    {
        partSystem = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    private void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = partSystem.GetCollisionEvents(other, collisionEvents);

        Rigidbody rb = other.GetComponent<Rigidbody>();
        int i = 0;

        while(i< numCollisionEvents)
        {
            if(rb != null)
            {
                Vector3 pos = collisionEvents[i].intersection;
                Vector3 force = collisionEvents[i].velocity * 10;
                rb.AddForce(force);
                
            }

            i++;
        }
    }
}
