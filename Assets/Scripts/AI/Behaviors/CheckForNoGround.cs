using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForNoGround : Node
{
    private Transform _thisTransform;
    private RaycastHit[] _result = new RaycastHit[1];
    private float _raycastDistance;

    public CheckForNoGround(Transform thisTransform, float raycastDistanceToGround = 2f) : base()
    {
        _raycastDistance = raycastDistanceToGround;
        _thisTransform = thisTransform;
        _result = new RaycastHit[1];
    }

    public override NodeState Evaluate()
    {

        if (Physics.RaycastNonAlloc(_thisTransform.position, Vector3.down, _result, _raycastDistance, 1 << 0) > 0) // Checks for default layer atm
        {
            return NodeState.FAILURE;
        }

        return NodeState.SUCCESS;
    }
}
