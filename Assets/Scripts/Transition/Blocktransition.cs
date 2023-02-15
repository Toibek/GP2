using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocktransition : MonoBehaviour
{
    
    [SerializeField] private GameObject oldBlock;
    
    private Camera camera;

    private void Start()
    {
        camera = Camera.main;
    }

    private void Update()
    {
        Vector3 direction = camera.transform.position - transform.position;
        direction.y = transform.position.y;
        if(Vector3.Dot(direction,transform.forward ) > 0)
            SwitchBlock();
    }

    void SwitchBlock()
    {
        Debug.Log("f");
        oldBlock.SetActive(false);
    }
    
}
