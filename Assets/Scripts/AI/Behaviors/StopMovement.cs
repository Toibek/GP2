using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class StopMovement : Node
{
    private Rigidbody _rb;
    private Vector3 _currentVelocity = Vector3.zero;
    private float _smoothDampSpeed = 0.6f;
    public StopMovement(Rigidbody rigidbody, float smoothDampSpeed = 0.6f) : base()
    {
        _rb = rigidbody;
        _smoothDampSpeed = smoothDampSpeed;
    }

    public override NodeState Evaluate()
    {
        if (GetData("CurrentVelocity") != null && (Vector3)GetData("CurrentVelocity") != null)
        {
            _currentVelocity = (Vector3)GetData("CurrentVelocity");
        }

        _rb.velocity = Vector3.SmoothDamp(_rb.velocity, new Vector3(0, _rb.velocity.y, 0), ref _currentVelocity, _smoothDampSpeed);

        GetRootNode().SetData("CurrentVelocity", _currentVelocity);
        if (PebbleCreature.Debug) Debug.Log("StopMovement");

        return NodeState.SUCCESS;
    }
}
