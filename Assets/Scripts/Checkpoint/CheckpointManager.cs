using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class CheckpointManager : MonoBehaviour
{
    
    //Player offsets
    [SerializeField] private float globalOffsetX;
    [SerializeField] private float globalOffsetY;
    [SerializeField] private float globalOffsetZ;
    //Manager
    public static CheckpointManager instance;
    //Lists
    private List<GameObject> checkPointList;
    private List<GameObject> queueList;
    //Checkpoint tracking
    private GameObject checkPointSaver;
    private GameObject selectedItem;
    private Vector3 checkPointSpawn;
    private Vector3 point;
    

    private void Awake()
    {
        //Creates a list so that pebbles spawn in a queue
        queueList = new List<GameObject>(); 
        //Creates a list for checkpoints
        checkPointList = new List<GameObject>();
        //Creates an instance to prevent two managers in the scene
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void UpdateCheckPoint(GameObject newCheckpoint, float offsetX,float offsetY,float offsetZ, bool indOffset)
    {
        //If the new Checkpoint hasn't been activated before, add it to the list and make it the newest checkpoint
        if (!checkPointList.Contains(newCheckpoint))
        {
            
            //Adds checkpoint to the list
            checkPointList.Add(newCheckpoint);
            //Sets the current checkpoint variable to the new object
            checkPointSaver = newCheckpoint;
            checkPointSpawn = checkPointSaver.transform.position;
            switch (indOffset)
            {
                //Offsets based on an individually set offset on the trigger
                case true:
                    checkPointSpawn.x += offsetX;
                    checkPointSpawn.y += offsetY;
                    checkPointSpawn.z += offsetZ;
                    break;
                //Sets the standard offset for all checkpoints
                case false:
                    checkPointSpawn.x += globalOffsetX;
                    checkPointSpawn.y += globalOffsetY;
                    checkPointSpawn.z += globalOffsetZ;
                    break;
            }
        }
    }

    private void FixedUpdate()
    {
        //Activates if there are pebbles in the list
        if (queueList.Count > 0)
        {
            //A check to prevent the script from repeating while the timer is being executed
            if (selectedItem == null)
            { 
                //Selects the first pebble in the queue and sets it to active
                selectedItem = queueList.First(); 
                selectedItem.SetActive(true);
                //Generates a random position within the checkpoint trigger
                point.x = Random.Range(-checkPointSaver.transform.localScale.x/2f, checkPointSaver.transform.localScale.x/2f);
                point.y = checkPointSaver.transform.position.y;
                point.z = Random.Range(-checkPointSaver.transform.localScale.z/2f, checkPointSaver.transform.localScale.z/2f);

                selectedItem.transform.position = checkPointSpawn + point;
                //Stops the items movement
                selectedItem.GetComponent<Rigidbody>().velocity = Vector3.zero;
                //If the pebble was thrown, it stops flying 
                selectedItem.GetComponent<Liftable>().flying = false;
                queueList.Remove(selectedItem);
                //Starts the delay timer and sets "selectedItem" to null
                StartCoroutine(Countdown2());
            }
        }
    }

    //Loading system for players
    public void LoadLastPlayerPosition(GameObject playerCharacter)
    {
        //Places the player on the latest checkpoint
        playerCharacter.transform.position = checkPointSpawn;
    }
    
    //Adds the pebble to a queue list
    public void AddPebbleToList(GameObject deadObject)
    {
        queueList.Add(deadObject);
        //Deactivates the pebble while it's in queue to make it invisable
        deadObject.SetActive(false);
        //Play respawn sound
        AudioManager.S_PlayOneShotSound(20);
    }

    private IEnumerator Countdown2()
    {
        //Waits two real life seconds
        yield return new WaitForSeconds(2);
        //Sets "selectedItem" to null to let the next pebble spawn
        selectedItem = null;
    }
}
