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
>>>> ORIGINAL //FG22_GP2_North_Team05/Assets/Scripts/Checkpoint/CheckpointManager.cs#8
    [SerializeField]private List<GameObject> checkPointList;
==== THEIRS //FG22_GP2_North_Team05/Assets/Scripts/Checkpoint/CheckpointManager.cs#9
    private List<GameObject> checkPointList;
==== YOURS //max.palsson_GP2v2/FG22_GP2_North_Team05/Assets/Scripts/Checkpoint/CheckpointManager.cs
    [SerializeField]private List<GameObject> checkPointList;
    private GameObject checkPointSaver;
<<<<
    private Vector3 checkPointSpawn;
    [SerializeField]private List<GameObject> queueList;
    

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

    public void LoadLastCheckpoint(GameObject layer)
    {
>>>> ORIGINAL //FG22_GP2_North_Team05/Assets/Scripts/Checkpoint/CheckpointManager.cs#8
        switch (tag)
        {
            case"Player":
                layer.transform.position = checkPointSpawn;
                break;
            case "Pebble":
                queueList.Add(layer);
                StartCoroutine(Countdown2(layer));
                break;
        }
==== THEIRS //FG22_GP2_North_Team05/Assets/Scripts/Checkpoint/CheckpointManager.cs#9
        queueList.Add(layer);
        StartCoroutine(Countdown2(layer));
==== YOURS //max.palsson_GP2v2/FG22_GP2_North_Team05/Assets/Scripts/Checkpoint/CheckpointManager.cs
        switch (tag)
        {
            case"Player":
                layer.transform.position = checkPointSpawn;
                Debug.Log(layer.name);
                Debug.Log(tag);
                Debug.Log(layer.transform.position);
                Debug.Log(checkPointSpawn);
                break;
            case "Pebble":
                queueList.Add(layer);
                StartCoroutine(Countdown2(layer));
                break;
        }
<<<<
        
    }

    private IEnumerator Countdown2(GameObject queue)
    {
        //Can be fixed with putting it in update
        while (!queueList.Contains(null))
        {
            yield return new WaitForSeconds(1);
            if (queueList.IndexOf(queue) == 0)
            {
                queue.transform.position = checkPointSpawn;
                queueList.Remove(queue);
                break;    
            }
            yield return new WaitForSeconds(5);
            
        }
    }
}
