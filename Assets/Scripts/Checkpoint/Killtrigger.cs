using System;
using UnityEngine;

namespace Checkpoint
{
    public class Killtrigger : MonoBehaviour
    {
        private Montis montis;
        private CameraControll camControll;

        private void OnTriggerEnter(Collider other)
        {
            if(other.isTrigger)return;
            if (other.CompareTag("Player"))
            {
                if(montis == null || camControll == null)
                {
                    montis = FindObjectOfType<Montis>();
                    camControll = FindObjectOfType<CameraControll>();
                }
                if (montis.heldObject != null)
                {
                    montis.heldObject.position = 
                        montis.gameObject.transform.position + (montis.gameObject.transform.forward * 2);
                    montis.heldObject.GetComponent<Rigidbody>().isKinematic = false;
                    montis.heldObject.parent = null;
                    montis.heldObject = null;
                }

                if (other.gameObject.transform.GetChild(0).TryGetComponent(out montis))
                {
                    other.gameObject.transform.position = camControll.play2Pos;
                }
                else
                {
                    other.gameObject.transform.position = camControll.play1Pos;
                    other.GetComponent<Liftable>().flying = false;
                }
                other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
            if(other.CompareTag("Pebble"))
            {
                CheckpointManager.instance.LoadLastCheckpoint(other.gameObject);
            }
        }
    }
}
