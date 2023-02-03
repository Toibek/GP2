using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour
{
    //Variable to limit how high the camera can go in the scene
    [SerializeField] private float MaxHeight;
    
    //Player objects
    private GameObject player1;
    private GameObject player2;
    private GameObject herd;
    
    //Object positions
    private Vector3 play1Pos;
    private Vector3 play2Pos;
    private Vector3 herdPos;

    //Camera positions
    private Vector3 startPosition;
    private Vector3 currentPosition;
    private Vector3 jointPosition;
    private void Awake()
    {
        //Used by the code to remember the original position of the camera
        startPosition = transform.position;
        
        //Helps find the player related objects in the scene
        player1 = GameObject.Find("Player 1");
        player2 = GameObject.Find("Player 2");
        herd = GameObject.Find("Herd");
    }

    private void Update()
    {
        SetPositionValue();
        SetCameraPosition();
    }

    //This function is used to shorten the code while also keeping track of the objects position
    void SetPositionValue()
    {
        //Transform.position can't be used while adding value to a variable, so I made three Vector3's of the instead.
        play1Pos = player1.transform.position;
        play2Pos = player2.transform.position;
        herdPos = herd.transform.position;
    }
    
    //This function controls and calculates where the camera is supposed to be and where it's going go.
    void SetCameraPosition()
    {
        //Calculates a point where the camera is supposed to look that is more focused on the herd, rather than the players.
        jointPosition = herdPos + (play2Pos + play1Pos - (herdPos * 2)) / 5;
        currentPosition.x = jointPosition.x;
        currentPosition.z = jointPosition.z + startPosition.z;
        //Makes the camera tilt up and down based on the distance between the players and the cube
        currentPosition.y = startPosition.y + ((Vector3.Distance(herdPos, play2Pos) + Vector3.Distance(play1Pos,herdPos)) /5);
        
        //A coded roof that keeps the camera from going to high
        if (currentPosition.y > MaxHeight)
            currentPosition.y = MaxHeight;
        //Puts the camera in the right place and makes it look at the calculated point that frame
        transform.position = currentPosition;
        transform.LookAt(jointPosition);
    }
}
