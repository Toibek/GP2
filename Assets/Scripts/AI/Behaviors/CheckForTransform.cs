using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class CheckForTransform : Node
{
    private LayerMask _searchMask;
    private float _detectRadius;
    private string _saveString;
    private Transform _thisTransform;
    private Collider[] Hits;
    public CheckForTransform(Transform thisTransform, float detectRadius, int howTransformsToCheckFor, LayerMask searchMask, string saveString = "") : base()
    {
        _detectRadius = detectRadius;
        _thisTransform = thisTransform;
        _searchMask = searchMask;
        _saveString = saveString;
        Hits = new Collider[howTransformsToCheckFor];
    }

    public override NodeState Evaluate()
    {
        if (_detectRadius != 0)
        {
            if (Physics.OverlapSphereNonAlloc(_thisTransform.position, _detectRadius, Hits, _searchMask) > 0)
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
                GetRootNode().SetData(_saveString, Hits[index].transform);
                return NodeState.SUCCESS;
            }
            else
            {
                GetRootNode().ClearData(_saveString);
                return NodeState.FAILURE;
            }
        }
        return NodeState.FAILURE;
    }
}
