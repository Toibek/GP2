using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardUI : MonoBehaviour
{

    private Camera _cam;

    // Start is called before the first frame update
    void Start()
    {
        _cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookAtPosition = transform.position - _cam.transform.position;
        lookAtPosition.x = 0; // ignore the y-axis
        transform.rotation = Quaternion.LookRotation(lookAtPosition);
    }

}
