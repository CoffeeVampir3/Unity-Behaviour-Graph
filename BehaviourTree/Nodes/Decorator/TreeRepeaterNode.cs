using System;
using UnityEngine;

namespace Coffee.BehaviourTree.Decorator
{
    [Serializable]
    public class TreeRepeaterNode : TreeDecoratorNode
    {
        public override Result Execute()
        {
            Debug.Log("Repeater executing");
            child.Execute();
            return Result.Running;
        }

        public TreeRepeaterNode(BehaviourTree tree) : base(tree)
        {
        }
    }
}