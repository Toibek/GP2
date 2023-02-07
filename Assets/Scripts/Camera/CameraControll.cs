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
    private int playerCount;

    //Object positions
    private Vector3 play1Pos;
    private Vector3 play2Pos;
    

    //Camera positions
    private Vector3 startPosition;
    private Vector3 currentPosition;
    private Vector3 jointPosition;
    private GameManager gm;
    private Vector3 minCamPosition;
    private Renderer rend;
    private void Start()
    {
        gm = GameManager.Instance;
        gm.OnPlayerJoined += GetPlayers;
        playerCount = 0;
        
        //Used by the code to remember the original position of the camera
        startPosition = transform.position;
        
    }
    private void GetPlayers()
    {
        if (gm.Player1 != null)
        {
            player1 = gm.Player1;
            if(playerCount == 0)
                playerCount += 1;
        }

        if (gm.Player2 != null)
        {
            player2 = gm.Player2;
            playerCount += 1;
        }
    }
    private void Update()
    {
        if (!player1) return;
        SetPositionValue();
        SetCameraPosition();
        CameraRay();
    }

    //This function is used to shorten the code while also keeping track of the objects position
    void SetPositionValue()
    {
        //Transform.position can't be used while adding value to a variable, so I made three Vector3's of the instead.
        play1Pos = player1.transform.position;
        if(player2 != null)
            play2Pos = player2.transform.position;
        
    }

    static bool VisibleFromCamera(Renderer renderer, Camera camera)
    {
        Plane[] frustumPlanes = GeometryUtility.CalculateFrustumPlanes(camera);
        return GeometryUtility.TestPlanesAABB(frustumPlanes, renderer.bounds);
    }
    
    void CameraRay()
    {
        Ray ray = new Ray(currentPosition, transform.forward);
        if(Physics.Raycast(ray, out RaycastHit hit))
            minCamPosition.z = hit.point.z - 20;
        if (play1Pos.z < minCamPosition.z || play2Pos.z < minCamPosition.z)
            Debug.Log("Nope");

        rend = player1.transform.GetChild(0).GetComponent<Renderer>();
        if (!VisibleFromCamera(rend, Camera.main))
        {
            Debug.Log("Yes");
        }


    }

    //This function controls and calculates where the camera is supposed to be and where it's going go.
    void SetCameraPosition()
    {
        switch (playerCount)
        {
            case 1:
                jointPosition = play1Pos;
                currentPosition.x = play1Pos.x;
                currentPosition.y = startPosition.y;
                currentPosition.z = startPosition.z + play1Pos.z;
                break;
            case 2: 
                jointPosition = (play2Pos + play1Pos) / 2;
                currentPosition.x = jointPosition.x;
                currentPosition.z = jointPosition.z + startPosition.z; 
                //Makes the camera tilt up and down based on the distance between the players and the cube
                currentPosition.y = startPosition.y + (Vector3.Distance(play1Pos, play2Pos) / 5);
                break;
            default:
                break;
        }
        //Calculates a point where the camera is supposed to look that is more focused on the herd, rather than the players.
       

        //A coded roof that keeps the camera from going to high
        if (currentPosition.y > MaxHeight)
            currentPosition.y = MaxHeight;
        //Puts the camera in the right place and makes it look at the calculated point that frame
        transform.position = currentPosition;
        transform.LookAt(jointPosition);
    }
}
