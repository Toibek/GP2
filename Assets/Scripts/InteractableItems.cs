using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class InteractableItems : MonoBehaviour
{
    [Range(1f,4f)][SerializeField] private float radius;
    [SerializeField]private LayerMask mask;
    [SerializeField] private Transform itemPosition;
    private bool reach;
    private GameObject item;

    private bool hold;
    // Update is called once per frame
    void Update()
    {
        if (Physics.SphereCast(transform.position, radius ,transform.forward,out RaycastHit hit, radius, mask))
        {
                Debug.Log(hit.collider.gameObject.name);
            if (hit.distance <= radius)
            {
                reach = true;
            }
        }
        
        if (reach)
            if (Input.GetKey(KeyCode.E)) 
            {
                hold = true;
                item = hit.collider.gameObject;
            }
        if (hold)
        {
            
             holdItem(item);
             
        }
    }

    void holdItem(GameObject traff)
    {
        traff.transform.position = itemPosition.position;
        traff.transform.rotation = this.transform.rotation;
    }

    private void OnDrawGizmos()
    {
        Handles.color = Color.green;
        reach = false;
        
        Handles.DrawWireDisc(this.transform.position,Vector3.up, radius);
    }
}