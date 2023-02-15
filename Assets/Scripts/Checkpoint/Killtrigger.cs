using System;
using UnityEngine;

namespace Checkpoint
{
    public class Killtrigger : MonoBehaviour
    {
        
        private void OnTriggerEnter(Collider other)
        {
            if(other.isTrigger)return;
            if(other.CompareTag("Player") || other.CompareTag("Pebble"))
                CheckpointManager.instance.LoadLastCheckpoint(other.gameObject);
        }
    }
}
