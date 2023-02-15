using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public float stopDistance;

    private Vector3 startPos;
    private Transform camT;

    void Start()
    {
        startPos = transform.position;
        camT = Camera.main.transform;
    }

    void Update()
    {
        if(Vector3.Distance(startPos, camT.position) < stopDistance)
        {
            transform.position = camT.position;
        }
    }
}
