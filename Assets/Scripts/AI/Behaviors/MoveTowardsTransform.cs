using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class MoveTowardsTransform : Node
{
    private Transform _followTransform;
    private Rigidbody _rb;
    private string _followKey;
    private float _runSpeed;
    private float _smoothDampSpeed;
    private float _stopFollowdistance;
    private Vector3 _currentVelocity;
    public MoveTowardsTransform(Rigidbody rigidbody,float runSpeed,float stopFollowdistance,string followKey = "", float smoothDampSpeed = 0.6f) : base()
    {
        _stopFollowdistance = stopFollowdistance;
        _smoothDampSpeed = smoothDampSpeed;
        _runSpeed = runSpeed;
        _rb = rigidbody;
        if (followKey != "")
            _followKey = followKey;
        else
            _followKey = TreeVariables.FollowTransform;
    }

    public override NodeState Evaluate()
    {
        _followTransform = (Transform)GetData(_followKey);
        if (_followTransform == null) return NodeState.FAILURE;

        if (PebbleCreature.Debug) Debug.Log("MoveTowardsTransform");
        Vector3 newPosFromRigidbody =
            (
                new Vector3(_followTransform.position.x, 0, _followTransform.position.z)
            - new Vector3(_rb.position.x, 0, _rb.position.z)
            ).normalized;

        if (
            (new Vector3(_followTransform.position.x, 0, _followTransform.position.z)
            - new Vector3(_rb.position.x, 0, _rb.position.z)
            ).sqrMagnitude < _stopFollowdistance * _stopFollowdistance
            )
        {
            return NodeState.RUNNING;
        }
        Vector3 newVel = newPosFromRigidbody * _runSpeed + Vector3.up * _rb.velocity.y;

        if (GetData(TreeVariables.CurrentVelocity) != null && (Vector3)GetData(TreeVariables.CurrentVelocity) != null)
        {
            _currentVelocity = (Vector3)GetData(TreeVariables.CurrentVelocity);
        }

        _rb.velocity = Vector3.SmoothDamp(_rb.velocity, newVel, ref _currentVelocity, _smoothDampSpeed);

        GetRootNode().SetData(TreeVariables.CurrentVelocity, _currentVelocity);

        return NodeState.SUCCESS;
    }
}
