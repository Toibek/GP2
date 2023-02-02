using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour
{
    private GameObject play1;
    private GameObject play2;
    private Vector3 startPosition;
    private Vector3 currentPosition;
    private Vector3 jointPosition;
    private void Awake()
    {
        startPosition = transform.position;
        currentPosition = startPosition;
         play1 = GameObject.Find("Player 1");
         play2 = GameObject.Find("Player 2");
    }

    private void Update()
    {
        jointPosition = (play1.transform.position + play2.transform.position) / 2;
        currentPosition.x = jointPosition.x + startPosition.x;
        currentPosition.z = jointPosition.z + startPosition.x;
        transform.position = currentPosition;
        transform.LookAt(jointPosition);
        Debug.Log(play1.transform.position.ToString());
    }
}
