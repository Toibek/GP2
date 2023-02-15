using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class SetVariableNode : Node
{
    private bool _inRoot;
    private object _value;
    private string _key;
    public SetVariableNode(string key,object value, bool inRoot = false) : base()
    {
        _inRoot = inRoot;
        _value = value;
        _key = key;
    }
    public override NodeState Evaluate()
    {
        if (_inRoot)
        {
            GetRootNode().SetData(_key, _value);
        }
        else
        {
            Parent.SetData(_key, _value);
        }
        return base.Evaluate();
    }
}
