using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    
    //Player
    [SerializeField] private float globalOffsetX;
    [SerializeField] private float globalOffsetY;
    [SerializeField] private float globalOffsetZ;
    public static CheckpointManager instance;
    private List<GameObject> checkPointList;
    
    public GameObject startingCheckpoint;
    private Vector3 checkPointSpawn;
    
    //Pebble
    private List<GameObject> pebblePointList;
    private GameObject currentPebble;
    private Vector3 pebblePointPosition;

    private void Awake()
    {
        checkPointList = new List<GameObject>();
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    private void Start()
    {
        checkPointList.Add(startingCheckpoint);
        checkPointSpawn = startingCheckpoint.transform.position;
    }

    public void UpdateCheckPoint(GameObject newCheckpoint, float offsetX,float offsetY,float offsetZ, bool indOffset)
    {
        //If the new Checkpoint hasn't been activated before, add it to the list and make it the newest checkpoint
        if (!checkPointList.Contains(newCheckpoint))
        {
            //Adds checkpoint to the list
            checkPointList.Add(newCheckpoint);
            //Sets the current checkpoint variable to the new object
            checkPointSpawn = newCheckpoint.transform.position;
            switch (indOffset)
            {
                case true:
                    
                    checkPointSpawn.x += offsetX;
                    checkPointSpawn.y += offsetY;
                    checkPointSpawn.z += offsetZ;
                    break;
                case false:
                    checkPointSpawn.x += globalOffsetX;
                    checkPointSpawn.y += globalOffsetY;
                    checkPointSpawn.z += globalOffsetZ;
                    
                    break;
            }
        }
    }

    public void UpdatePebblePoint(GameObject newPebblepoint)
    {
        if (!pebblePointList.Contains(newPebblepoint))
        {
            pebblePointList.Add(newPebblepoint);
            currentPebble = newPebblepoint;
            pebblePointPosition = currentPebble.transform.position;
            Debug.Log("1");
        }
    }
    
    public void LoadLastCheckpoint(GameObject player, string isPebble)
    {
        switch (isPebble)
        {
            case "Pebble":
                player.transform.position = pebblePointPosition;
                break;
            case "Player":
                player.transform.position = checkPointSpawn;
                break;
        }
    }
}
