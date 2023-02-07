using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;
public class GravityNode : Node
{
    private Rigidbody _rb;
    public GravityNode(Rigidbody rigidbody) : base()
    {
        _rb = rigidbody;
    }

    public override NodeState Evaluate()
    {
        if (!_rb.useGravity)
        {
            _rb.velocity = _rb.velocity + Physics.gravity * Time.deltaTime;
        }

        return NodeState.RUNNING;
    }
}
