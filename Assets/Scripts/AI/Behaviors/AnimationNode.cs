using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;
public class AnimationNode : Node
{
    private Animator _animator;
    private Rigidbody _rb;
    public AnimationNode(Animator animator, Rigidbody rigidbody) : base()
    {
        _animator = animator;
        _rb = rigidbody;
    }

    public override NodeState Evaluate()
    {
        if (_animator != null && _rb != null)
            _animator.SetFloat("Velocity", _rb.velocity.magnitude);
        else
            return NodeState.SUCCESS;
        if (GetData(TreeVariables.IsAwake) != null)
            _animator.SetBool("IsAwake", (bool)GetData(TreeVariables.IsAwake));
        return NodeState.SUCCESS;
    }
}
