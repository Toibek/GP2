using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;
using UnityEngine.AI;

public class IdleMove : Node
{
    private Rigidbody _rb;

    private Transform _thisTransform;
    
    private Vector3 _newPosition;
    private Vector3 _currentVelocity;

    private float _smoothDampSpeed = 0.6f;

    private bool _GenerateNewPos;

    private float _runSpeed;
    private float _movedForSeconds;
    private float _resetMovePositionAfter = 5f;
    private float _moveArea;
    private float _stopDistance;

    public IdleMove(Transform thisTransform, Rigidbody rigidbody, float moveArea, float stopDistance,float runSpeed, float smoothDampSpeed = 0.6f) : base()
    {
        _rb = rigidbody;
        _thisTransform = thisTransform;
        _moveArea = moveArea;
        _runSpeed = runSpeed;
        _stopDistance = stopDistance;
        _GenerateNewPos = true;
        _smoothDampSpeed = smoothDampSpeed;
    }

    public override NodeState Evaluate()
    {

        _movedForSeconds += Time.deltaTime;

        if (_movedForSeconds > _resetMovePositionAfter)
        {
            _movedForSeconds = 0;
            _GenerateNewPos = true;
        }

        if (_GenerateNewPos)
        {
            _newPosition = 
                _rb.position +
                new Vector3(
                        Random.Range(-_moveArea, _moveArea),// x
                        0,                                  // y
                        Random.Range(-_moveArea, _moveArea) // z
                    );
            if (NavMesh.SamplePosition(_newPosition,out NavMeshHit hit, 3f, NavMesh.AllAreas))
            {
                _newPosition = hit.position;
            }
            _GenerateNewPos = false;
        }


        if ((new Vector3(_newPosition.x, 0, _newPosition.z) - new Vector3(_rb.position.x, 0, _rb.position.z)).sqrMagnitude > _stopDistance * _stopDistance)
        {
            if (PebbleCreature.Debug) Debug.Log("IdleMove");

            Vector3 newVel = (new Vector3(_newPosition.x, 0, _newPosition.z) - new Vector3(_rb.position.x, 0, _rb.position.z)).normalized * _runSpeed + Vector3.up * _rb.velocity.y;

            if (GetData("CurrentVelocity") != null && (Vector3)GetData("CurrentVelocity") != null)
            {
                _currentVelocity = (Vector3)GetData("CurrentVelocity");
            }

            _rb.velocity = Vector3.SmoothDamp(_rb.velocity, newVel, ref _currentVelocity, _smoothDampSpeed);
            GetRootNode().SetData("CurrentVelocity", _currentVelocity);

        }
        else
        {
            GetRootNode().SetData("CurrentVelocity", _currentVelocity);
            _movedForSeconds = 0;
            Parent.SetData("ReachedIdlePos", true);
            _GenerateNewPos = true;
        }

        return NodeState.SUCCESS;
    }
}
