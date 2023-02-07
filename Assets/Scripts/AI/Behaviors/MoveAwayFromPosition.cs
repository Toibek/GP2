using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class MoveAwayFromPosition : Node
{
    private Rigidbody _rb;
    private Vector3 _currentVelocity;
    private string _moveToPositionSaveVariable;
    private float _smoothDampSpeed = 0.6f;
    private float _runSpeed;
    public MoveAwayFromPosition(Rigidbody rigidbody, float runSpeed, string moveToPositionSaveVariable = "RequestedNewPosition") : base() 
    {
        _rb = rigidbody;
        _runSpeed = runSpeed;
        _moveToPositionSaveVariable = moveToPositionSaveVariable;
    }

    public override NodeState Evaluate()
    {
        //return base.Evaluate();
        Transform RunFromTransform = (Transform)GetData("Player");
        if (RunFromTransform != null)
        {
            Vector3 newPosFromRigidbody =
                
                -( 
                    new Vector3 (RunFromTransform.position.x,0,RunFromTransform.position.z) 
                -   new Vector3(_rb.position.x,0, _rb.position.z)
                ).normalized
                * _runSpeed;

            Parent.SetData(_moveToPositionSaveVariable, _rb.transform.TransformPoint(newPosFromRigidbody));
            //Debug.Log(newPosFromRigidbody);

            Vector3 newVel = newPosFromRigidbody * 10f + Vector3.up * _rb.velocity.y;

            _rb.velocity = Vector3.SmoothDamp(_rb.velocity, newVel, ref _currentVelocity, _smoothDampSpeed);
            //Debug.Log(_rb.velocity);
            return NodeState.SUCCESS;
            //if (NavMesh.Raycast(transform.position, RunFromTransform.position, out NavMeshHit hit, NavMesh.AllAreas))
            //{
            //    _agent.SetDestination(
            //                transform.position
            //                - (hit.position - transform.position).normalized * 3f
            //     );
            //}
            //else
            //{
            //    _agent.SetDestination(
            //        transform.position
            //        - (RunFromTransform.position - transform.position).normalized * 3f
            //        );
            //}
        }
        return NodeState.FAILURE;

    }
}
