using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;
public class CheckForGround : Node
{

    private Transform _thisTransform;
    private RaycastHit[] _result = new RaycastHit[10];
    private LayerMask _groundLayer;
    private float _raycastDistance;
    private float _radius;

    public CheckForGround(Transform thisTransform,LayerMask groundLayerMask, float raycastDistanceToGround = 2f, float radius = 1f) : base()
    {
        _groundLayer = groundLayerMask;
        _raycastDistance = raycastDistanceToGround;
        _thisTransform = thisTransform;
        _result = new RaycastHit[1];
        _radius = radius;
    }

    public override NodeState Evaluate()
    {
        Collider[] result = new Collider[10];
        if (Physics.OverlapSphereNonAlloc(_thisTransform.position, _radius, result, _groundLayer, QueryTriggerInteraction.Ignore) > 0) // Checks for default layer atm
        {
            int childrenCount = _thisTransform.childCount;
            Transform[] childrenTransform = new Transform[childrenCount];
            for (int i = 0; i < childrenCount; i++)
            {
                for (int x = 0; x < 10; x++)
                {
                    if (childrenTransform[i] != result[x] && result[x] != _thisTransform)
                    {
                        result[x] = null;
                    }
                }
            }

            for (int i = 0; i < 10; i++)
            {
                if (result[i] != null)
                {
                    if (PebbleCreature.Debug) Debug.Log("GroundCheckTrue overlap " + result[i].transform.name);
                    return NodeState.SUCCESS;
                }
            }
        }

        if (Physics.SphereCastNonAlloc(_thisTransform.position, _radius, Vector3.down,_result, _raycastDistance, _groundLayer) > 0) // Checks for default layer atm
        {
            if (PebbleCreature.Debug) Debug.Log("GroundCheckTrue raycast " + _result[0].transform.name);

            return NodeState.SUCCESS;
        }

        return NodeState.FAILURE;
    }
}
