using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class IsNotTamed : Node
{
    bool _tamed;
    public IsNotTamed(bool tamed) : base()
    {
        _tamed = tamed;
    }

    public override NodeState Evaluate()
    {
        if (GetData("Tamed") != null)
            _tamed = (bool)GetData(TreeVariables.Tamed);
        else
            GetRootNode().SetData(TreeVariables.Tamed, _tamed);

        if (_tamed)
            return NodeState.FAILURE;
        else
            return NodeState.SUCCESS;

    }
}
