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
    private float _runLength;
    public MoveAwayFromPosition(Rigidbody rigidbody, float runSpeed,float runLength, string moveToPositionSaveVariable = "RequestedNewPosition", float smoothDampSpeed = 0.6f) : base() 
    {
        _rb = rigidbody;
        _runSpeed = runSpeed;

        if (runLength != 0)
            _runLength = runLength;
        else
            _runLength = 1;

        _moveToPositionSaveVariable = moveToPositionSaveVariable;
        _smoothDampSpeed = smoothDampSpeed;
    }

    public override NodeState Evaluate()
    {
        //return base.Evaluate();
        Transform RunFromTransform = (Transform)GetData("Player");
        if (RunFromTransform != null)
        {
            if (PebbleCreature.Debug) Debug.Log("MoveAway");
            Vector3 newPosFromRigidbody =
                
                ( 
                new Vector3(_rb.position.x,0, _rb.position.z)
                - new Vector3 (RunFromTransform.position.x,0,RunFromTransform.position.z) 
                ).normalized;

            Node parentToUse = this;
            for(int i = 0; i < 2; i++)
            {
                if (parentToUse.Parent != null)
                parentToUse = parentToUse.Parent;
            }
            parentToUse.ClearData(_moveToPositionSaveVariable);
            parentToUse.SetData(_moveToPositionSaveVariable, _rb.position + (newPosFromRigidbody * _runLength));
            //Debug.Log(newPosFromRigidbody);

            Vector3 newVel = newPosFromRigidbody * _runSpeed + Vector3.up * _rb.velocity.y;

            if (GetData("CurrentVelocity") != null && (Vector3)GetData("CurrentVelocity") != null)
            {
                _currentVelocity = (Vector3)GetData("CurrentVelocity");
            }

            _rb.velocity = Vector3.SmoothDamp(_rb.velocity, newVel, ref _currentVelocity, _smoothDampSpeed);

            GetRootNode().SetData("CurrentVelocity", _currentVelocity);

            return NodeState.SUCCESS;

            //if (NavMesh.Raycast(transform.position, RunFromTransform.position, out NavMeshHit hit, NavMesh.AllAreas))
            //{
            //  Closest Point to navMesh here with Hit.position
            //}
            //else
            //{
            //  It's on the navMesh here
            //}

        }
        return NodeState.FAILURE;

    }
}
