using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;
using UnityEngine.AI;

public class RunFromPlayer : Node
{
    private NavMeshAgent _agent;
    private Transform _thisTransform;
    private float _runLength;
    public RunFromPlayer(Transform thisTransform, NavMeshAgent agent, float runLength) : base()
    {
        _thisTransform = thisTransform;
        _agent = agent;
        _runLength = runLength;
    }
    public override NodeState Evaluate()
    {
        Transform RunFromTransform = (Transform)GetData("Player");
        Debug.Log("Run From Player Movement");
        if (RunFromTransform != null)
        {
            _agent.SetDestination(
                    _thisTransform.position
                    - (RunFromTransform.position - _thisTransform.position).normalized * _runLength
                    );
            return NodeState.SUCCESS;
            //if (NavMesh.Raycast(transform.position, RunFromTransform.position, out NavMeshHit hit, NavMesh.AllAreas))
            //{
            //    _agent.SetDestination(
            //                transform.position
            //                - (hit.position - transform.position).normalized * 3f
            //     );
            //}
            //else
            //{
            //    _agent.SetDestination(
            //        transform.position
            //        - (RunFromTransform.position - transform.position).normalized * 3f
            //        );
            //}
        }
        return NodeState.FAILURE;
    }
}
