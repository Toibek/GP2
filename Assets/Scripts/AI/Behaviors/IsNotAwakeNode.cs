using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class IsNotAwakeNode : Node
{
    private bool _isAwake;

    public IsNotAwakeNode(bool startsAwake) : base()
    {
        _isAwake = startsAwake;
    }

    public override NodeState Evaluate()
    {
        if (GetData(TreeVariables.IsAwake) != null)
        {
            _isAwake = (bool)GetData(TreeVariables.IsAwake);
        }

        if (_isAwake)
        {
            GetRootNode().SetData(TreeVariables.IsAwake, _isAwake);
            return NodeState.FAILURE;
        }
        //if (_detectRadius != 0)
        //{
        //    if (Physics.OverlapSphereNonAlloc(_thisTransform.position, _detectRadius, Hits, _playerMask) > 0)
        //    {
        //        _isAwake = true;
        //        return NodeState.SUCCESS;
        //    }
        //    else
        //    {
        //        ClearData("Player");
        //        return NodeState.FAILURE;
        //    }
        //}
        GetRootNode().SetData(TreeVariables.IsAwake, _isAwake);
        return NodeState.SUCCESS;

    }
}
