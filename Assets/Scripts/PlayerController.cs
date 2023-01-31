using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    private Rigidbody rb;
    private Vector3 movementInput;
    private Coroutine MoveRoutine;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        movementInput = new(input.x, 0, input.y);
        if (MoveRoutine == null && movementInput != Vector3.zero)
        {
            MoveRoutine = StartCoroutine(MovementEnum());
        }
    }
    IEnumerator MovementEnum()
    {
        while (movementInput != Vector3.zero)
        {
            rb.velocity = movementInput * movementSpeed;
            yield return new WaitForEndOfFrame();
        }
        MoveRoutine = null;
    }
    private void OnPrimary(InputValue value)
    {
        Debug.Log("Primary " + value.Get<float>());
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
