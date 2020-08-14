using UnityEngine;

namespace Coffee.BehaviourTree.Decorator
{
    public class TreeRepeaterNode : TreeDecoratorNode
    {
        public override Result Execute()
        {
            Debug.Log("Repeater repeated with value: " + child.Execute());
            return Result.Running;
        }

        public TreeRepeaterNode(BehaviourTree tree) : base(tree)
        {
        }
    }
}