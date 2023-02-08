using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using BehaviorTree;

public class Dazed : Node
{
    private float _dazedTime = 0;
    public Dazed(float initDazed = 0f) : base()
    {
        _dazedTime = initDazed;
    }

    public override NodeState Evaluate()
    {

        if (GetData(TreeVariables.Dazed) != null)
        {
            _dazedTime = (float)GetData(TreeVariables.Dazed);
        }
        else
        {
            GetRootNode().SetData(TreeVariables.Dazed, 0f);
        }

        _dazedTime -= Time.deltaTime;
        if (_dazedTime <= 0)
            return NodeState.SUCCESS;
        else
            return NodeState.FAILURE;
    }
}
