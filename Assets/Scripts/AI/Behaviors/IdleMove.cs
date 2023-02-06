using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;
using UnityEngine.AI;

public class IdleMove : Node
{
    private Rigidbody _rb;

    private Transform _thisTransform;

    private Vector3 newPosition;

    private bool _GenerateNewPos;

    private float _moveArea;
    private float _stopDistance;

    public IdleMove(Transform thisTransform, Rigidbody rigidbody, float moveArea, float stopDistance) : base()
    {
        _rb = rigidbody;
        _thisTransform = thisTransform;
        _moveArea = moveArea;
        _stopDistance = stopDistance;
        _GenerateNewPos = true;
    }

    public override NodeState Evaluate()
    {
        if (_GenerateNewPos)
        {
            newPosition = 
                _rb.position +
                new Vector3(
                        Random.Range(-_moveArea, _moveArea),// x
                        0,                                  // y
                        Random.Range(-_moveArea, _moveArea) // z
                    );
            _GenerateNewPos = false;
        }

        if ((newPosition - _rb.position).sqrMagnitude > _stopDistance * _stopDistance)
        {

            _rb.velocity = (newPosition - _rb.position).normalized + Vector3.up * _rb.velocity.y;
        }
        else
        {
            Parent.SetData("ReachedIdlePos", true);
            _GenerateNewPos = true;
        }

        //_agent.SetDestination
        //    (
        //        _thisTransform.position + 
        //        new Vector3(Random.Range
        //        (
        //            -_moveArea, _moveArea),             // x
        //            0,                                  // y
        //            Random.Range(-_moveArea, _moveArea) // z
        //        )
        //    );


        return NodeState.SUCCESS;
    }
}
