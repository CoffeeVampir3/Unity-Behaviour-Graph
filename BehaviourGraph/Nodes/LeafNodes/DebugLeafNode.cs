using Coffee.BehaviourTree.Leaf;

namespace Coffee.Behaviour.Nodes.LeafNodes
{
    public class DebugLeafNode : LeafNode
    {
        public string debugMessage;
        
        protected override void Init()
        {
            base.Init();
            thisNode = new TreeLeafDebugNode(parentTree);
        }
    }
}