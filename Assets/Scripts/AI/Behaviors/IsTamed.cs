using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class IsTamed : Node
{
    bool _tamed;
    public IsTamed(bool tamed) : base()
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
            return NodeState.SUCCESS;
        else
            return NodeState.FAILURE;

    }

}
