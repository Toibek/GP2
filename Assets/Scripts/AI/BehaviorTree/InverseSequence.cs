using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static Unity.VisualScripting.Metadata;

namespace BehaviorTree
{
    public class InverseSequence : Node
    {
        public InverseSequence() : base()
        {

        }

        public InverseSequence(List<Node> children) : base(children)
        {

        }

        public override NodeState Evaluate()
        {

            bool anyChildIsRunning = false;

            foreach (Node node in children)
            {
                switch (node.Evaluate())
                {
                    case NodeState.FAILURE:
                        state = NodeState.SUCCESS;
                        return state;
                    case NodeState.SUCCESS:
                        continue;
                    case NodeState.RUNNING:
                        anyChildIsRunning = true;
                        continue;
                    default:
                        state = NodeState.SUCCESS;
                        return state;
                }
            }

            state = anyChildIsRunning ? NodeState.RUNNING : NodeState.FAILURE;
            return state;
        }
    }

}

