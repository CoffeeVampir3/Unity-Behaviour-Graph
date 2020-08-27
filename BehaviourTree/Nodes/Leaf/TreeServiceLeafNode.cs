using UnityEngine.Events;

namespace Coffee.BehaviourTree.Leaf
{
    internal class TreeServiceLeafNode : TreeLeafNode
    {

        public TreeServiceLeafNode(BehaviourTree tree) : base(tree)
        {
        }

        public override Result Execute()
        {
            
            return Result.Success;
        }

        public override void Reset()
        {
            //Empty
        }
    }
}