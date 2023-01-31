using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        Debug.Log("Move: " + input);
    }
    private void OnPrimary()
    {
        Debug.Log("Primary");
    }
    private void OnSecondary()
    {
        Debug.Log("Secondary");
    }
    private void OnDrop()
    {
        Debug.Log("Drop");
    }
}
