using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;
using UnityEngine.AI;

public class IdleMove : Node
{
    private NavMeshAgent _agent;
    private Transform _thisTransform;
    private float _moveArea;

    public IdleMove(Transform thisTransform, NavMeshAgent agent, float moveArea) : base()
    {
        _agent = agent;
        _thisTransform = thisTransform;
        _moveArea = moveArea;
    }

    public override NodeState Evaluate()
    {

        _agent.SetDestination
            (
                _thisTransform.position + 
                new Vector3(Random.Range
                (
                    -_moveArea, _moveArea),             // x
                    0,                                  // y
                    Random.Range(-_moveArea, _moveArea) // z
                )
            );
        return NodeState.SUCCESS;
    }
}
