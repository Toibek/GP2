using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointScript : MonoBehaviour
{
    [Header("Does this point kill the entity?")]
    [SerializeField]private bool kill;
    
    
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

        if (other.CompareTag("Player") || other.CompareTag("Pebble"))
        {
            string type = other.tag;
            switch (kill)
            {
                case true:
                    CheckpointManager.instance.LoadLastCheckpoint(other.gameObject, type);
                    break;

                case false:
                    switch (isPebblepoint)
                    {
                        case false:
                            CheckpointManager.instance.UpdateCheckPoint(gameObject, indOffsetX, indOffsetY, indOffsetZ,
                                useIndividualHeightOffset);
                            break;
                        case true:
                            Debug.Log(other.tag);
                            CheckpointManager.instance.UpdatePebblePoint(gameObject);
                            break;
                    }
                    break;
            }
        }
    }
}
