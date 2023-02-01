using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] float Speed;


    private Coroutine moveRoutine;
    private Rigidbody rb;
    private Vector2 input;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnMove(InputValue value)
    {
        input = value.Get<Vector2>();
        if (moveRoutine == null && input != Vector2.zero) moveRoutine = StartCoroutine(MoveEnum());
    }
    private IEnumerator MoveEnum()
    {
        while (input != Vector2.zero)
        {
            rb.velocity = new Vector3(input.x, 0, input.y) * Speed;
            yield return new WaitForFixedUpdate();
        }
        moveRoutine = null;
    }
}
