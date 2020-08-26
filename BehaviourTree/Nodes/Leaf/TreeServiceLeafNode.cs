using UnityEngine.Events;

namespace Coffee.BehaviourTree.Leaf
{
    internal class TreeServiceLeafNode : TreeLeafNode
    {
        public UnityEvent serviceEvent;
        
        public TreeServiceLeafNode(BehaviourTree tree) : base(tree)
        {
        }

        public override Result Execute()
        {
            serviceEvent?.Invoke();
            return Result.Success;
        }

        public override void Reset()
        {
            //Empty
        }
    }
}