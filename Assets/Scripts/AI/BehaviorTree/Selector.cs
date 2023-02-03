using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class Selector : Node
    {
        public Selector() : base()
        {

        }

        public Selector(List<Node> children) : base(children)
        {

        }

        public override NodeState Evaluate()
        {

            foreach (Node node in children)
            {
                switch (node.Evaluate())
                {
                    case NodeState.FAILURE:
                        continue;
                    case NodeState.SUCCESS:
                        state = NodeState.SUCCESS;
                        continue;
                    case NodeState.RUNNING:
                        state = NodeState.RUNNING;
                        continue;
                    default:
                        continue;
                }
            }

            state = NodeState.FAILURE;
            return state;
        }
    }
}
