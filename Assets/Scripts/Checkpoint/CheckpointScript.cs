using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    [SerializeField]private bool kill;
    private void OnTriggerEnter(Collider other)
    {
        if(other.isTrigger)
            return;
            
        if (other.CompareTag("Player")) 
            switch (kill)
            {
              case true:
                  CheckpointManager.instance.LoadLastCheckpoint(other.gameObject);
                  break;
            
              case false:
                  CheckpointManager.instance.UpdateCheckPoint(gameObject); 
                  break;
            }
    }
}
