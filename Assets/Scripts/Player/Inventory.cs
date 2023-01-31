using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{
    private void OnPrimary(InputValue value)
    {
        if (value.Get<float>() == 0)
            PrimaryUp();
        else
            PrimaryDown();
    }
    private void PrimaryDown()
    {
        Debug.Log("Primary Down");
    }
    private void PrimaryUp()
    {
        Debug.Log("Primary Up");
    }
}
