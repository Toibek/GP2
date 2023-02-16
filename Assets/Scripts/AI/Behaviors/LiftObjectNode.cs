using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class LiftObjectNode : Node
{
    private float _grabRadius;
    private Transform _heldObject;
    private Transform _thisTransform;
    private Collider[] _hits;
    public LiftObjectNode(Transform thisTransform,float grabRadius) : base()
    {
        _grabRadius = grabRadius;
        _thisTransform = thisTransform;
        _hits = new Collider[25];
    }

    public override NodeState Evaluate()
    {

        if (_heldObject == null)
        {
            var lift = ClosestLiftable();
            if (lift == null) return NodeState.FAILURE;
            Vector3 runPos = lift.transform.position;
            _heldObject = lift.transform;
            _heldObject.position = _thisTransform.position + _thisTransform.up * 2;
            _heldObject.parent = _thisTransform;
            if (_heldObject.TryGetComponent(out Rigidbody rb))
            {
                rb.isKinematic = true;
            }
            if (_heldObject.TryGetComponent(out Animator anim))
            {
                anim.speed = 0;
            }
            GetRootNode().SetData(TreeVariables.HasPickedUp, true);
            GetRootNode().SetData(TreeVariables.PickUpArea, runPos);
        }
        else
        {
            _heldObject.parent = null;
            _heldObject.position = _thisTransform.position + _thisTransform.forward;
            if (_heldObject.TryGetComponent(out Rigidbody rb))
            {
                rb.isKinematic = false;
            }
            if (_heldObject.TryGetComponent(out Animator anim))
            {
                anim.speed = 1;
            }
            _heldObject = null;
            GetRootNode().SetData(TreeVariables.HasPickedUp, false);
        }

        return NodeState.SUCCESS;
    }

    private Liftable ClosestLiftable()
    {
        float d = Mathf.Infinity;
        Liftable closest = null;
        if (Physics.OverlapSphereNonAlloc(_thisTransform.position, _grabRadius, _hits) != 0)
        {
            for (int i = 0; i < _hits.Length; i++)
            {
                if (_hits[i] == null) continue;

                float dis = Vector3.Distance(_hits[i].transform.position, _thisTransform.position);
                if (_hits[i].TryGetComponent<Liftable>(out Liftable newLiftableObject))
                {
                    if (dis < d)
                    {
                        d = dis;
                    
                        closest = newLiftableObject;
                    }
                }
            }
        }
            
        return closest;
    }
}
