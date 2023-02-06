using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class IsAwake : Node
{

    private bool _isAwake;
    private Collider[] Hits = new Collider[4];
    private float _detectRadius;
    private Transform _thisTransform;
    private LayerMask _playerMask;

    public IsAwake(bool startsAwake, Transform thisTransform, float detectRadius, int howManyPlayerToCheckFor, LayerMask playerMask) : base()
    {
        _isAwake = startsAwake;
        _thisTransform = thisTransform;
        _detectRadius = detectRadius;
        Hits = new Collider[howManyPlayerToCheckFor];
        _playerMask = playerMask;
    }

    public override NodeState Evaluate()
    {
        if (_isAwake) return NodeState.SUCCESS;
        if (_detectRadius != 0)
        {
            if (Physics.OverlapSphereNonAlloc(_thisTransform.position, _detectRadius, Hits, _playerMask) > 0)
            {
                _isAwake = true;
                return NodeState.SUCCESS;
            }
            else
            {
                ClearData("Player");
                return NodeState.FAILURE;
            }
        }
        return NodeState.RUNNING;

    }
}
