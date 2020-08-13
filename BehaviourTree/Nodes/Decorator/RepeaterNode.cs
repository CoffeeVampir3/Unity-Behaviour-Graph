using UnityEngine;

namespace Coffee.BehaviourTree.Decorator
{
    public class RepeaterNode : DecoratorNode
    {
        public RepeaterNode(BehaviourTree tree, IBehaviourNode child) : base(tree, child)
        {
        }

        public override BehaviourTree.Result Execute()
        {
            Debug.Log("Repeater repeated with value: " + Child.Execute());
            return BehaviourTree.Result.Running;
        }
    }
}