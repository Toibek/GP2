using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class ResetGravity : Node
{
    private Rigidbody _rb;
    public ResetGravity(Rigidbody rigidbody) : base()
    {
        _rb = rigidbody;
    }

    public override NodeState Evaluate()
    {
        if (!_rb.useGravity) 
            _rb.velocity = new Vector3(_rb.velocity.x,0, _rb.velocity.z);

        return NodeState.RUNNING;
    }

}
