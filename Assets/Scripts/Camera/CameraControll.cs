using UnityEngine;
using UnityEngine.AI;
using Plane = UnityEngine.Plane;
using Vector3 = UnityEngine.Vector3;

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
    private Vector3 heightDifferenceP1;
    private Vector3 heightDifferenceP2;
    private Vector3 velocity = Vector3.zero;
    

    //Camera positions
    private Vector3 startPosition;
    private Vector3 currentPosition;
    private Vector3 jointPosition;
    private Vector3 savePosition;
    private Vector3 lookAtPosition;
    private Renderer rend;
    private Renderer rend2;
    private NavMeshData _data;

    //Game Manager
    private GameManager gm;
    private void Start()
    {
        gm = GameManager.Instance;
        gm.OnPlayerJoined += GetPlayers;
        playerCount = 0;
        
        rend = player1.GetComponent<Renderer>();
        rend2 = player2.GetComponent<Renderer>();
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
        Physics.Raycast(ray, out RaycastHit hit);
        NavMesh.SamplePosition(hit.point, out NavMeshHit navHit, 1f, NavMesh.AllAreas);
        if (navHit.hit)
            savePosition = navHit.position;

        if (!VisibleFromCamera(rend, Camera.main))
        {
            play1Pos = savePosition;
            play1Pos.y += 2;
            player1.transform.position = play1Pos;
        }
        
        if (!VisibleFromCamera(rend2, Camera.main))
        {
            play2Pos = savePosition;
            play2Pos.y += 2;
            player2.transform.position = play2Pos;
        }
    }

    //This function controls and calculates where the camera is supposed to be and where it's going go.
    void SetCameraPosition()
    {
        switch (playerCount)
        {
            case 1:
                lookAtPosition = play1Pos;
                currentPosition.x = play1Pos.x;
                currentPosition.y = startPosition.y + play1Pos.y;
                currentPosition.z = startPosition.z + play1Pos.z;
                break;
            
            
            case 2:
                jointPosition = (play1Pos + play2Pos)/2;
                    lookAtPosition = Vector3.SmoothDamp(lookAtPosition ,jointPosition,ref velocity, 0.5f);
                currentPosition.x = jointPosition.x;
                currentPosition.z = jointPosition.z + startPosition.z; 
                //Makes the camera tilt up and down based on the distance between the players
                heightDifferenceP1.y = play1Pos.y;
                heightDifferenceP2.y = play2Pos.y;
                currentPosition.y = (startPosition.y + (Vector3.Distance(play1Pos, play2Pos) / 5) + Vector3.Distance(heightDifferenceP1,heightDifferenceP2));
                break;
            default:
                break;
        }
        //A coded roof that keeps the camera from going to high
        if (currentPosition.y > MaxHeight)
            currentPosition.y = MaxHeight;
        //Puts the camera in the right place and makes it look at the calculated point that frame
        transform.position = Vector3.SmoothDamp(transform.position,currentPosition, ref velocity, 0.5f);
        transform.LookAt(lookAtPosition);
    }
}
