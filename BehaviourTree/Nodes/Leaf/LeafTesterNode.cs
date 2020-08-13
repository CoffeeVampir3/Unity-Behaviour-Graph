using UnityEngine;

namespace Coffee.BehaviourTree.Leaf
{
    public class LeafTesterNode : LeafNode
    {
        public LeafTesterNode(BehaviourTree tree) : base(tree)
        {
        }

        public override BehaviourTree.Result Execute()
        {
            Debug.Log("Reached the leaf!");
            return BehaviourTree.Result.Success;
        }
    }
}