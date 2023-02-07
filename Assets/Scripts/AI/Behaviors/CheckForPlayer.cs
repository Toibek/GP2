using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class CheckForPlayer : Node
{
    private LayerMask _playerMask;
    private float _detectRadius;
    private Transform _thisTransform;
    private Collider[] Hits;
    public CheckForPlayer(Transform thisTransform,float detectRadius,int howManyPlayerToCheckFor, LayerMask playerMask) : base()
    {
        _detectRadius = detectRadius;
        _thisTransform = thisTransform;
        _playerMask = playerMask;
        Hits = new Collider[howManyPlayerToCheckFor];
    }

    public override NodeState Evaluate()
    {
        if (_detectRadius != 0)
        {
            if (Physics.OverlapSphereNonAlloc(_thisTransform.position, _detectRadius, Hits, _playerMask) > 0)
            {
                int index = -1;
                float lastDistance = float.MaxValue;
                for (int i = 0; i < Hits.Length; i++)
                {
                    if (Hits[i] != null && lastDistance > (Hits[i].transform.position - _thisTransform.position).sqrMagnitude)
                    {
                        lastDistance = (Hits[i].transform.position - _thisTransform.position).sqrMagnitude;
                        index = i;
                    }
                }
                Parent.SetData("Player", Hits[index].transform);
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
