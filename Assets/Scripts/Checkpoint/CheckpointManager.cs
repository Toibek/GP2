using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager instance;
    private List<GameObject> checkPointList;
    public GameObject startingCheckpoint;
    private GameObject currentCheckpoint;
    public GameObject player;
    
    private void Awake()
    {
        checkPointList = new List<GameObject>();
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        currentCheckpoint = startingCheckpoint;
    }

    public void UpdateCheckPoint(GameObject newCheckpoint)
    {
        //If the new Checkpoint hasn't been activated before, add it to the list and make it the newest checkpoint
        if (!checkPointList.Contains(newCheckpoint))
        {
            //Adds checkpoint to the list
            checkPointList.Add(newCheckpoint);
            //Sets the current checkpoint variable to the new object
            currentCheckpoint = newCheckpoint;
        }
            
    }

    void LoadLastCheckpoint()
    {
        player.transform.position = currentCheckpoint.transform.position;
    }
}
