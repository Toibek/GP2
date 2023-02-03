using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class RotateTowardsVelocity : Node
{
    private Rigidbody _rb;
    private float _rotationAngelsDelta;
    public RotateTowardsVelocity(Rigidbody rb, float rotationAngelsDelta) : base()
    {
        _rb = rb;
        _rotationAngelsDelta = rotationAngelsDelta;
    }

    public override NodeState Evaluate()
    {
        if (_rb.velocity.sqrMagnitude > 0.5f)_rb.rotation = Quaternion.RotateTowards(_rb.rotation,Quaternion.LookRotation(new Vector3(_rb.velocity.x,0,_rb.velocity.z),Vector3.up), _rotationAngelsDelta * Time.deltaTime);
        return NodeState.SUCCESS;
    }
}
