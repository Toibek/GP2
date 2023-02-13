using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        rb = GetComponentInChildren<Rigidbody>();
    }
    private IEnumerator MoveEnum()
    {
        while (moving != Vector2.zero)
        {
            Debug.Log(settings);
            rb.AddForce(new Vector3(moving.x, 0, moving.y) * settings.MovementAcceleration);
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, settings.MovementSpeed);
            if (rb.velocity.magnitude >= 0.1f)
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(rb.velocity, Vector3.up), 360 * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }
        moveRoutine = null;
    }
}
