using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class WaitForNode : Node
{
    private float _moveTimer = 0;
    private float _moveAfterSec;

    private float _moveAfterMin = 2f;
    private float _moveAfterMax = 4f;

    private string _boolKeyToActivate;

    public WaitForNode(float movmentFreqenceMin, float movementFrequenceMax, string boolKey) : base()
    {
        _boolKeyToActivate = boolKey;
        _moveAfterMin = movmentFreqenceMin;
        _moveAfterMax = movementFrequenceMax;

    }

    public override NodeState Evaluate()
    {

        try
        {
            if (!(bool)Parent.GetData(_boolKeyToActivate))
            {
                if (PebbleCreature.Debug) Debug.Log("WaitForNode Found Key");
                return NodeState.SUCCESS;
            }

        }
        catch
        {
            if (Parent != null) Parent.SetData(_boolKeyToActivate, true);

        }
        if (PebbleCreature.Debug) Debug.Log("WaitForNode");
        _moveTimer += Time.deltaTime;
        if (_moveTimer > _moveAfterSec)
        {
            _moveTimer = 0;
            _moveAfterSec = Random.Range(_moveAfterMin, _moveAfterMax);
            Parent.SetData(_boolKeyToActivate, false);
            return NodeState.SUCCESS;
        }
        return NodeState.FAILURE;
    }
}
