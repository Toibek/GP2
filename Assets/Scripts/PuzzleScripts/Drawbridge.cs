using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawbridge : MonoBehaviour
{
    public float TargetRot;
    private bool button0Pressed = false;
    private bool button1Pressed = false;

    void Start()
    {
        
    }

    void Update()
    {
        if(button0Pressed && button1Pressed)
        {
            
        }

        if (transform.rotation.eulerAngles.x != TargetRot)
        {
            //transform.rotation.eulerAngles
        }
        Debug.Log(transform.rotation.eulerAngles.x);
    }

    public void OnPassCondition0()
    {
        button0Pressed = true;
    }

    public void OnPassCondition1()
    {
        button1Pressed = true;
    }

    public void OnFailCondition0()
    {
        button1Pressed = false;
    }

    public void OnFailCondition1()
    {
        button1Pressed = false;
    }
}
