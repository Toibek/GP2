using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class HasPickedUp : Node
{

    private bool _hasPickedUp = false;

    public HasPickedUp() : base()
    {

    }

    public override NodeState Evaluate()
    {
        if (GetData(TreeVariables.HasPickedUp) != null)
        {
            _hasPickedUp = (bool)GetData(TreeVariables.HasPickedUp);
        }

        if (_hasPickedUp)
        {
            //GetRootNode().SetData(TreeVariables.PickUpArea, _hasPickedUp);
            return NodeState.SUCCESS;
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
        //GetRootNode().SetData(TreeVariables.PickUpArea, _hasPickedUp);
        return NodeState.FAILURE;

    }
}
