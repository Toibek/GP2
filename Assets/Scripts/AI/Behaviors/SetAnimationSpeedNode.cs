using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class SetAnimationSpeedNode : Node
{
    private Animator _animator;
    private float _speed;
    public SetAnimationSpeedNode(Animator animator, float animationSpeed) : base()
    {
        _animator = animator;
    }


    public override NodeState Evaluate()
    {
        if (_animator.speed != _speed)
        {
            _animator.speed = _speed;
        }

        return NodeState.SUCCESS;
    }
}
