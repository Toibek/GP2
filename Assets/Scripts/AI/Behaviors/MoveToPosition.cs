using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class MoveToPosition : Node
{
    private Rigidbody _rb;
    private Vector3 _currentVelocity;
    private float _smoothDampSpeed = 0.6f;
    private float _stopDistance;
    private string _key;
    private float _runSpeed;
    public MoveToPosition(Rigidbody rigidbody, string keyToPosition, float runSpeed, float stopDistance, float smoothDampSpeed = 0.6f) : base()
    {
        _rb = rigidbody;
        _key = keyToPosition;
        _runSpeed = runSpeed;
        _stopDistance = stopDistance;
        _smoothDampSpeed = smoothDampSpeed;
    }

    public override NodeState Evaluate()
    {
        if (Parent.GetData(_key) == null || (Vector3)Parent.GetData(_key) == null) 
        {
            return NodeState.FAILURE; 
        }

        Vector3 RunTowardsPosition = (Vector3) GetData(_key);
        if ((RunTowardsPosition - _rb.position).sqrMagnitude < _stopDistance * _stopDistance) 
        {
            Parent.ClearData(_key);
            return NodeState.FAILURE;
        }

        if (PebbleCreature.Debug) Debug.Log("MoveToPosition");
        Vector3 newPosFromRigidbody =
            (
                new Vector3(RunTowardsPosition.x, 0, RunTowardsPosition.z)
            - new Vector3(_rb.position.x, 0, _rb.position.z)
            ).normalized;

        Vector3 newVel = newPosFromRigidbody * _runSpeed + Vector3.up * _rb.velocity.y;

        if (GetData("CurrentVelocity") != null && (Vector3)GetData("CurrentVelocity") != null)
        {
            _currentVelocity = (Vector3)GetData("CurrentVelocity");
        }

        _rb.velocity = Vector3.SmoothDamp(_rb.velocity, newVel, ref _currentVelocity, _smoothDampSpeed);

        GetRootNode().SetData("CurrentVelocity", _currentVelocity);

        return NodeState.SUCCESS;
    }
}
