using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointScript : MonoBehaviour
{
    [Header("Is a checkpoint for Pebbles")]
    [SerializeField] private bool isPebblepoint;
    
    [Header("Offset for player checkpoints")]
    [SerializeField] private bool useIndividualHeightOffset;
    [SerializeField] private float indOffsetX;
    [SerializeField] private float indOffsetY;
    [SerializeField] private float indOffsetZ;
    private void OnTriggerEnter(Collider other)
    {
        if(other.isTrigger)
            return;

        if (other.CompareTag("Player"))
            CheckpointManager.instance.UpdateCheckPoint(gameObject, indOffsetX, indOffsetY, indOffsetZ, useIndividualHeightOffset);
        
        
        
    }
}

