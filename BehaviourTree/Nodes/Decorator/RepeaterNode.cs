using UnityEngine;

namespace Coffee.BehaviourTree.Decorator
{
    public class RepeaterNode : DecoratorNode
    {
        public RepeaterNode(BehaviourTree tree, IBehaviourNode child) : base(tree, child)
        {
        }

        public override Result Execute()
        {
            Debug.Log("Repeater repeated with value: " + Child.Execute());
            return Result.Running;
        }
    }
}