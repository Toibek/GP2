using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

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
    private GameObject selectedItem;
    private Vector3 point;
    

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

    private void FixedUpdate()
    {
        if (queueList.Count > 0)
        {
            if (selectedItem == null)
            { 
                selectedItem = queueList.First(); selectedItem.SetActive(true);
                point.x = Random.Range(-checkPointSaver.transform.localScale.x/2f, checkPointSaver.transform.localScale.x/2f);
                point.y = checkPointSaver.transform.position.y;
                point.z = Random.Range(-checkPointSaver.transform.localScale.z/2f, checkPointSaver.transform.localScale.z/2f);

                selectedItem.transform.position = checkPointSpawn + point;
                selectedItem.GetComponent<Rigidbody>().velocity = Vector3.zero;
                selectedItem.GetComponent<Liftable>().flying = false;
                queueList.Remove(selectedItem);
                StartCoroutine(Countdown2());
            
            }
        }
    }

    public void LoadLastCheckpoint(GameObject deadObject)
    {
        queueList.Add(deadObject);
        deadObject.SetActive(false);
        //Play respawn sound
        AudioManager.S_PlayOneShotSound(20);
    }

    private IEnumerator Countdown2()
    {
        
        yield return new WaitForSeconds(2);
        selectedItem = null;
    }
}
