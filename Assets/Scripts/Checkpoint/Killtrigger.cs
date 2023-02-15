using System;
using UnityEngine;

namespace Checkpoint
{
    public class Killtrigger : MonoBehaviour
    {
        public Montis montis;
        
        private void OnTriggerEnter(Collider other)
        {
            if(other.isTrigger)return;
            if(montis == null)
                montis = FindObjectOfType<Montis>();
            if (other.CompareTag("Player"))
            {
                if (montis.heldObject != null)
                {
                    montis.heldObject.position = 
                        montis.gameObject.transform.position + (montis.gameObject.transform.forward * 2);
                    montis.heldObject.GetComponent<Rigidbody>().isKinematic = false;
                    montis.heldObject.parent = null;
                    montis.heldObject = null;
                }
                CheckpointManager.instance.LoadLastCheckpoint(other.gameObject, other.tag);
            }
            if(other.CompareTag("Pebble"))
            {
                CheckpointManager.instance.LoadLastCheckpoint(other.gameObject, other.tag);
            }
        }
    }
}
