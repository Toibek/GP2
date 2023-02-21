using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    internal CharacterSO settings;
    internal Vector3 lookPoint;
    internal bool disabled;

    [SerializeField] float jumpForce;
    [SerializeField] LayerMask groundingLayers;
    Rigidbody rb;
    private Coroutine moveRoutine;
    Vector3 velocity;
    public Vector2 Move
    {
        set { moving = value; if (moveRoutine == null) moveRoutine = StartCoroutine(MoveEnum()); }
    }
    private Vector2 moving;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        //fu im lazy <3
        Vector3 lookDir;
        if (lookPoint != Vector3.zero)
        {
            lookDir = lookPoint - transform.position;
        }
        else
        {
            lookDir = velocity;
        }
        lookDir = new(lookDir.x, 0, lookDir.z);

        if (lookDir.magnitude >= 0.1f)
        {
            transform.rotation = Quaternion.LookRotation(lookDir, Vector3.up);
        }
    }
    public void Jump()
    {
        if (disabled) return;
        if (Physics.Raycast(transform.position, Vector3.down, 1.5f, groundingLayers))
            rb.AddForce(0, jumpForce, 0);
    }
    private IEnumerator MoveEnum()
    {
        while (moving != Vector2.zero || velocity != Vector3.zero)
        {
            yield return new WaitForFixedUpdate();
            if (disabled) continue;
            if (moving != Vector2.zero)
                velocity += new Vector3(moving.x, 0, moving.y) * settings.MovementAcceleration * Time.deltaTime;
            else
                velocity -= Vector3.ClampMagnitude(velocity.normalized, velocity.magnitude) * settings.MovementDeceleration * Time.deltaTime;
            velocity = Vector3.ClampMagnitude(velocity, settings.MovementSpeed);
            rb.velocity = new(velocity.x, rb.velocity.y, velocity.z);
        }
        moveRoutine = null;
    }
}
