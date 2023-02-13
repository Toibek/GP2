using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointScript : MonoBehaviour
{
    [SerializeField] private bool useIndividualHeightOffset;
    [SerializeField] private float individualHeightOffset;
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
                  CheckpointManager.instance.UpdateCheckPoint(gameObject, individualHeightOffset, useIndividualHeightOffset);
                  break;
            }
    }
}
