using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour
{
    [SerializeField] private float MaxHight; 
    
    private GameObject play1;
    private GameObject play2;
    private GameObject herd;
    private Vector3 play1Pos;
    private Vector3 play2Pos;
    private Vector3 herdPos;

    private Vector3 startPosition;
    private Vector3 currentPosition;
    private Vector3 jointPosition;
    private void Awake()
    {
        startPosition = transform.position;
        currentPosition = startPosition;
         play1 = GameObject.Find("Player 1");
         play2 = GameObject.Find("Player 2");
         herd = GameObject.Find("Herd");
    }

    private void Update()
    {
        SetPositionValue();
        SetCameraPosition();
    }

    void SetPositionValue()
    {
        play1Pos = play1.transform.position;
        play2Pos = play2.transform.position;
        herdPos = herd.transform.position;
    }
    
    void SetCameraPosition()
    {
        jointPosition = herdPos + (play2Pos + play1Pos - (herdPos * 2)) / 5;
        currentPosition.x = jointPosition.x + startPosition.x;
        currentPosition.y = startPosition.y + ((Vector3.Distance(herdPos, play2Pos) + Vector3.Distance(play1Pos,herdPos)) /5);
        if (currentPosition.y > MaxHight)
            currentPosition.y = MaxHight;
        currentPosition.z = jointPosition.z;
        transform.position = currentPosition;
        transform.LookAt(jointPosition);
    }
}
