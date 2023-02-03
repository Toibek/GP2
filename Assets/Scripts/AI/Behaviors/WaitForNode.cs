using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using BehaviorTree;

public class WaitForNode : Node
{
    private float _moveTimer = 0;
    private float _moveAfterSec;

    private float _moveAfterMin = 2f;
    private float _moveAfterMax = 4f;

    public WaitForNode(float movmentFreqenceMin, float movementFrequenceMax) : base()
    {
        _moveAfterMin = movmentFreqenceMin;
        _moveAfterMax = movementFrequenceMax;
    }

    public override NodeState Evaluate()
    {
        _moveTimer += Time.deltaTime;
        if (_moveTimer > _moveAfterSec)
        {
            _moveTimer = 0;
            _moveAfterSec = Random.Range(_moveAfterMin, _moveAfterMax);
            return NodeState.SUCCESS;
        }
        return NodeState.FAILURE;
    }
}
