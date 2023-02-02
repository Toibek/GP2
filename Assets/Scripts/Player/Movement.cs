using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    internal CharacterSO settings;

    private Coroutine moveRoutine;
    private Rigidbody rb;
    public Vector2 Move
    {
        set { moving = value; if (moveRoutine == null) moveRoutine = StartCoroutine(MoveEnum()); }
    }
    Vector2 moving;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private IEnumerator MoveEnum()
    {
        while (moving != Vector2.zero)
        {
            rb.AddForce(new Vector3(moving.x, 0, moving.y) * settings.MovementAcceleration);
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, settings.MovementSpeed);
            yield return new WaitForFixedUpdate();
        }
        moveRoutine = null;
    }
}
