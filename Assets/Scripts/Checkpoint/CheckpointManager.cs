using System;
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
    private GameObject checkPointSaver;
    private Vector3 checkPointSpawn;
    private List<GameObject> queueList;
    

    private void Awake()
    {
        queueList = new List<GameObject>(); 
        checkPointList = new List<GameObject>();
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

    public void LoadLastCheckpoint(GameObject deadObject)
    {
        queueList.Add(deadObject); 
        StartCoroutine(Countdown2(deadObject));

    }

    private IEnumerator Countdown2(GameObject queue)
    {
        //Can be fixed with putting it in update
        while (!queueList.Contains(null))
        {
            yield return new WaitForSeconds(1);
            if (queueList.IndexOf(queue) == 0)
            {
                queue.GetComponent<Rigidbody>().velocity = Vector3.zero;
                queue.GetComponent<Liftable>().flying = false;
                queue.transform.position = checkPointSpawn;
                queueList.Remove(queue);
                break;    
            }
        }
    }
}
