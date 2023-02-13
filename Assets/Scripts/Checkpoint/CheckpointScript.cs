using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointScript : MonoBehaviour
{
    [SerializeField] private bool useIndOffset;
    [SerializeField] private float indOffsetX;
    [SerializeField] private float indOffsetY;
    [SerializeField] private float indOffsetZ;
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
                  CheckpointManager.instance.UpdateCheckPoint(gameObject,indOffsetX, indOffsetY,indOffsetZ, useIndOffset);
                  break;
            }
    }
}
