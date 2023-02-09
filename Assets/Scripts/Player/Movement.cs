using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    internal CharacterSO settings;

    [SerializeField] float jumpForce;
    [SerializeField] LayerMask groundingLayers;

    private Coroutine moveRoutine;
    private Rigidbody rb;
    public Vector2 Move
    {
        set { moving = value; if (moveRoutine == null) moveRoutine = StartCoroutine(MoveEnum()); }
    }
    private Vector2 moving;
    private void Start()
    {
        rb = GetComponentInChildren<Rigidbody>();
    }

    private void FixedUpdate()
    {
        //fu im lazy <3
        if (rb.velocity.magnitude >= 0.1f)
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(rb.velocity, Vector3.up), 360 * Time.deltaTime);
    }
    public void Jump()
    {
        if (Physics.SphereCast(new Ray(transform.position, -transform.up), 0.5f, 0.6f, groundingLayers))
        {
            Debug.Log("Jumping");
            rb.AddForce(0, jumpForce, 0);
        }
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
